using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using AutoMapper;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Localization;
using TicTacToe.Domain;
using TicTacToe.Infrastructure.SignalR.Hubs;
using TicTacToe.Application.Events;
using TicTacToe.Domain.Game.Entity;
using TicTacToe.Application.Dto;
using TicTacToe.Domain.Game.ValueObject;
using TicTacToe.Infrastructure.SignalR.Resolver;
using TicTacToe.Domain.Game;
using TicTacToe.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace TicTacToe.Web
{
    public class Startup
    {
        
        public static ApplicationEnvironment appEnv = Microsoft.Extensions.PlatformAbstractions.PlatformServices.Default.Application;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc()
                .AddJsonOptions(opt =>
                {
                    opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                })
                .AddViewLocalization()
                .AddDataAnnotationsLocalization();

            services.AddDbContext<GameContext>(opt => opt.UseInMemoryDatabase());
            services.AddLogging();
            services.AddScoped<IPublisher, SignalRGamePublisher>();
            services.AddScoped<IGameRepository, GameInMemoryRepository>();

            // SignalR
            var settings = new JsonSerializerSettings();
            settings.ContractResolver = new SignalRContractResolver();

            var serializer = JsonSerializer.Create(settings);
            services.Add(new ServiceDescriptor(typeof(JsonSerializer),
                         provider => serializer,
                         ServiceLifetime.Transient));

            services.AddSignalR(options =>
            {
                options.Hubs.EnableDetailedErrors = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(LogLevel.Warning);
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
                //app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();


            Mapper.Initialize(config =>
            {
                config.CreateMap<GameInformationDto, GameInformation>().ConstructUsing(x => new GameInformation(x.Id)).ReverseMap();
                config.CreateMap<TokenDto, Token>().ReverseMap();
                config.CreateMap<StateDto, State>().ReverseMap();
                config.CreateMap<Grid, TokenDto[,]>().ConstructProjectionUsing(x => Mapper.Map<TokenDto[,]>(x.innerGrid));
                config.CreateMap<Board, BoardDto>();

                config.CreateMap<GameDto, Game>().ConstructUsing(x =>
                    new Game(
                        new GameInformation(x.GameInformation.Id),
                        new Board(
                            new Grid(Mapper.Map<Token[,]>(x.Grid)), x.MoveCounter)));
                config.CreateMap<Game, GameDto>().ConstructUsing(x =>
                    new GameDto(
                        Mapper.Map<GameInformationDto>(x.GameInfo),
                        Mapper.Map<TokenDto[,]>(x.Board.grid.innerGrid),
                        x.Board.moveCounter));
                config.CreateMap<Infrastructure.Database.DbModels.Game, Game>().ConvertUsing(x =>
                    new Game(
                        new GameInformation(guidIdentifiant: x.GuidIdentifiant)
                        {
                            NomPlayer1 = x.NomPlayer1,
                            NomPlayer2 = x.NomPlayer2
                        },
                        new Board(
                            (Grid)x.Grid, x.MoveCounter)));
                config.CreateMap<Game, Infrastructure.Database.DbModels.Game>()
                    .ForMember(db => db.GuidIdentifiant, m => m.MapFrom(u => u.Id))
                    .ForMember(db => db.NomPlayer1, m => m.MapFrom(u => u.GameInfo.NomPlayer1))
                    .ForMember(db => db.NomPlayer2, m => m.MapFrom(u => u.GameInfo.NomPlayer2))
                    .ForMember(db => db.Grid, m => m.MapFrom(u => (int)u.Board.grid))
                    .ForMember(db => db.MoveCounter, m => m.MapFrom(u => u.Board.moveCounter));

            });

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseMvc(config =>
            {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "App", action = "games" }
                );
            });
            
            var context = app.ApplicationServices.GetService<GameContext>();
            new InitializerToSeedGameContext(context).Seed();

            app.UseSignalR();

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("fr")
            });

        }
    }
}

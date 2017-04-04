using System;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.Extensions.Logging;
using AutoMapper;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TicTacToe.Application.Events;
using TicTacToe.Domain;
using TicTacToe.Domain.Core;
using TicTacToe.Application.Dto;
using TicTacToe.Domain.Game.Entity;
using TicTacToe.Domain.Game;
using TicTacToe.Domain.Game.ValueObject;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TicTacToe.Web.Controllers.Api
{
    public class GameController : Controller
    {
        private IGameRepository _repository;
        private ILogger<GameController> _logger;
        private IPublisher _eventPublisher;

        public GameController(IPublisher eventPublisher,
                                IGameRepository repository, 
                                ILogger<GameController> logger)
        {
            _eventPublisher = eventPublisher ?? throw new ArgumentNullException(nameof(eventPublisher));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region private methods
        private string GetErrorMessages(ModelErrorCollection errors)
        {
            if (errors != null)
            {
                return string.Join(
                           ", ",
                           errors.Where(error => !string.IsNullOrWhiteSpace(error.ErrorMessage))
                                 .Select(error => error.ErrorMessage)) +
                       string.Join(
                            ", ",
                            errors.Where(error => error.Exception != null && !string.IsNullOrWhiteSpace(error.Exception.Message))
                                  .Select(error => error.Exception.Message));
            }
            else
                return string.Empty;
        }
        private string GetErrorMessages(ModelStateEntry child)
        {
            string result = GetErrorMessages(child.Errors);
            if (child.Children != null)
            {
                result += string.Join(
                       ", ",
                       child.Children.Select(innerChild => GetErrorMessages(innerChild)));
            }
            return result;
        }
        private JsonResult GetItemExamenById<TDestination, TSource>(string id, Func<string, Result<TSource>> GetItemFromRepository)
        {
            try
            {
                Result<TSource> result = GetItemFromRepository(id);

                if (result.Success)
                {
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return Json(Mapper.Map<TDestination>(result.Value));
                }
                else
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(result.Error);
                }
        }
            catch (Exception ex)
            {
                _logger.LogError($"Une erreur s'est produite lors de la récupération du {typeof(TSource)} de l'examen {id}", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json($"Une erreur s'est produite lors de la récupération du {typeof(TSource)} de l'examen {id}");
    }
}

        private JsonResult SetItemById<TSource, TDestination>(string id,
                                                             TSource viewModel,
                                                             Func<string, TDestination, Result> SetItemToRepository)
            
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Map to the Entity
                    var model = Mapper.Map<TDestination>(viewModel);

                    //// Save to the Database
                    var result = SetItemToRepository(id, model);

                    if (result.Success && _repository.SaveAll().Success)
                    {
                        // Envoi SignalR
                        //if(model is IPointDifferentiel)
                        //    _eventPublisher.Publish(id, _repository.GetAvancementExamenById(id).Value);
                        //else if(model is Diagnostique)
                        //    _eventPublisher.Publish(id, _repository.GetDiagnostiqueById(id).Value);

                        Response.StatusCode = (int)HttpStatusCode.OK;
                        return Json(Mapper.Map<TSource>(model));
                    }
                    else
                    {
                        _logger.LogError($"Une erreur s'est produite lors de la mise à jour du {typeof(TDestination)} de l'examen {id}");
                        Response.StatusCode = (int)HttpStatusCode.NotModified;
                        return Json($"Une erreur s'est produite lors de la mise à jour du {typeof(TDestination)} de l'examen {id}");
                    }
                }
                else
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    string errorMessage = GetErrorMessages(ModelState.Root);
                    return Json(errorMessage);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Une erreur s'est produite lors de la mise à jour du {typeof(TDestination)} de l'examen {id}", ex);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json($"Une erreur s'est produite lors de la mise à jour du {typeof(TDestination)} de l'examen {id}");
            }
        }
        #endregion
        

        [HttpGet]
        public JsonResult GetGameInfoById(string gameId)
        {

            var result = _repository.GetGameById(gameId);
            if (result.Success)
            {
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json(Mapper.Map<GameInformationDto>(result.Value.GameInfo));
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(result.Error);
            }

        }

        [HttpGet]
        public JsonResult DoesThisGameExist(string gameId)
        {

            var result = _repository.GetGameById(gameId);
            if (result.Success)
            {
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json(true);
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json(false);
            }

        }

        [ValidateModel]
        public JsonResult CreateGameWithGrid([FromBody]GameDto vm)
        {

            // Map to the Entity
            var game = Mapper.Map<Game>(vm);
            var result = _repository.GetGameById(game.GameInfo.Id);
            if (result.Success)
            {
                _logger.LogError($"La partie ne peut pas être créée car son identifiant '{game.GameInfo.Id}' est déjà utilisé");
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Une erreur s'est produite lors de la sauvegarde du nouveau jeu");
            }
                    
            //Create
            var createResult = _repository.AddGame(game)
                                        .OnSuccess(() => _repository.SaveAll());

            if (createResult.Failure)
            {
                _logger.LogError($"Échec de la sauvegarde du nouveau jeu : {createResult.Error}");
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json($"Échec de la sauvegarde du nouveau jeu : {createResult.Error}");
            }

            Response.StatusCode = (int)HttpStatusCode.OK;
            return Json(Mapper.Map<GameDto>(game));
            

        }

        [ValidateModel]
        public JsonResult CreateGame([FromBody]GameInformationDto vm)
        {

            // Map to the Entity
            var info = Mapper.Map<GameInformation>(vm);
            var result = _repository.GetGameById(info.Id);
            if (result.Success)
            {
                _logger.LogError($"La partie ne peut pas être créée car son identifiant '{info.Id}' est déjà utilisé");
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Une erreur s'est produite lors de la sauvegarde du nouveau jeu");
            }

            //Create
            var createResult = _repository.AddGame(new Game(info))
                                        .OnSuccess(() => _repository.SaveAll());

            if (createResult.Failure)
            {
                _logger.LogError($"Échec de la sauvegarde du nouveau jeu : {createResult.Error}");
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json($"Échec de la sauvegarde du nouveau jeu : {createResult.Error}");
            }

            Response.StatusCode = (int)HttpStatusCode.OK;
            return Json(Mapper.Map<GameInformationDto>(info));

        }

        [ValidateModel]
        public JsonResult ApplyPlayerSelectionOnGame(
                    [FromBody]PlayerSelectionDto viewModel)
        {

            var resultGame = _repository.GetGameById(viewModel.GameId);
            var result = resultGame
                            .OnSuccess(() => resultGame.Value.ApplyPlayerSelection(viewModel.NomPlayer, viewModel.Line, viewModel.Column))
                            .OnSuccess(() => _repository.Update(resultGame.Value))
                            .OnSuccess(() => _repository.SaveAll());

            if (result.Success)
            {
                var boardDto = Mapper.Map<BoardDto>(resultGame.Value.Board);
                _eventPublisher.Publish<BoardDto>(resultGame.Value.Id, boardDto);
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json(boardDto);
            }
            else
            { 
                _logger.LogError(result.Error);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(result.Error);
            }         

        }

        [HttpGet]
        public JsonResult GetBoardById(string gameId)
        {

            var resultGame = _repository.GetGameById(gameId);
            if (resultGame.Success)
            {
                Response.StatusCode = (int)HttpStatusCode.OK;
                var boardDto = Mapper.Map<BoardDto>(resultGame.Value.Board);
                return Json(boardDto);
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(resultGame.Error);
            }

        }
        [HttpGet]
        public JsonResult GetAllGamesInfos()
        {
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json(Mapper.Map<GameInformationDto[]>(_repository.GetAllGamesInfos()));

        }
        

    }
}
   

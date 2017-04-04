using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

using Microsoft.Extensions.PlatformAbstractions;

namespace TicTacToe.Web.Controllers.Web
{
    public class AppController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Games()
        {
            return View();
        }

        public IActionResult Game()
        {
            return View("_Game");
        }

        public IActionResult Inscription()
        {
            return View("_Inscription");
        }

       
    }
}

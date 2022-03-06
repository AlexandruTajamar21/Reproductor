using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reproductor.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Media;

namespace Reproductor.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        SoundPlayer player = new SoundPlayer();
        string[] canciones = { "Canciones/Ejemplo.wav", "Canciones/Ejemplo2.wav" };
        int posicion = 0;


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            player = new SoundPlayer("Canciones/Ejemplo.wav");
            player.LoadAsync();
            player.PlayLooping();
            return View();
        }
        [HttpPost]
        public IActionResult Index(string accion)
        {
            if(accion == "parar")
            {
                this.player.Stop();
            }
            if(accion == "reanudar")
            {
                this.player = new SoundPlayer(this.canciones[this.posicion]);
                this.player.Play();
            }
            if(accion == "anterior" && this.posicion > 0)
            {
                this.posicion -= 1;
                this.player = new SoundPlayer(this.canciones[this.posicion]);
                player.LoadAsync();
                player.PlayLooping();
            }
            if (accion == "siguiente" && this.posicion< this.canciones.Length)
            {
                this.posicion += 1;
                this.player = new SoundPlayer(this.canciones[this.posicion]);
                player.LoadAsync();
                player.PlayLooping();
            }
            return View();
        }

        public IActionResult Privacy()
        {
            player = new SoundPlayer("Canciones/Ejemplo2.wav");
            player.LoadAsync();
            player.PlayLooping();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

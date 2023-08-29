using Microsoft.AspNetCore.Mvc;
using PreguntadORT.Models;

namespace PreguntadORT.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
      
      public IActionResult Puntajes()
    {
        ViewBag.ListaPuntajes = BD.ObtenerPuntajes();
        return View();
    }
    public IActionResult ConfigurarJuego()
    {
        Juego.InicializarJuego();
        ViewBag.Categoria = BD.ObtenerCategorias();
        ViewBag.Dificultad= BD.ObtenerDificultades();
        return View();
    }
    
    public IActionResult Comenzar(string username, int dificultad, int categoria)
    {
        Juego.CargarPartida(username, dificultad, categoria);
        Console.WriteLine(Juego.listaPreguntas.Count);

        if(Juego.listaPreguntas.Count != 0) 
        {
            Console.WriteLine(Juego.listaPreguntas.Count);
            return RedirectToAction("Jugar");
        }
        else
        {
            return RedirectToAction("ConfigurarJuego");
        }

    }
    public IActionResult Jugar()
    {
        if(Juego.listaPreguntas.Count == 0)
        {
            ViewBag.Username = Juego.ObtenerUsername();
            ViewBag.puntajeActual = Juego.ObtenerPuntaje();
            ViewBag.cantidadPreguntasCorrectas = Juego.ObtenerPreguntasCorrectas();
            BD.InsertarPuntajes(ViewBag.Username, ViewBag.puntajeActual, ViewBag.cantidadPreguntasCorrectas);
            ViewBag.ListaPuntajes = BD.ObtenerPuntajes();
            return View("Fin");
            
        }
        else
        {
            ViewBag.Username = Juego.ObtenerUsername();
            ViewBag.puntajeActual = Juego.ObtenerPuntaje();
            ViewBag.cantidadPreguntasCorrectas = Juego.ObtenerPreguntasCorrectas();
            ViewBag.Pregunta = Juego.ObtenerProximaPregunta();
            ViewBag.Respuesta = Juego.ObtenerProximaRespuesta(ViewBag.Pregunta.IdPregunta);
            return View("Juego");
        }
    }
    [HttpPost] public IActionResult VerificarRespuesta(int IdPregunta, int IdRespuesta)
    {
        Juego.VerificarRespuesta(IdPregunta,IdRespuesta);
        ViewBag.Correcto = Juego.VerificarRespuesta(IdPregunta, IdRespuesta);
        ViewBag.Username = Juego.ObtenerUsername();
        ViewBag.puntajeActual = Juego.ObtenerPuntaje();
        return View("Respuesta");
    }
}

using System;
using System.Collections.Generic;

public static class Juego
{
    private static string Username{get;set;}
    private static int puntajeActual{get;set;}
    private static int cantidadPreguntasCorrectas{get;set;}
    public static List<Pregunta> listaPreguntas{get;set;} = new List<Pregunta>();
    public static List<Respuesta> listaRespuestas{get;set;} = new List<Respuesta>();

    public static string ObtenerUsername()
    {
        return Username;
    }

    public static int ObtenerPuntaje()
    {
        return puntajeActual;
    }

    
    public static int ObtenerPreguntasCorrectas(){
            return cantidadPreguntasCorrectas;
        }

    public static void InicializarJuego()
    {
         Username = "";
         puntajeActual = 0;
         cantidadPreguntasCorrectas = 0;
     
    }
    public static List<Categoria> ListaCategorias()
    {
        return BD.ObtenerCategorias();
    } 
    public static  List<Dificultad> ListaDificultades()
    {
        return BD.ObtenerDificultades();
    }
    public static void CargarPartida(string username, int IdDificultad, int IdCategoria)
    {
        Username = username;
        listaPreguntas = BD.ObtenerPreguntas(IdDificultad, IdCategoria);
        listaRespuestas = BD.ObtenerRespuestas(listaPreguntas);
    }
    public static Pregunta ObtenerProximaPregunta()
    {
        Random rnd = new Random();
        int RNDMNUM = rnd.Next(0, listaPreguntas.Count);

        return listaPreguntas[RNDMNUM];
    }   
    public static List<Respuesta> ObtenerProximaRespuesta(int IdPregunta)
    {
        List<Respuesta> ProxRespuesta = new List<Respuesta>();
        foreach (var item in listaRespuestas)
        {
            if (item.IdPregunta == IdPregunta)
            {
                ProxRespuesta.Add(item);
            }
        }
        return ProxRespuesta;
    }
    public static bool VerificarRespuesta(int IdPregunta, int IdRespuesta)
    {
        bool algo = false;

        foreach (var item in listaRespuestas)
        {
            if(IdRespuesta == item.IdRespuesta && item.IdPregunta == IdPregunta && item.Correcta == true)
            {
                puntajeActual += 10;
                cantidadPreguntasCorrectas ++;
                algo = true;
            }
        }

        for (int j = 0; j < listaPreguntas.Count; j++) 
        {
            if (listaPreguntas[j].IdPregunta == IdPregunta)
            {
                listaPreguntas.RemoveAt(j);
                j--;

            }
        }
        
        return algo;
    }
}
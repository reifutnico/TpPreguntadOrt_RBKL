using System.Data.SqlClient;
using Dapper;

public class BD
{
private static string _connectionString = @"Server=DESKTOP-SEAVP9L\SQLEXPRESS; DataBase=PreguntadOrt;Trusted_Connection=True;";
 public static List<Categoria> ObtenerCategorias()
    {
        using(SqlConnection BD = new SqlConnection(_connectionString))
        {
            string SQL = "SELECT * FROM Categorias";
            return BD.Query<Categoria>(SQL).ToList();
        }
    }

    public static List<Dificultad> ObtenerDificultades()
    {
        using(SqlConnection BD = new SqlConnection(_connectionString))
        {
            string SQL = "SELECT * FROM Dificultades";
            return BD.Query<Dificultad>(SQL).ToList();
        }
    }

    public static List<Pregunta> ObtenerPreguntas(int idDificultad, int idCategoria)
    {
        using(SqlConnection BD = new SqlConnection(_connectionString))
        {
            string SQL = "SELECT * FROM Preguntas";
            if(idDificultad != -1 && idCategoria == -1)
            {
                SQL += " WHERE IdDificultad = @pIdDificultad";
            }
            else if(idDificultad == -1 && idCategoria != -1)
            {
                SQL += " WHERE IdCategoria = @pIdCategoria";
            }
            else if(idDificultad != -1 && idCategoria != -1)
            {
                SQL += " WHERE IdDificultad = @pIdDificultad AND IdCategoria = @pIdCategoria";
            }
            return BD.Query<Pregunta>(SQL, new{pIdDificultad = idDificultad, pIdCategoria = idCategoria}).ToList();
        }
    }

    public static List<Respuesta> ObtenerRespuestas(List<Pregunta> ListaPreguntas)
    {
        List<Respuesta> ListaRespuestas = new List<Respuesta>();

        using(SqlConnection BD = new SqlConnection(_connectionString))
        {
            string SQL = "SELECT * FROM Respuestas WHERE IdPregunta = @pIdPregunta";
            foreach (var item in ListaPreguntas)
            {
                ListaRespuestas.AddRange(BD.Query<Respuesta>(SQL, new{pIdPregunta = item.IdPregunta}).ToList());
            }
        }
        return ListaRespuestas;
    }
   
   
    public static void InsertarPuntajes(string username, int puntaje, int cantcorrectas){
        string sql = "INSERT INTO Puntajes(Username, FechaHora, Puntaje, CantCorrectas) VALUES(@pUsername, @pFechaHora, @pPuntaje, @pCantCorrectas)";
        
        using(SqlConnection db = new SqlConnection(_connectionString))
        {
            db.Execute(sql, new{pUsername = username, pFechaHora = DateTime.Now, pPuntaje = puntaje, pCantCorrectas = cantcorrectas});
        }
    }

    public static List<puntaje> ObtenerPuntajes(){
        using(SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT Top 10 * FROM Puntajes order by Puntaje desc";
            return db.Query<puntaje>(sql).ToList();
        }

    }


}
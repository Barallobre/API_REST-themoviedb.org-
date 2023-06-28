namespace Movies.Models;

public class MovieModel
{
    public string? Titulo { get; set; }
  
    public string? Titulo_original { get; set; }

    public decimal Nota_media { get; set; }
   
    public DateTime Fecha_estreno { get; set; }
   
    public string? Descripcion { get; set; }

    public List<SimilarMovieModel> Peliculas_similares { get; set; }

    public MovieModel()
    {
        Peliculas_similares = new List<SimilarMovieModel>();
    }
}

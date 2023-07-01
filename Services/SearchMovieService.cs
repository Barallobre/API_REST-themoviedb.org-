using Movies.Entities;
using Movies.Interfaces;
using Movies.Models;
using Serilog;
using System.Text.Json;

namespace Movies.Services
{
    public class SearchMovieService : ISearchMovieService
    {
        private readonly ISearchSimilarMoviesService _searchSimilarMoviesService;

        public SearchMovieService(ISearchSimilarMoviesService searchSimilarMoviesService)
        {
            _searchSimilarMoviesService = searchSimilarMoviesService;
        }
        public MovieModel Result(HttpResponseMessage result)
        {

            string moviesResult = result.Content.ReadAsStringAsync().Result;

            try
            {
                MoviesList? movies = JsonSerializer.Deserialize<MoviesList>(moviesResult);

                string resultSimilarMovies = _searchSimilarMoviesService.GetSimilarMovies(movies.results[0].id);

                MoviesList? moviesSimilar = JsonSerializer.Deserialize<MoviesList>(resultSimilarMovies);

                MovieModel movieModel = MapMovieSearch(movies.results, moviesSimilar?.results);

                return movieModel;
            }
            catch (Exception ex)
            {
                Log.Information($"ERROR -> Exception: {ex}");
            };
            return null;
        }

        private static MovieModel MapMovieSearch(List<Movie> movies, List<Movie> moviesSimilar)
        {
            List<string> peliculas = new List<string>();
            if (moviesSimilar != null && moviesSimilar.Count > 0)
            {
                for (int i = 0; i < Math.Min(5, moviesSimilar.Count); i++)
                {
                    string date = moviesSimilar[i].release_date.Length > 4 ? moviesSimilar[i].release_date.Substring(0, 4) : "";
                    string pelicula = string.Format($"{moviesSimilar[i].title} ({date})");
                    peliculas.Add(pelicula);
                }
            }
            string concat = string.Join(", ", peliculas);

            MovieModel movieModel = new MovieModel()
            {
                Titulo = movies[0].title,
                Titulo_original = movies[0].original_title,
                Nota_media = movies[0].vote_average,
                Fecha_estreno = movies[0].release_date != "" ? DateTime.Parse(movies[0].release_date).ToString("dd-MM-yyyy") : "",
                Descripcion = movies[0].overview,
                Peliculas_misma_tematica = concat
            };

            return movieModel;
        }
    }
}

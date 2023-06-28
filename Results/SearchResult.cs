﻿using Movies.Entities;
using Movies.Models;
using System.Text.Json;

namespace Movies.Results
{
    public class SearchResult
    {
        public static MovieModel Result(HttpResponseMessage result)
        {

            var moviesResult = result.Content.ReadAsStringAsync().Result;

            try
            {
                var movies = JsonSerializer.Deserialize<MoviesList>(moviesResult);

                MovieModel movieModel = mapMovieSearch(movies.results);

                return movieModel;

                //TOTO serilog
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
                //TODO implementar excepción
            };
            return null;
        }

        private static MovieModel mapMovieSearch(List<Movie> movies)
        {

            List<SimilarMovieModel> similarMovieModelList = new List<SimilarMovieModel>();

            if (movies != null)
            {
                for(int i = 1; i<=5; i++) 
                {
                    SimilarMovieModel similarMovieModel = new SimilarMovieModel();
                    similarMovieModel.Titulo = movies[i].title;
                    similarMovieModel.Fecha_estreno = movies[i].release_date.Year;
                    similarMovieModelList.Add(similarMovieModel);
                }
            }

            MovieModel movieModel = new MovieModel()
            {
                Titulo = movies[0].title,
                Titulo_original = movies[0].original_title,
                Nota_media = movies[0].vote_average,
                Fecha_estreno = movies[0].release_date,
                Descripcion = movies[0].overview,
                Peliculas_similares = similarMovieModelList
            };




            return movieModel;
        }
    }
}

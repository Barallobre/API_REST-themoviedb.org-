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
                var movies = JsonSerializer.Deserialize<MoviesModel>(moviesResult);
   
                Console.WriteLine(movies);

                return movies.results[0];
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);  
            };
            return null;
        }
    }
}

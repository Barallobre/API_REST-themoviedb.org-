using Serilog;
using System.Net.Http.Headers;
using System.Web;

namespace Movies.Services
{
    public class SimilarMovies
    {
        
        string api_key = "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJiNGQyZjYzYjNkZjk2ZjM1ZjU5NjQyMTFmODE3NzJjNiIsInN1YiI6IjY0OTliYTE3YjM0NDA5MDBjNTUwMzRjOSIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.UB0sOyYmelsU6_Rnn7QwiUbuCmUtq6BEiZmv8dBwMoc";

        public static string GetSimilarMovies(int id)
        {
            HttpResponseMessage result = null;
            using (var client = new HttpClient())
            {
                var uriBuilder = new UriBuilder($"https://api.themoviedb.org/3/movie/{id}/similar");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJiNGQyZjYzYjNkZjk2ZjM1ZjU5NjQyMTFmODE3NzJjNiIsInN1YiI6IjY0OTliYTE3YjM0NDA5MDBjNTUwMzRjOSIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.UB0sOyYmelsU6_Rnn7QwiUbuCmUtq6BEiZmv8dBwMoc");
                var parameters = HttpUtility.ParseQueryString(string.Empty);
                parameters["language"] = "es-ES";
                uriBuilder.Query = parameters.ToString();
                Uri finalUrl = uriBuilder.Uri;

                try
                {
                    Log.Information($"START Request -> URL: {finalUrl}");
                    result = client.GetAsync(finalUrl).Result;
                    Log.Information($"END Request -> StatusCode: {result.StatusCode}");
                }
                catch (Exception ex)
                {
                    Log.Information($"ERROR -> Excepction: {ex}");
                }

            }
            string resultMovies = result.Content.ReadAsStringAsync().Result;
            Log.Information($"MovieModel: {result}");

            return resultMovies;
        }
    }
}

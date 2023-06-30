using Microsoft.AspNetCore.Mvc;
using Movies.Models;
using Movies.Results;
using Serilog;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Web;

namespace Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public MoviesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet("{name}")]
        [Produces("application/json")]
        public MovieModel Get(string name)
        {
            var token = _configuration.GetSection("Settings")["token"];
            HttpResponseMessage result = null;
            using (var client = new HttpClient())
            {
                var uriBuilder = new UriBuilder("https://api.themoviedb.org/3/search/movie");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var parameters = HttpUtility.ParseQueryString(string.Empty);
                parameters["query"] = name;
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
            var content = SearchResult.Result(result);
            string jsonString = JsonSerializer.Serialize(content);
            Log.Information($"MovieModel: {jsonString}");
            
            return content;
        }
    }
}

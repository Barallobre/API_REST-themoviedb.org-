using Microsoft.AspNetCore.Mvc;
using Movies.Entities;
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
        string api_key = "b4d2f63b3df96f35f5964211f81772c6";

        [HttpGet("{name}")]
        [Produces("application/json")]
        public MovieModel Get(string name)
        {
            HttpResponseMessage result = null;
            using (var client = new HttpClient())
            {
                var uriBuilder = new UriBuilder("https://api.themoviedb.org/3/search/movie");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var parameters = HttpUtility.ParseQueryString(string.Empty);
                parameters["api_key"] = api_key;
                parameters["query"] = name;
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
            var member = JsonSerializer.Deserialize<MovieModel>(jsonString);
            
            return member;
        }
    }
}

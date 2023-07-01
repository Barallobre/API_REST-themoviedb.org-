using Microsoft.AspNetCore.Mvc;
using Movies.Interfaces;
using Movies.Models;
using Serilog;
using System.Text.Json;

namespace Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ISearchMovieService _searchResult;

        public MoviesController(ISearchMovieService searchResult)
        {
              _searchResult = searchResult;
        }

        [HttpGet("{name}")]
        [Produces("application/json")]
        public MovieModel Get(string name)
        {
            MovieModel? content = null; 
            try
            {
               var result = _searchResult.ApiCall(name);
               content = _searchResult.Result(result);
               string jsonString = JsonSerializer.Serialize(content);
               Log.Information($"MovieModel: {jsonString}");
            }
            catch (Exception ex)
            {
                Log.Information($"ERROR -> Excepction: {ex}");
            }
            
            return content;
        }
    }
}

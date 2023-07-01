using Movies.Models;

namespace Movies.Interfaces
{
    public interface ISearchMovieService
    {
        public HttpResponseMessage ApiCall(string name);
        public MovieModel Result(HttpResponseMessage result);
    }
}

using Movies.Models;

namespace Movies.Interfaces
{
    public interface ISearchMovieService
    {
        public MovieModel Result(HttpResponseMessage result);
    }
}

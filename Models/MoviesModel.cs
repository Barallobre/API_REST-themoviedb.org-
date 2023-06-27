
namespace Movies.Models
{
    public class MoviesModel
    {
        public long page { get; set; }
        
        public List<MovieModel> results { get; set; }
        
        public long total_pages { get; set; }
        
        public long total_results { get; set; }
        
        public MoviesModel() 
        {
            results = new List<MovieModel>();
        }

    }
}

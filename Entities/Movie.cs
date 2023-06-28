
using Movies.Results;

namespace Movies.Entities
{
    public class Movie
    {
        public string? title { get; set; }
      
        public string? original_title { get; set; }

        public decimal vote_average { get; set; }
       
        public DateTime release_date { get; set; }
       
        public string? overview { get; set; }

    }
}

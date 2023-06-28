namespace Movies.Entities
{
    public class MoviesList
    {
        public long page { get; set; }

        public List<Movie> results { get; set; }

        public long total_pages { get; set; }

        public long total_results { get; set; }

        public MoviesList()
        {
            results = new List<Movie>();
        }

    }
}

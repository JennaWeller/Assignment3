using System.Linq;
namespace Assignment3.Models
{
    //inherits from iMovieRepository
    public class EFMovieRepository : IMovieRepository
    {
        private MovieDbContext _context;

        public EFMovieRepository(MovieDbContext context)
        {
            _context = context;
        }
        public IQueryable<MovieResponse> Movies => _context.Movies;
    }
}

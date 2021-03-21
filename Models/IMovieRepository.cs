using System.Linq;
namespace Assignment3.Models
{
    public interface IMovieRepository
    {    //get the movie response form
        IQueryable<MovieResponse> Movies { get; }
    }
}
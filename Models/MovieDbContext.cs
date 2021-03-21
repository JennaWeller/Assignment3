using Microsoft.EntityFrameworkCore;
namespace Assignment3.Models
{
    //inherits from DbContext
    public class MovieDbContext : DbContext
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options)
        : base(options) { }
        public DbSet<MovieResponse> Movies { get; set; }
    }
}
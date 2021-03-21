using Assignment3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


namespace Assignment3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private MovieDbContext _context;
        private IMovieRepository _repository;

        public HomeController(ILogger<HomeController> logger, MovieDbContext context, IMovieRepository repository)
        {
            _logger = logger;
            _context = context;
            _repository = repository;
        }
        //calling the view for the index page 
        public IActionResult Index()
        {
            return View();
        }
        //calling the view for the podcast page
        public IActionResult MyPodcasts()
        {
            return View();
        }
        [HttpGet]
        public IActionResult MovieForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult MovieForm(MovieResponse movie)
        {
            if (ModelState.IsValid)
            {
                _context.Movies.Add(movie);
                _context.SaveChanges();
                return View("MovieList", _context.Movies);

            }

            return View("MovieForm");

        }
        public static int staticID;

        [HttpPost]
        public IActionResult EditMovie(int id)
        {
            staticID = id;
            return View("EditMovie", new MovieViewModel
            {
                MovieResponseModel = _context.Movies.Single(x => x.MovieId == staticID),
                ID = staticID
            });
        }

        [HttpPost]
        public IActionResult UpdateMovie(MovieViewModel model)
        {   //checking if the model state is valid
            if (ModelState.IsValid)
            {
                var movie = _context.Movies.Single(x => x.MovieId == staticID);
                _context.Entry(movie).Property(x => x.Category).CurrentValue = model.MovieResponseModel.Category;
                _context.Entry(movie).Property(x => x.Title).CurrentValue = model.MovieResponseModel.Title;
                _context.Entry(movie).Property(x => x.Year).CurrentValue = model.MovieResponseModel.Year;
                _context.Entry(movie).Property(x => x.Director).CurrentValue = model.MovieResponseModel.Director;
                _context.Entry(movie).Property(x => x.Rating).CurrentValue = model.MovieResponseModel.Rating;
                _context.Entry(movie).Property(x => x.Edited).CurrentValue = model.MovieResponseModel.Edited;
                _context.Entry(movie).Property(x => x.Lent).CurrentValue = model.MovieResponseModel.Lent;
                _context.Entry(movie).Property(x => x.Notes).CurrentValue = model.MovieResponseModel.Notes;
                _context.SaveChanges();
                //return to the page that shows the table of movies
                return RedirectToAction("MovieList");
            }
            else
            {   //otherwise return this view
                return View(new MovieViewModel
                {
                    MovieResponseModel = _context.Movies.Single(x => x.MovieId == staticID),
                    ID = staticID
                });
            }
        }
        //delete functionality
        public IActionResult DeleteMovie(int id)
        {
            _context.Remove(_context.Movies.Single(x => x.MovieId == id));
            _context.SaveChanges();
            return RedirectToAction("MovieList");
        }
        public IActionResult MovieList()
        {
            return View(_context.Movies);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

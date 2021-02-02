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

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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
        //get is diff than the post, for less secure info. just displays the movie form
        [HttpGet]
        public IActionResult MovieForm()
        {
            return View();
        }
        //post takes the info from the form and redirects it to the movie output page
        [HttpPost]
        public IActionResult MovieForm(MovieResponse movieResponse)
        {   //adding the values from the form into storage
            TempStorage.AddMovie(movieResponse);
            return View("MovieOutput", movieResponse);
        }

        public IActionResult MovieList()
        {   //printing the movie info from storage to the movie list page 
            return View(TempStorage.Movies.Where(r => r.Title != "Independence Day"));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

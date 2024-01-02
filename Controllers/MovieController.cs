using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using V3.Data;
using V3.Models;

namespace V3.Controllers
{
    public class MovieController : Controller
    {
    
        private ApplicationDbContext _context;

        public MovieController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        public ActionResult Random()
        {
            var Movies = _context.Movies.ToList();
            return View(Movies);
        }

       public ActionResult Details(int id)
        {
            var movies = _context.Movies.SingleOrDefault(m => m.ID == id);
      
            if (movies == null)
            {
                return NotFound();
            }

            return View(movies);
        }

       


        public ActionResult Index(int? Pageindex, String Sortby)
        {
            if (!Pageindex.HasValue)
                Pageindex = 1;

            if (string.IsNullOrWhiteSpace(Sortby))
                Sortby = "name";

            return Content (string.Format($"Pageindex={Pageindex}&Sortby = {Sortby}"));
        }


        //[Route("movies/Advanced/{year}/{month}")]
        public ActionResult Advanced(int year, int month)
        {
            return Content(year + "/" + month);
        }
    }
}
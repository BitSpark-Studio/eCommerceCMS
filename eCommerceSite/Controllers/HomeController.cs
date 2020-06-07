using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using eCommerceSite.Models;
using Microsoft.EntityFrameworkCore;
using eCommerceSite.Models.Cetagory;

namespace eCommerceSite.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly DatabaseContext _context;

        public HomeController( ILogger<HomeController> logger, DatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var category = await _context.Cetagorie.Select(x => new { x.Title, x.Photo }).ToListAsync();
            List<HomeCategoryViewModel> HomeCategory = new List<HomeCategoryViewModel>();
            if (category.Count == 0)
            {
                return View(HomeCategory);
            }
            for (int i = 0; i <category.Count; i++)
            {
                var CategoryList = new HomeCategoryViewModel()
                {
                    Category_Title = category[i].Title,
                    Category_Image = category[i].Photo
                };
                HomeCategory.Add(CategoryList);
            }
            return View(HomeCategory);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
          
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

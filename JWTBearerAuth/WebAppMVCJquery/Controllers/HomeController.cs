using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAppMVCJquery.ActionFilter;
using WebAppMVCJquery.Models;

namespace WebAppMVCJquery.Controllers
{
    [LogActionFilter]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {

            var list = new List<CheckModel>
            {
                 new CheckModel{Id = 1, Name = "Aquafina", Checked = false},
                 new CheckModel{Id = 2, Name = "Mulshi Springs", Checked = false},
                 new CheckModel{Id = 3, Name = "Alfa Blue", Checked = false},
                 new CheckModel{Id = 4, Name = "Atlas Premium", Checked = false},
                 new CheckModel{Id = 5, Name = "Bailley", Checked = false},
                 new CheckModel{Id = 6, Name = "Bisleri", Checked = false},
                 new CheckModel{Id = 7, Name = "Himalayan", Checked = false},
                 new CheckModel{Id = 8, Name = "Cool Valley", Checked = false},
                 new CheckModel{Id = 9, Name = "Dew Drops", Checked = false},
                 new CheckModel{Id = 10, Name = "Dislaren", Checked = false},

            };
            return View(list);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public ActionResult Index(IFormCollection fc, CheckModel m)
        {
            
            foreach(var item in fc.ToList())
            {
                //       if(item==true)
                //    {

                //    }
                //    else
                //    {

                //    }

            }
                return View();
        }
    }
}

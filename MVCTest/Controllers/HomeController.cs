using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCTest.Models;

namespace MVCTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Head | HttpVerbs.Post)]
        public ActionResult _Header(string title)
        {
            ViewBag.Title = title;

            var model = new HeaderViewModel();

            //check authentication
            model.UserViewModel = new UserViewModel
            {
                Id = 0,
                UserName = "iperez",
                First = "Ivan"
            };

            return View("_Header", model);
        }

    }


}
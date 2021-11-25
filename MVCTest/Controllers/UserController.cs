using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCTest.Models;
using OSM.Business.Interfaces;

namespace MVCTest.Controllers
{
    public class UserController : Controller
    {
        private readonly IUsersBusinessService _usersBusinessService;

        public UserController(IUsersBusinessService iUsersBusinessService)
        {
            _usersBusinessService = iUsersBusinessService;
        }
        [HttpGet]
        public ActionResult Index()
        {

            var usersListModelView = new UsersViewModel();

            usersListModelView.UsersList = _usersBusinessService.GetAll();
            return View("Index", usersListModelView);
        }

    }
}
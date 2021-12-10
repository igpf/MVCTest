using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCTest.Models;
using OSM.Business.Interfaces;
using OSM.Business.Services;
using OSM.Data;
using OSM.Data.Repositories;

namespace MVCTest.Controllers
{
    public class UserController : Controller
    {
        private readonly IUsersBusinessService _usersBusinessService;

        public UserController()
        {

            _usersBusinessService = new UsersBusinessService(new UnitOfWork(new OSMDatabaseContext()));
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Head)]
        public ActionResult Index()
        {

            var usersListModelView = new UsersViewModel();

            usersListModelView.UsersList = _usersBusinessService.GetAll();
            return View(usersListModelView);
        }

    }
}
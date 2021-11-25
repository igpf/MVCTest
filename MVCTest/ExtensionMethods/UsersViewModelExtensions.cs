using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCTest.Models;
using OSM.Data.Entities;

namespace MVCTest.ExtensionMethods
{
    public static class UsersViewModelExtensions
    {
        public static UsersViewModel ToUsersViewModel(List<Users> usersList)
        {
            var vm = new UsersViewModel();
            
            vm.UsersList = usersList;

            return vm;
        }
    }
}
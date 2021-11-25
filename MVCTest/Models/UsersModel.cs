using System.Collections.Generic;
using OSM.Data.Entities;

namespace MVCTest.Models
{
    public class UsersViewModel

    {
        public List<Users> UsersList;

    }

    public class UserViewModel
    {
        public int Id { get; set; }
        public string First { get; set; }
        public string Last { get; set; }

        public string UserName { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM.Data.Entities
{
    public class Users
    {
        public int  Id { get; set; }
        
        public string UserName { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Email { get; set; }
        
        public string Phone { get; set; }
        
        public string Passwd { get; set; }
        
        public bool isActive { get; set; }
        
        public bool isAdmin { get; set; }
    }
}

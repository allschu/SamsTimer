using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared
{
    public class Member
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        private string Address { get; set; }
        private string Zipcode { get; set; }
        private string City { get; set; }
        private string Phone { get; set; }
        private DateTime DateOfBirth { get; set; }
        private string Email { get; set; }
    }
}

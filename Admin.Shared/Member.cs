using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared
{
    public class Member : BaseEntity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        private string Address { get; set; }
        private string Zipcode { get; set; }
        private string City { get; set; }
        private string Phone { get; set; }
        private DateTime DateOfBirth { get; set; }
    }
}

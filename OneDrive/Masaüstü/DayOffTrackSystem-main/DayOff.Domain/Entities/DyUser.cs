using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Domain.Entities
{
    public class DyUser
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TcNo { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string Neighborhood { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        public string District { get; set; }
        public string City { get; set; }

        public int? GenderId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime EmploymentDate { get; set; }
        public bool IsActive { get; set; }

        public int RoleId { get; set; }
        public int? DepartmentId { get; set; }
        public int? TitleId { get; set; }
    }
}

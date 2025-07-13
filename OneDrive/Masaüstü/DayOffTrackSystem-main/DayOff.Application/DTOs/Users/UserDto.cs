using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.DTOs.Users
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string TcNo { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; }

        public string? Neighborhood { get; set; }
        public string? Street { get; set; }
        public string? Building { get; set; }
        public string? District { get; set; }
        public string? City { get; set; }


        public int? GenderId { get; set; }
        public string? GenderName { get; set; } //JOIN

        public DateTime? DateOfBirth { get; set; }
        public DateTime EmploymentDate { get; set; }
        public bool IsActive { get; set; }

        public int RoleId { get; set; }
        public string? RoleName { get; set; } //JOIN

        public int? DepartmentId { get; set; }
        public string? DepartmentName { get; set; } 

        public int? TitleId { get; set; }
        public string? TitleName { get; set; } 
    }
}

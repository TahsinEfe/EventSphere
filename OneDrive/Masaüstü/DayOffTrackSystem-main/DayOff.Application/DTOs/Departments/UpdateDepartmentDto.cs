﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.DTOs.Departments
{
    public class UpdateDepartmentDto
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = null!; 
    }
}

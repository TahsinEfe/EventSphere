using AutoMapper;
using DayOff.Application.DTOs.DayOffTypes;
using DayOff.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Mappings
{
    public class DayOffTypeProfile : Profile
    {
        public DayOffTypeProfile()
        {
            CreateMap<DyDayOffType, DayOffTypeDto>().ReverseMap();
        }
    }
}

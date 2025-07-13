using DayOff.Application.DTOs.Holidays;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.Holidays.Commands
{
    public class CreateHolidayCommand : IRequest<int>
    {
        public CreateHolidayDto CreateDto { get; set; }

        public CreateHolidayCommand(CreateHolidayDto dto)
        {
            CreateDto = dto;
        }
    }
}

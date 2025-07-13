using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.Holidays.Commands
{
    public class DeleteHolidayCommand : IRequest<bool>
    {
        public int HolidayId { get; set; }

        public DeleteHolidayCommand(int holidayId)
        {
            HolidayId = holidayId;
        }
    }
}

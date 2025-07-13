using DayOff.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.Holidays.Commands
{
    public class DeleteHolidayCommandHandler : IRequestHandler<DeleteHolidayCommand, bool>
    {
        private readonly IHolidayService _holidayService;
    
        public async Task<bool> Handle(DeleteHolidayCommand request, CancellationToken cancellationToken)
        {
            return await _holidayService.DeleteAsync(request.HolidayId);
        }
    }
}

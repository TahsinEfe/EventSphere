using DayOff.Application.DTOs.DayOffTypes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.DayOffTypes.Queries
{
    public class GetAllDayOffTypesQuery : IRequest<List<DayOffTypeDto>>
    {
    }
}

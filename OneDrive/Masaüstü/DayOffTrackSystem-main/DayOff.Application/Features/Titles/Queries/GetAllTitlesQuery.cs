using DayOff.Application.DTOs.Titles;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.Titles.Queries
{
    public class GetAllTitlesQuery : IRequest<List<TitleDto>>
    {
    }
}

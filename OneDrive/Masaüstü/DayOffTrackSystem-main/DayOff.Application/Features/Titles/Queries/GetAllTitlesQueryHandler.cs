using DayOff.Application.DTOs.Titles;
using DayOff.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.Titles.Queries
{
    public class GetAllTitlesQueryHandler : IRequestHandler<GetAllTitlesQuery, List<TitleDto>>
    {
        private readonly ITitleService _titleService;

        public GetAllTitlesQueryHandler(ITitleService titleService)
        {
            _titleService = titleService;
        }

        public async Task<List<TitleDto>> Handle(GetAllTitlesQuery request, CancellationToken cancellationToken)
        {
            var titles = await _titleService.GetAllAsync();

            return titles.Select(t => new TitleDto
            {
                TitleId = t.TitleId,
                TitleName = t.TitleName
            }).ToList();
        }
    }
}

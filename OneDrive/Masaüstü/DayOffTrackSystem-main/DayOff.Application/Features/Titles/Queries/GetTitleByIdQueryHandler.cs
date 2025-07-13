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
    public class GetTitleByIdQueryHandler : IRequestHandler<GetTitleByIdQuery, TitleDto?>
    {
        private readonly ITitleService _titleService;

        public GetTitleByIdQueryHandler(ITitleService titleService)
        {
            _titleService = titleService;
        }

        public async Task<TitleDto?> Handle(GetTitleByIdQuery request, CancellationToken cancellationToken)
        {
            var title = await _titleService.GetByIdAsync(request.TitleId);
            if (title == null) return null;

            return new TitleDto
            {
                TitleId = title.TitleId,
                TitleName = title.TitleName
            };
        }
    }
}
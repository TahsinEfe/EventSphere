using DayOff.Application.Interfaces.Services;
using DayOff.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.Titles.Commands
{
    public class UpdateTitleCommandHandler : IRequestHandler<UpdateTitleCommand, bool>
    {
        private readonly ITitleService _titleService;

        public UpdateTitleCommandHandler(ITitleService titleService)
        {
            _titleService = titleService;
        }

        public async Task<bool> Handle(UpdateTitleCommand request, CancellationToken cancellationToken)
        {
            var entity = new DyTitle
            {
                TitleId = request.TitleId,
                TitleName = request.TitleName
            };

            return await _titleService.UpdateAsync(entity);
        }
    }
}

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
    public class CreateTitleCommandHandler : IRequestHandler<CreateTitleCommand, int>
    {
        private readonly ITitleService _titleService;
    
        public CreateTitleCommandHandler(ITitleService titleService)
        {
            _titleService = titleService;
        }

        public async Task<int> Handle(CreateTitleCommand request, CancellationToken cancellationToken)
        {
            var entity = new DyTitle
            {
                TitleName = request.TitleName
            };

            return await _titleService.CreateAsync(entity);
        }

    }
}

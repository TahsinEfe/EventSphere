using DayOff.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.Titles.Commands
{
    public class DeleteTitleCommandHandler : IRequestHandler<DeleteTitleCommand, bool>
    {
        private readonly ITitleService _titleService;

        public DeleteTitleCommandHandler(ITitleService titleService)
        {
            _titleService = titleService;
        }

        public async Task<bool> Handle(DeleteTitleCommand request, CancellationToken cancellationToken)
        {
            return await _titleService.DeleteAsync(request.TitleId);
        }
    }
}

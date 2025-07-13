using DayOff.Application.Interfaces.Services;
using DayOff.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.DayOffTypes.Commands
{
    internal class UpdateDayOffTypeCommandHandler : IRequestHandler<UpdateDayOffTypeCommand, bool>
    {
        private readonly IDayOffTypeService _typeService;

        public UpdateDayOffTypeCommandHandler(IDayOffTypeService typeService)
        {
            _typeService = typeService;
        }

        public async Task<bool> Handle(UpdateDayOffTypeCommand request, CancellationToken cancellationToken)
        {
            var dto = request.UpdateDto;

            var entity = new DyDayOffType
            {
                DyOffId = dto.DyOffId,
                DyOffName = dto.DyOffName,
                IsGenderSpecific = dto.IsGenderSpecific,
                AllowedGenderId = dto.AllowedGenderId,
                IsPartialAllowed = dto.IsPartialAllowed
            };

            return await _typeService.UpdateAsync(entity);
        }
    }
}
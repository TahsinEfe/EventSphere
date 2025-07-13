using DayOff.Application.DTOs.DayOffTypes;
using DayOff.Application.Interfaces.Services;
using DayOff.Domain.Entities;
using MediatR;

namespace DayOff.Application.Features.DayOffTypes.Commands
{
    public class CreateDayOffTypeCommandHandler : IRequestHandler<CreateDayOffTypeCommand, int>
    {
        private readonly IDayOffTypeService _typeService;

        public CreateDayOffTypeCommandHandler(IDayOffTypeService typeService)
        {
            _typeService = typeService;
        }

        public async Task<int> Handle(CreateDayOffTypeCommand request, CancellationToken cancellationToken)
        {
            var dto = request.CreateDto;

            var entity = new DyDayOffType
            {
                DyOffName = dto.DyOffName,
                IsGenderSpecific = dto.IsGenderSpecific,
                AllowedGenderId = dto.AllowedGenderId,
                IsPartialAllowed = dto.IsPartialAllowed
            };

            return await _typeService.CreateAsync(entity);
        }
    }
}

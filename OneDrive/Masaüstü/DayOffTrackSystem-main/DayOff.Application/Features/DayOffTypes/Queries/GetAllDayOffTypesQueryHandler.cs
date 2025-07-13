using AutoMapper;
using DayOff.Application.DTOs.DayOffTypes;
using DayOff.Application.Features.DayOffTypes.Queries;
using DayOff.Application.Interfaces;
using DayOff.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Handlers.DayOffTypes
{
    public class GetAllDayOffTypesQueryHandler : IRequestHandler<GetAllDayOffTypesQuery, List<DayOffTypeDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllDayOffTypesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<DayOffTypeDto>> Handle(GetAllDayOffTypesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _unitOfWork.DayOffTypes.GetAllAsync();

            // DEBUG: Buraya log at
            Console.WriteLine($"[DEBUG] DB'den gelen kayıt sayısı: {entities.Count()}");

            return _mapper.Map<List<DayOffTypeDto>>(entities);
        }
    }
}
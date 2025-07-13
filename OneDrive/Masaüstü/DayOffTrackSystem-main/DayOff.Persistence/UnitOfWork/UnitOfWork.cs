using DayOff.Application.Interfaces;
using DayOff.Application.Interfaces.Repositories;
using DayOff.Domain.Entities;
using DayOff.Persistence.Context;
using DayOff.Persistence.Entities;
using DayOff.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DayOffDbContext _context;

        public IGenericRepository<Domain.Entities.DyUser> Users { get; }
        public IGenericRepository<Domain.Entities.DyRole> Roles { get; }
        public IGenericRepository<Domain.Entities.DyDepartment> Departments { get; }
        public IGenericRepository<Domain.Entities.DyTitle> Titles { get; }
        public IGenericRepository<Domain.Entities.DyGender> Genders { get; }


        public IGenericRepository<Domain.Entities.DyDayOffRequest> DayOffRequests { get; }
        public IGenericRepository<Domain.Entities.DyDayOffBalance> DayOffBalances { get; }
        public IGenericRepository<Domain.Entities.DyDayOffPolicy> DayOffPolicies { get; }
        public IDayOffTypeRepository DayOffTypes { get; }
        public IGenericRepository<Domain.Entities.DyDayOffHistory> DayOffHistories { get; }


        public IGenericRepository<Domain.Entities.DyNotification> Notifications { get; }
        public IGenericRepository<Domain.Entities.DyHoliday> Holidays { get; }


        public IGenericRepository<Domain.Entities.VwWeeklyDayOffStat> WeeklyDayOffStats { get; }


        public UnitOfWork(DayOffDbContext context)
        {
            _context = context;

            Users = new GenericRepository<Domain.Entities.DyUser>(_context);
            Roles = new GenericRepository<Domain.Entities.DyRole>(_context);
            Departments = new GenericRepository<Domain.Entities.DyDepartment>(_context);
            Titles = new GenericRepository<Domain.Entities.DyTitle>(_context);
            Genders = new GenericRepository<Domain.Entities.DyGender>(_context);

            DayOffRequests = new GenericRepository<Domain.Entities.DyDayOffRequest>(_context);
            DayOffBalances = new GenericRepository<Domain.Entities.DyDayOffBalance>(_context);
            DayOffPolicies = new GenericRepository<Domain.Entities.DyDayOffPolicy>(_context);
            DayOffTypes = new DayOffTypeRepository(_context);
            DayOffHistories = new GenericRepository<Domain.Entities.DyDayOffHistory>(_context);

            Notifications = new GenericRepository<Domain.Entities.DyNotification>(_context);
            Holidays = new GenericRepository<Domain.Entities.DyHoliday>(_context);

            WeeklyDayOffStats = new GenericRepository<Domain.Entities.VwWeeklyDayOffStat>(_context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();

        }
    }
}

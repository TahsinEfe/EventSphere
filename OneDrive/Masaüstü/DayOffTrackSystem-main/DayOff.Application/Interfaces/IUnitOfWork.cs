using DayOff.Application.Interfaces.Repositories;
using DayOff.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace DayOff.Application.Interfaces
{
    public interface IUnitOfWork  : IDisposable
    {
        IGenericRepository<DyUser> Users { get; }
        IGenericRepository<DyRole> Roles { get; }
        IGenericRepository<DyDepartment> Departments { get; }
        IGenericRepository<DyTitle> Titles { get; }
        IGenericRepository<DyGender> Genders { get; }


        IGenericRepository<DyDayOffRequest> DayOffRequests { get; }
        IGenericRepository<DyDayOffBalance> DayOffBalances { get; }
        IGenericRepository<DyDayOffPolicy> DayOffPolicies { get; }
        IDayOffTypeRepository DayOffTypes { get; }
        IGenericRepository<DyDayOffHistory> DayOffHistories { get; }


        IGenericRepository<DyNotification> Notifications { get; }
        IGenericRepository<DyHoliday> Holidays { get; }


        IGenericRepository<VwWeeklyDayOffStat> WeeklyDayOffStats { get; }


        Task<int> SaveChangesAsync();
    }
}

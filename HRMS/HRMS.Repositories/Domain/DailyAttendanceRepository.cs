using HRMS.Domain.DAO;
using HRMS.Domain.Models.DataModels;
using HRMS.Repositories.Common;

namespace HRMS.Repositories.Domain
{
    public class DailyAttendanceRepository : BaseRepository<DailyAttendanceEntity>, IDailyAttendanceRepository
    {
        private readonly HRMSWebDbContext _dbcontext;

        public DailyAttendanceRepository(HRMSWebDbContext dbcontext) : base(dbcontext)
        {
            this._dbcontext = dbcontext;
        }
    }
}

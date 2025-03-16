using HRMS.Web.DAO;
using HRMS.Web.Models.DataModels;
using HRMS.Web.Repositories.Common;
using System.Linq.Expressions;

namespace HRMS.Web.Repositories.Domain
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

using HRMS.Web.DAO;
using HRMS.Web.Models.DataModels;
using HRMS.Web.Repositories.Common;

namespace HRMS.Web.Repositories.Domain
{
    public class AttendanceMasterRepository : BaseRepository<AttendanceMasterEntity>, IAttendanceMasterRepository
    {
        private readonly HRMSWebDbContext _dbcontext;

        public AttendanceMasterRepository(HRMSWebDbContext dbcontext) : base(dbcontext)
        {
            this._dbcontext = dbcontext;
        }
    }
}

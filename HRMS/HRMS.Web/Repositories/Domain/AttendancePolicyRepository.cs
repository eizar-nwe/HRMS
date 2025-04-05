using HRMS.Web.DAO;
using HRMS.Web.Models.DataModels;
using HRMS.Web.Repositories.Common;

namespace HRMS.Web.Repositories.Domain
{
    public class AttendancePolicyRepository : BaseRepository<AttendancePolicyEntity>, IAttendancePolicyRepository
    {
        private readonly HRMSWebDbContext _dbcontext;

        public AttendancePolicyRepository(HRMSWebDbContext dbcontext) : base(dbcontext)
        {
            this._dbcontext = dbcontext;
        }
    }
}

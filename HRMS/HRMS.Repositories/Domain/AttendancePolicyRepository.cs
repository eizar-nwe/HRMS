using HRMS.Domain.DAO;
using HRMS.Domain.Models.DataModels;
using HRMS.Repositories.Common;

namespace HRMS.Repositories.Domain
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

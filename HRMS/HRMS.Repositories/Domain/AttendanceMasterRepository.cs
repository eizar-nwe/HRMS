using HRMS.Domain.DAO;
using HRMS.Domain.Models.DataModels;
using HRMS.Repositories.Common;

namespace HRMS.Repositories.Domain
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

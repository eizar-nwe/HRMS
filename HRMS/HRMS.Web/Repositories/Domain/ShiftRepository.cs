using HRMS.Web.DAO;
using HRMS.Web.Models.DataModels;
using HRMS.Web.Repositories.Common;

namespace HRMS.Web.Repositories.Domain
{
    public class ShiftRepository : BaseRepository<ShiftEntity>, IShiftRepository
    {
        private readonly HRMSWebDbContext _dbcontext;

        public ShiftRepository(HRMSWebDbContext dbcontext) : base(dbcontext)
        {
            this._dbcontext = dbcontext;
        }
    }
}

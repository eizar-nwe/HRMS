using HRMS.Domain.DAO;
using HRMS.Domain.Models.DataModels;
using HRMS.Repositories.Common;

namespace HRMS.Repositories.Domain
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

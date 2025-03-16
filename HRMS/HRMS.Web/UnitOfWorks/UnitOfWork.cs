using HRMS.Web.DAO;
using HRMS.Web.Repositories.Domain;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Web.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HRMSWebDbContext _dbContext;

        public UnitOfWork(HRMSWebDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        private IPositionRepository positoryRepository;
        public IPositionRepository PositionRepository
        {
            get
            {
                return positoryRepository = positoryRepository ?? new PositonRepository(_dbContext);
            }
        }

        private IDepartmentRespository departmentRespository;
        public IDepartmentRespository DepartmentRespository
        {
            get
            {
                return departmentRespository = departmentRespository ?? new DepartmentRepository(_dbContext);
            }
        }

        private IEmployeeRepository employeeRepositiry;
        public IEmployeeRepository EmployeeRespository => employeeRepositiry ?? new EmployeeRepositiry(_dbContext);

        private IDailyAttendanceRepository dailyAttendanceRepository;
        public IDailyAttendanceRepository DailyAttendanceRepository => dailyAttendanceRepository ?? new DailyAttendanceRepository(_dbContext);

        public void Commit() => _dbContext.SaveChanges();        

        public void Rollback()
        {
            _dbContext.Dispose();
        }
    }
}

using HRMS.Domain.DAO;
using HRMS.Repositories.Domain;
using Microsoft.EntityFrameworkCore;

namespace HRMS.UnitOfWorks
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

        private IDepartmentRepository departmentRespository;
        public IDepartmentRepository DepartmentRespository
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
        private IAttendancePolicyRepository attendancePolicyRepository;
        public IAttendancePolicyRepository AttendancePolicyRepository
        {
            get
            {
                return attendancePolicyRepository = attendancePolicyRepository?? new AttendancePolicyRepository(_dbContext);
            }
        }
        private IAttendanceMasterRepository attendanceMasterRepository;
        public IAttendanceMasterRepository AttendanceMasterRepository => attendanceMasterRepository?? new AttendanceMasterRepository(_dbContext);
        
        private IShiftRepository shiftRepository;
        public IShiftRepository ShiftRepository => shiftRepository?? new ShiftRepository(_dbContext);

        private IPayrollRepository payrollRepository;
        public IPayrollRepository PayrollRepository => payrollRepository?? new PayrollRepository(_dbContext);

        private IShiftAssignRepository shiftAssignRepository;
        public IShiftAssignRepository ShiftAssignRepository => shiftAssignRepository?? new ShiftAssignRepository(_dbContext);

        public void Commit() => _dbContext.SaveChanges();        

        public void Rollback()
        {
            _dbContext.Dispose();
        }
    }
}

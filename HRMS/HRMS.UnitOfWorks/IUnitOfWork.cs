using HRMS.Repositories.Domain;

namespace HRMS.UnitOfWorks
{
    public interface IUnitOfWork
    {
        void Commit();
        void Rollback();
        IPositionRepository PositionRepository { get; }
        IDepartmentRepository DepartmentRespository { get; }
        IEmployeeRepository EmployeeRespository { get; }
        IDailyAttendanceRepository DailyAttendanceRepository { get; }
        IAttendancePolicyRepository AttendancePolicyRepository { get; }
        IAttendanceMasterRepository AttendanceMasterRepository { get; }        
        IShiftRepository ShiftRepository { get; }
        IPayrollRepository PayrollRepository { get; }
        IShiftAssignRepository ShiftAssignRepository { get; }
    }
}

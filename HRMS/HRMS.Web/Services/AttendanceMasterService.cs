using HRMS.Web.Models.ViewModels;
using HRMS.Web.UnitOfWorks;

namespace HRMS.Web.Services
{
    public class AttendanceMasterService : IAttendanceMasterService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AttendanceMasterService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public void DayEndProcess(AttendanceMasterViewModel attMstrVM)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttendanceMasterViewModel> GetAll()
        {
            return (from a in _unitOfWork.AttendanceMasterRepository.GetAll(w => w.IsActive)
                    join e in _unitOfWork.EmployeeRespository.GetAll(w=>w.IsActive)
                    on a.EmployeeId equals e.Id
                    join d in _unitOfWork.DepartmentRespository.GetAll(w=>w.IsActive)
                    on a.DepartmentId equals d.Id
                    join s in _unitOfWork.ShiftRepository.GetAll(w=>w.IsActive)
                    on a.ShiftId equals s.Id

                    select new AttendanceMasterViewModel
                    {
                        Id = a.Id,
                        AttendanceDate = a.AttendanceDate,
                        InTime = s.InTime,
                        OutTime = s.OutTime,
                        EmployeeId = a.EmployeeId,
                        DepartmentId = a.DepartmentId,
                        ShiftId = a.ShiftId,
                        IsLate = a.IsLate,
                        IsEarlyOut = a.IsEarlyOut,
                        IsLeave = a.IsLeave,
                        EmployeeInfo=e.Code +"/"+ e.Name,
                        DepartmentInfo=d.Code +"/"+ d.Description,
                        ShiftInfo=s.Name
                    }).ToList();
        }
    }
}

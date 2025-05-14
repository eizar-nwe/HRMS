using HRMS.Domain.Models.DataModels;
using HRMS.Domain.Models.ViewModels;
using HRMS.Domain.Utilities;
using HRMS.UnitOfWorks;

namespace HRMS.Services
{
    public class AttendanceMasterService : IAttendanceMasterService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AttendanceMasterService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<string> GetIpAsync()
        {
            return await NetworkHelper.GetIpAddressAsnyc();
        }
        public void DayEndProcess(AttendanceMasterViewModel attMstrVM)
        {
            try
            {
                //Data exchange from view model to data model by using automapper
                List<AttendanceMasterEntity> attendanceMasters = new List<AttendanceMasterEntity>();

                //get the daily atttendance accroding to assigned shift on that date with LINQ query
                var DailyAttendanceWithShiftAssignData = (from da in _unitOfWork.DailyAttendanceRepository.GetAll(w => w.IsActive)
                                                          join sa in _unitOfWork.ShiftAssignRepository.GetAll(w => w.IsActive)
                                                          on da.EmployeeId equals sa.EmployeeId
                                                          where sa.EmployeeId == attMstrVM.EmployeeId &&
                                                          (attMstrVM.AttendanceDate >= sa.FromDate && sa.FromDate <= attMstrVM.ToDate)

                                                          select new
                                                          {
                                                              dailyAttendance = da,
                                                              shiftAssign = sa
                                                          }).ToList();

                // check  he/she IsLate, IsEarlyOut, IsLeave
                foreach (var data in DailyAttendanceWithShiftAssignData)
                {
                    ShiftEntity definedShift = _unitOfWork.ShiftRepository.GetBy(w => w.Id == data.shiftAssign.ShiftId).SingleOrDefault();

                    if (definedShift is not null)
                    {
                        AttendanceMasterEntity attendanceMaster = new AttendanceMasterEntity()
                        {
                            Id = Guid.NewGuid().ToString(),
                            IsLeave = false,
                            InTime = data.dailyAttendance.InTime,
                            OutTime = data.dailyAttendance.OutTime,
                            EmployeeId = data.shiftAssign.EmployeeId,
                            ShiftId = definedShift.Id,
                            DepartmentId = data.dailyAttendance.DepartmentId,
                            AttendanceDate = data.dailyAttendance.AttendanceDate,
                            CreatedBy = "system",
                            CreatedAt = DateTime.Now,
                            IsActive = true,
                            Ip = GetIpAsync().Result
                        };

                        //checking out the late status
                        if (data.dailyAttendance.InTime > definedShift.LateAfter)
                        {//10:00>09:15
                            attendanceMaster.IsLate = true;
                        }
                        else
                        {
                            attendanceMaster.IsLate = false;
                        }
                        //checking out the early out status
                        if (data.dailyAttendance.OutTime < definedShift.EarlyOutBefore)
                        {// 17:44 < 17:45
                            attendanceMaster.IsEarlyOut = true;
                        }
                        else
                        {
                            attendanceMaster.IsEarlyOut = false;
                        }
                        attendanceMasters.Add(attendanceMaster);                        
                    }//end of the deifned shift not null
                }
                //insert the attendance data to the attendance master table from this raw data ( DailyAttendance )
                _unitOfWork.AttendanceMasterRepository.Create(attendanceMasters);
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
            }
        }

        public void Delete(DateTime frDate, DateTime toDate, string departmentId, string employeeId)
        {
            try
            {
                IEnumerable<AttendanceMasterEntity> collectAttendanceMaster = null;
                if(departmentId is not null)
                {
                    collectAttendanceMaster = _unitOfWork.AttendanceMasterRepository.GetAll(w => (w.AttendanceDate.Date >= frDate.Date && w.AttendanceDate.Date <= toDate.Date) && w.DepartmentId == departmentId).ToList();
                }
                else if (employeeId is not null)
                {
                    collectAttendanceMaster = _unitOfWork.AttendanceMasterRepository.GetAll(w => (w.AttendanceDate.Date >= frDate.Date && w.AttendanceDate.Date <= toDate.Date) && w.EmployeeId == employeeId).ToList();
                }
                else
                {
                    collectAttendanceMaster = _unitOfWork.AttendanceMasterRepository.GetAll(w => (w.AttendanceDate.Date >= frDate.Date && w.AttendanceDate.Date <= toDate.Date) && w.DepartmentId == departmentId && w.EmployeeId == employeeId).ToList();
                }

                foreach (var attendance in collectAttendanceMaster)
                {
                    _unitOfWork.AttendanceMasterRepository.Delete(attendance, true);
                    _unitOfWork.Commit();
                }
            }
	        catch (System.Exception)
            {
                _unitOfWork.Rollback();
            }
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

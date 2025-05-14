using HRMS.Domain.Models.DataModels;
using HRMS.Domain.Models.ViewModels;
using HRMS.UnitOfWorks;
using HRMS.Domain.Utilities;

namespace HRMS.Services
{
    public class DailyAttendanceService : IDailyAttendanceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DailyAttendanceService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<string> GetIpAsync()
        {
            return await NetworkHelper.GetIpAddressAsnyc();
        }
        public void Create(DailyAttendanceViewModel DailyAttendVM)
        {
            try
            {
                DailyAttendanceEntity DailyAttEntity = new DailyAttendanceEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    AttendanceDate = DailyAttendVM.AttendanceDate,
                    InTime = DailyAttendVM.InTime,
                    OutTime = DailyAttendVM.OutTime,
                    DepartmentId = DailyAttendVM.DepartmentId,
                    EmployeeId = DailyAttendVM.EmployeeId,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "system",
                    IsActive = true,
                    Ip = GetIpAsync().Result
                };
               _unitOfWork.DailyAttendanceRepository.Create(DailyAttEntity);
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
            }
        }

        public void Delete(string id)
        {
            try
            {
                DailyAttendanceEntity DailyAttEntity = _unitOfWork.DailyAttendanceRepository.GetAll(w => w.IsActive && w.Id == id).FirstOrDefault();
                if (DailyAttEntity is not null)
                {
                    DailyAttEntity.IsActive = false;
                    _unitOfWork.DailyAttendanceRepository.Delete(DailyAttEntity);
                    _unitOfWork.Commit();
                }
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
            }
        }

        public IEnumerable<DailyAttendanceViewModel> GetAll()
        {
            return (from a in _unitOfWork.DailyAttendanceRepository.GetAll(w=>w.IsActive)
                        join e in _unitOfWork.EmployeeRespository.GetAll(w=>w.IsActive)
                        on a.EmployeeId equals e.Id
                        join d in _unitOfWork.DepartmentRespository.GetAll(w => w.IsActive)
                        on a.DepartmentId equals d.Id
                        where a.IsActive && e.IsActive && d.IsActive

                        select new DailyAttendanceViewModel
                        {
                            Id = a.Id,
                            AttendanceDate = a.AttendanceDate,
                            InTime = a.InTime,
                            OutTime = a.OutTime,
                            EmployeeInfo = e.Code + "/" + e.Name,
                            DepartmentInfo = d.Code + "/" + d.Description
                        }).ToList();            
        }

        public DailyAttendanceViewModel GetById(string id)
        {
            return _unitOfWork.DailyAttendanceRepository.GetAll(w => w.IsActive && w.Id == id).Select(s => new DailyAttendanceViewModel
            {
                Id = s.Id,
                AttendanceDate = s.AttendanceDate,
                InTime = s.InTime,
                OutTime = s.OutTime,
                EmployeeId = s.EmployeeId,
                DepartmentId = s.DepartmentId
            }).FirstOrDefault();
        }

        public void Update(DailyAttendanceViewModel DailyAttendVM)
        {
            try
            {
                DailyAttendanceEntity DailyAttEntity = _unitOfWork.DailyAttendanceRepository.GetAll(w => w.IsActive && w.Id == DailyAttendVM.Id).FirstOrDefault();
                if (DailyAttEntity is not null)
                {
                    DailyAttEntity.AttendanceDate = DailyAttendVM.AttendanceDate;
                    DailyAttEntity.InTime = DailyAttendVM.InTime;
                    DailyAttEntity.OutTime = DailyAttendVM.OutTime;
                    DailyAttEntity.EmployeeId = DailyAttendVM.EmployeeId;
                    DailyAttEntity.DepartmentId = DailyAttendVM.DepartmentId;
                    DailyAttEntity.UpdatedAt = DateTime.Now;
                    DailyAttEntity.UpdatedBy = "system";
                    DailyAttEntity.Ip = GetIpAsync().Result;

                    _unitOfWork.DailyAttendanceRepository.Update(DailyAttEntity);
                    _unitOfWork.Commit();
                }
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
            }
        }
    }
}

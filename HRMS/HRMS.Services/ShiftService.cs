using HRMS.Domain.Models.DataModels;
using HRMS.Domain.Models.ViewModels;
using HRMS.UnitOfWorks;
using HRMS.Domain.Utilities;

namespace HRMS.Services
{
    public class ShiftService : IShiftService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShiftService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<string> GetIpAsync()
        {
            return await NetworkHelper.GetIpAddressAsnyc();
        }
        public void Create(ShiftViewModel ShiftVM)
        {
            try
            {
                ShiftEntity shiftEntity = new ShiftEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = ShiftVM.Name,
                    InTime = ShiftVM.InTime,
                    OutTime = ShiftVM.OutTime,
                    LateAfter = ShiftVM.LateAfter,
                    EarlyOutBefore = ShiftVM.EarlyOutBefore,
                    AttendancePolicyId = ShiftVM.AttendancePolicyId,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "system",
                    IsActive = true,
                    Ip = GetIpAsync().Result
                };

                _unitOfWork.ShiftRepository.Create(shiftEntity);
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
            }
        }

        public void Delete(string id)
        {
            try
            {
                ShiftEntity shiftEntity = _unitOfWork.ShiftRepository.GetAll(w => w.IsActive && w.Id == id).FirstOrDefault();
                if (shiftEntity is not null)
                {
                    shiftEntity.IsActive = false;
                    _unitOfWork.ShiftRepository.Delete(shiftEntity);
                    _unitOfWork.Commit();
                }
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
            }
        }

        public IEnumerable<ShiftViewModel> GetAll()
        {
            return (from s in _unitOfWork.ShiftRepository.GetAll(w=>w.IsActive)
                                             join a in _unitOfWork.AttendancePolicyRepository.GetAll(w=>w.IsActive)
                                             on s.AttendancePolicyId equals a.Id
                                             
                                             select new ShiftViewModel
                                             {
                                                 Id = s.Id,
                                                 Name = s.Name,
                                                 InTime = s.InTime,
                                                 OutTime = s.OutTime,
                                                 LateAfter = s.LateAfter,
                                                 EarlyOutBefore = s.EarlyOutBefore,
                                                 AttendancePolicyId = a.Name
                                             }).ToList();
        }

        public ShiftViewModel GetById(string id)
        {
            return _unitOfWork.ShiftRepository.GetAll(w => w.IsActive && w.Id == id).Select(s => new ShiftViewModel
            {
                Id = s.Id,
                Name = s.Name,
                InTime = s.InTime,
                OutTime = s.OutTime,
                LateAfter = s.LateAfter,
                EarlyOutBefore = s.EarlyOutBefore,
                AttendancePolicyId = s.AttendancePolicyId
            }).FirstOrDefault();
        }

        public void Update(ShiftViewModel shiftVM)
        {
            try
            {
                ShiftEntity shiftEntity = _unitOfWork.ShiftRepository.GetAll(w => w.IsActive && w.Id == shiftVM.Id).FirstOrDefault();
                if (shiftEntity is not null)
                {
                    shiftEntity.Name = shiftVM.Name;
                    shiftEntity.InTime = shiftVM.InTime;
                    shiftEntity.OutTime = shiftVM.OutTime;
                    shiftEntity.LateAfter = shiftVM.LateAfter;
                    shiftEntity.EarlyOutBefore = shiftVM.EarlyOutBefore;
                    shiftEntity.AttendancePolicyId = shiftVM.AttendancePolicyId;

                    shiftEntity.UpdatedBy = "system";
                    shiftEntity.UpdatedAt = DateTime.Now;
                    shiftEntity.Ip = GetIpAsync().Result;

                    _unitOfWork.ShiftRepository.Update(shiftEntity);
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

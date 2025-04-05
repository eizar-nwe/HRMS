using HRMS.Web.Models.DataModels;
using HRMS.Web.Models.ViewModels;
using HRMS.Web.UnitOfWorks;
using HRMS.Web.Utilities;

namespace HRMS.Web.Services
{
    public class AttendancePolicyService : IAttendancePolicyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AttendancePolicyService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<string> GetIpAsync()
        {
            return await NetworkHelper.GetIpAddressAsnyc();
        }
        public void Create(AttendancePolicyViewModel attPolicyVM)
        {
            try
            {
                AttendancePolicyEntity attPolicyEntity = new AttendancePolicyEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = attPolicyVM.Name,
                    NumberOfLateTime = attPolicyVM.NumberOfLateTime,
                    NumberOfEarlyOutTime = attPolicyVM.NumberOfEarlyOutTime,
                    DeductionInAmount = attPolicyVM.DeductionInAmount,
                    DeductionInDay = attPolicyVM.DeductionInDay,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "system",
                    Ip = GetIpAsync().Result,
                    IsActive = true
                };
                _unitOfWork.AttendancePolicyRepository.Create(attPolicyEntity);
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
                AttendancePolicyEntity attPolicyEntity = _unitOfWork.AttendancePolicyRepository.GetAll(w => w.IsActive && w.Id == id).FirstOrDefault();
                if (attPolicyEntity is not null)
                {
                    attPolicyEntity.IsActive = false;
                    _unitOfWork.AttendancePolicyRepository.Delete(attPolicyEntity);
                    _unitOfWork.Commit();
                    
                }
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
            }
        }

        public IEnumerable<AttendancePolicyViewModel> GetAll()
        {
            return _unitOfWork.AttendancePolicyRepository.GetAll(w => w.IsActive).Select(s => new AttendancePolicyViewModel
            {
                Id = s.Id,
                Name = s.Name,
                NumberOfLateTime = s.NumberOfLateTime,
                NumberOfEarlyOutTime = s.NumberOfEarlyOutTime,
                DeductionInAmount = s.DeductionInAmount,
                DeductionInDay = s.DeductionInDay
            }).ToList();
        }

        public AttendancePolicyViewModel GetById(string id)
        {
            return _unitOfWork.AttendancePolicyRepository.GetAll(w => w.IsActive && w.Id == id).Select(s => new AttendancePolicyViewModel
            {
                Id = s.Id,
                Name = s.Name,
                NumberOfLateTime = s.NumberOfLateTime,
                NumberOfEarlyOutTime = s.NumberOfEarlyOutTime,
                DeductionInAmount = s.DeductionInAmount,
                DeductionInDay = s.DeductionInDay
            }).FirstOrDefault();
        }

        public void Update(AttendancePolicyViewModel attPolicyVM)
        {
            try
            {
                AttendancePolicyEntity attPolicyEntity = _unitOfWork.AttendancePolicyRepository.GetAll(w => w.IsActive && w.Id == attPolicyVM.Id).FirstOrDefault();
                if (attPolicyEntity is not null)
                {
                    attPolicyEntity.Name = attPolicyVM.Name;
                    attPolicyEntity.NumberOfLateTime = attPolicyVM.NumberOfLateTime;
                    attPolicyEntity.NumberOfEarlyOutTime = attPolicyVM.NumberOfEarlyOutTime;
                    attPolicyEntity.DeductionInAmount = attPolicyVM.DeductionInAmount;
                    attPolicyEntity.DeductionInDay = attPolicyVM.DeductionInDay;
                    attPolicyEntity.UpdatedBy = "system";
                    attPolicyEntity.UpdatedAt = DateTime.Now;
                    attPolicyEntity.Ip = GetIpAsync().Result;

                    _unitOfWork.AttendancePolicyRepository.Update(attPolicyEntity);
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

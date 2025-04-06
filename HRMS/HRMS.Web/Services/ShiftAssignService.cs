using HRMS.Web.Models.DataModels;
using HRMS.Web.Models.ViewModels;
using HRMS.Web.UnitOfWorks;
using HRMS.Web.Utilities;

namespace HRMS.Web.Services
{
    public class ShiftAssignService : IShiftAssignService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShiftAssignService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<string> GetIpAsync()
        {
            return await NetworkHelper.GetIpAddressAsnyc();
        }
        public void Create(ShiftAssignViewModel shiftAssignVM)
        {
            try
            {
                ShiftAssignEntity shiftAssignEntity = new ShiftAssignEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    EmployeeId = shiftAssignVM.EmployeeId,
                    ShiftId = shiftAssignVM.ShiftId,
                    DepartmentId = shiftAssignVM.DepartmentId,
                    FromDate = shiftAssignVM.FromDate,
                    ToDate = shiftAssignVM.ToDate,

                    CreatedAt = DateTime.Now,
                    CreatedBy = "system",
                    IsActive = true,
                    Ip = GetIpAsync().Result
                };
                _unitOfWork.ShiftAssignRepository.Create(shiftAssignEntity);
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
                ShiftAssignEntity ShiftAssignEntity = _unitOfWork.ShiftAssignRepository.GetAll(w => w.IsActive && w.Id == id).FirstOrDefault();
                if (ShiftAssignEntity is not null)
                {
                    ShiftAssignEntity.IsActive = false;
                    _unitOfWork.ShiftAssignRepository.Delete(ShiftAssignEntity);
                    _unitOfWork.Commit();
                }
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
            }
        }

        public IEnumerable<ShiftAssignViewModel> GetAll()
        {
            return (from a in _unitOfWork.ShiftAssignRepository.GetAll(w=>w.IsActive)
                    join e in _unitOfWork.EmployeeRespository.GetAll(w=>w.IsActive)
                    on a.EmployeeId equals e.Id
                    join d in _unitOfWork.DepartmentRespository.GetAll(w=>w.IsActive)
                    on a.DepartmentId equals d.Id
                    join s in _unitOfWork.ShiftRepository.GetAll(w=>w.IsActive)
                    on a.ShiftId equals s.Id                    

                    select new ShiftAssignViewModel
                    {
                        Id = a.Id,
                        FromDate = a.FromDate,
                        ToDate = a.ToDate,
                        EmployeeInfo = e.Code + "/" + e.Name,
                        ShiftInfo = s.Name,
                        DepartmentInfo = d.Code + "/" + d.Description,
                    }).ToList();
        }

        public ShiftAssignViewModel GetById(string id)
        {
            return _unitOfWork.ShiftAssignRepository.GetAll(w => w.IsActive && w.Id == id).Select(s => new ShiftAssignViewModel
            {
                Id = s.Id,
                EmployeeId = s.EmployeeId,
                ShiftId = s.ShiftId,
                DepartmentId = s.DepartmentId,
                FromDate = s.FromDate,
                ToDate = s.ToDate
            }).FirstOrDefault();
        }

        public void Update(ShiftAssignViewModel shiftAssignVM)
        {
            try
            {
                ShiftAssignEntity shiftAssignEntity = _unitOfWork.ShiftAssignRepository.GetAll(w => w.IsActive && w.Id == shiftAssignVM.Id).FirstOrDefault();
                if (shiftAssignEntity is not null)
                {
                    shiftAssignEntity.EmployeeId = shiftAssignVM.EmployeeId;
                    shiftAssignEntity.ShiftId = shiftAssignVM.ShiftId;
                    shiftAssignEntity.DepartmentId = shiftAssignVM.DepartmentId;
                    shiftAssignEntity.UpdatedAt = DateTime.Now;
                    shiftAssignEntity.UpdatedBy = "system";
                    shiftAssignEntity.Ip = GetIpAsync().Result;

                    _unitOfWork.ShiftAssignRepository.Update(shiftAssignEntity);
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

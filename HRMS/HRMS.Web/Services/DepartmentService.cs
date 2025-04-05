using HRMS.Web.Models.DataModels;
using HRMS.Web.Models.ViewModels;
using HRMS.Web.UnitOfWorks;
using HRMS.Web.Utilities;

namespace HRMS.Web.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWorks;

        public DepartmentService(IUnitOfWork unitOfWorks)
        {
            this._unitOfWorks = unitOfWorks;
        }
        public async Task<string> GetIpAsync()
        {
            return await NetworkHelper.GetIpAddressAsnyc();
        }
        public void Create(DepartmentViewModel departmentVM)
        {
            try
            {
                DepartmentEntity deptEntity = new DepartmentEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    Code = departmentVM.Code,
                    Description = departmentVM.Description,
                    ExtensionPhone = departmentVM.ExtensionPhone,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "system",
                    IsActive = true,
                    Ip = GetIpAsync().Result
                };

                _unitOfWorks.DepartmentRespository.Create(deptEntity);
                _unitOfWorks.Commit();
            }
            catch (Exception)
            {
                _unitOfWorks.Rollback();
            }
        }

        public void Delete(string Id)
        {
            try
            {
                DepartmentEntity deptEntity = _unitOfWorks.DepartmentRespository.GetAll(w => w.IsActive && w.Id == Id).FirstOrDefault();

                if (deptEntity is not null)
                {
                    deptEntity.IsActive = false;
                    _unitOfWorks.DepartmentRespository.Delete(deptEntity);
                    _unitOfWorks.Commit();
                }
            }
            catch (Exception e)
            {
                _unitOfWorks.Rollback();
            }
        }

        public IEnumerable<DepartmentViewModel> GetAll()
        {
            return _unitOfWorks.DepartmentRespository.GetAll(w => w.IsActive).Select(s => new DepartmentViewModel
            {
                Id = s.Id,
                Code = s.Code,
                Description = s.Description,
                ExtensionPhone = s.ExtensionPhone
            }).ToList();
        }

        public DepartmentViewModel GetById(string Id)
        {
            return _unitOfWorks.DepartmentRespository.GetAll(w => w.IsActive && w.Id == Id).Select(s => new DepartmentViewModel
            {
                Id = s.Id,
                Code = s.Code,
                Description = s.Description,
                ExtensionPhone = s.ExtensionPhone
            }).FirstOrDefault();
        }

        public void Update(DepartmentViewModel departmentVM)
        {
            try
            {
                DepartmentEntity deptEntity = _unitOfWorks.DepartmentRespository.GetAll(w => w.IsActive && w.Id == departmentVM.Id).FirstOrDefault();

                if (deptEntity is not null)
                {
                    deptEntity.Code = departmentVM.Code;
                    deptEntity.Description = departmentVM.Description;
                    deptEntity.ExtensionPhone = departmentVM.ExtensionPhone;
                    deptEntity.UpdatedBy = "system";
                    deptEntity.UpdatedAt = DateTime.Now;
                    deptEntity.Ip = GetIpAsync().Result;

                    _unitOfWorks.DepartmentRespository.Update(deptEntity);
                    _unitOfWorks.Commit();
                }
            }
            catch (Exception e)
            {
                _unitOfWorks.Rollback();
            }
        }
    }
}

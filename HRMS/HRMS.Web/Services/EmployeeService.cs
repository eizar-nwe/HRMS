using HRMS.Web.Models.DataModels;
using HRMS.Web.Models.ViewModels;
using HRMS.Web.UnitOfWorks;
using HRMS.Web.Utilities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HRMS.Web.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<string> GetIPAsync()
        {
            return await NetworkHelper.GetIpAddressAsnyc();
        }
        public void Create(EmployeeViewModel employeeVM)
        {
            try
            {                
                //DTO process from view model to data model for save process
                EmployeeEntity employeeEntity = new EmployeeEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    Code = employeeVM.Code,
                    Name = employeeVM.Name,
                    Email = employeeVM.Email,
                    Gender = employeeVM.Gender,
                    DOB = employeeVM.DOB,
                    DOE = employeeVM.DOE,
                    DOR = employeeVM.DOR,
                    Address = employeeVM.Address,
                    BasicSalary = employeeVM.BasicSalary,
                    Phone = employeeVM.Phone,
                    DepartmentId = employeeVM.DepartmentId,
                    PositionId = employeeVM.PositionId,
                    UserId = employeeVM.UserId,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "system",
                    IsActive = true,
                    Ip = GetIPAsync().Result
                };
                _unitOfWork.EmployeeRespository.Create(employeeEntity);
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
            }
        }

        public void Delete(string Id)
        {
            try
            {
                EmployeeEntity empeEntity = _unitOfWork.EmployeeRespository.GetAll(w => w.IsActive && w.Id == Id).FirstOrDefault();

                if (empeEntity is not null)
                {
                    empeEntity.IsActive = false;
                    _unitOfWork.EmployeeRespository.Update(empeEntity);
                    _unitOfWork.Commit();

                }
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
            }
        }

        public IEnumerable<EmployeeViewModel> GetAll()
        {
            return (from e in _unitOfWork.EmployeeRespository.GetAll(s=>s.IsActive)
                                                  join p in _unitOfWork.PositionRepository.GetAll(s => s.IsActive)
                                                  on e.PositionId equals p.Id
                                                  join d in _unitOfWork.DepartmentRespository.GetAll(s => s.IsActive)
                                                  on e.DepartmentId equals d.Id
                                                  where e.IsActive && d.IsActive && p.IsActive

                                                  select new EmployeeViewModel
                                                  {
                                                      Id = e.Id,
                                                      Code = e.Code,
                                                      Name = e.Name,
                                                      Email = e.Email,
                                                      Gender = e.Gender,
                                                      DOB = e.DOB,
                                                      DOR = e.DOR,
                                                      DOE = e.DOE,
                                                      Address = e.Address,
                                                      BasicSalary = e.BasicSalary,
                                                      Phone = e.Phone,
                                                      DepartmentInfo = d.Code + "/" + d.Description,
                                                      PositionInfo = p.Code + "/" + p.Description
                                                  }).ToList();
            
        }

        public EmployeeViewModel GetById(string Id)
        {
            return _unitOfWork.EmployeeRespository.GetAll(w => w.IsActive && w.Id == Id).Select(e => new EmployeeViewModel
            {
                Id = e.Id,
                Code = e.Code,
                Name = e.Name,
                DOB = e.DOB,
                DOR = e.DOR,
                DOE = e.DOE,
                Address = e.Address,
                Phone = e.Phone,
                PositionId = e.PositionId,
                DepartmentId = e.DepartmentId,
                Gender = e.Gender,
                Email = e.Email,
                BasicSalary = e.BasicSalary,
            }).FirstOrDefault();
        }

        public void Update(EmployeeViewModel employeeVM)
        {
            try
            {                
                //DTO process from view model to data model for save process
                EmployeeEntity employeeEntity = _unitOfWork.EmployeeRespository.GetAll(w => w.IsActive && w.Id == employeeVM.Id).FirstOrDefault();

                if (employeeEntity is not null)
                {
                    employeeEntity.Code = employeeVM.Code;
                    employeeEntity.Name = employeeVM.Name;
                    employeeEntity.Email = employeeVM.Email;
                    employeeEntity.Gender = employeeVM.Gender;
                    employeeEntity.DOB = employeeVM.DOB;
                    employeeEntity.DOE = employeeVM.DOE;
                    employeeEntity.DOR = employeeVM.DOR;
                    employeeEntity.Address = employeeVM.Address;
                    employeeEntity.BasicSalary = employeeVM.BasicSalary;
                    employeeEntity.Phone = employeeVM.Phone;
                    employeeEntity.DepartmentId = employeeVM.DepartmentId;
                    employeeEntity.PositionId = employeeVM.PositionId;
                    employeeEntity.UpdatedAt = DateTime.Now;
                    employeeEntity.UpdatedBy = "system";
                    employeeEntity.Ip = GetIPAsync().Result;

                    _unitOfWork.EmployeeRespository.Update(employeeEntity);
                    _unitOfWork.Commit();
                };
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
            }
        }

        public IEnumerable<EmployeeDetailReportViewModel> GetByCode(string fromCode, string toCode)
        {
            IEnumerable<EmployeeDetailReportViewModel> employeeDetails = (from e in _unitOfWork.EmployeeRespository.GetAll(w => w.IsActive)
                                                                         join p in _unitOfWork.PositionRepository.GetAll(w => w.IsActive)
                                                                         on e.PositionId equals p.Id
                                                                         join d in _unitOfWork.DepartmentRespository.GetAll(w => w.IsActive)
                                                                         on e.DepartmentId equals d.Id
                                                                         where e.Code.CompareTo(fromCode)>=0 && e.Code.CompareTo(toCode)<=0

                                                                         select new EmployeeDetailReportViewModel
                                                                         {                                                                      
                                                                             Code = e.Code,
                                                                             Name = e.Name,
                                                                             Email=e.Email,
                                                                             Gender=e.Gender,
                                                                             DOB = e.DOB.ToString("yyyy-MM-dd"),
                                                                             DOR = e.DOR.HasValue? e.DOR.Value.ToString("yyyy-MM-dd") : null,
                                                                             DOE = e.DOE.ToString("yyyy-MM-dd"),
                                                                             Address = e.Address,
                                                                             BasicSalary=e.BasicSalary,
                                                                             Phone = e.Phone,
                                                                             DepartmentInfo = d.Code + "/" + d.Description,
                                                                             PositionInfo = p.Code + "/" + p.Description
                                                                         }).AsEnumerable();

            return employeeDetails;
        }

        public IEnumerable<EmployeeDetailReportViewModel> GetByDepartmentId(string fromDepartmentId)
        {
            throw new NotImplementedException();
        }
    }
}

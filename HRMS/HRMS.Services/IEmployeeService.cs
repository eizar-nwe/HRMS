using HRMS.Domain.Models.ViewModels;

namespace HRMS.Services
{
    public interface IEmployeeService
    {
        void Create(EmployeeViewModel employeeVM);
        IEnumerable<EmployeeViewModel> GetAll();
        EmployeeViewModel GetById(string Id);
        void Update(EmployeeViewModel employeeVM);
        void Delete(string Id);
        IEnumerable<EmployeeDetailReportViewModel> GetByCode(string fromCode, string toCode);
        IEnumerable<EmployeeDetailReportViewModel> GetByDepartmentId(string fromDepartmentId);
    }
}

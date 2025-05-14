using HRMS.Domain.Models.ViewModels;

namespace HRMS.Services
{
    public interface IDepartmentService
    {
        void Create(DepartmentViewModel departmentVM);
        IEnumerable<DepartmentViewModel> GetAll();
        DepartmentViewModel GetById(string Id);
        void Update(DepartmentViewModel departmentVM);
        void Delete(string Id);
    }
}

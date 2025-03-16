using HRMS.Web.Models.ViewModels;

namespace HRMS.Web.Services
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

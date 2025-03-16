using HRMS.Web.Models.ViewModels;

namespace HRMS.Web.Services
{
    public interface IPositionService
    {
        //crud process
        void Create(PositionViewModel viewModel);

        //R
        IEnumerable<PositionViewModel> GetAll();

        PositionViewModel GetById(string id);
        //U
        void Update(PositionViewModel viewModel);

        //D
        void Delete(string Id);
    }
}

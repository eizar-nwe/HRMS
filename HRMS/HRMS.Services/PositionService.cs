using HRMS.Domain.Models.DataModels;
using HRMS.Domain.Models.ViewModels;
using HRMS.UnitOfWorks;
using HRMS.Domain.Utilities;

namespace HRMS.Services
{
    public class PositionService : IPositionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PositionService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<string> GetIPAsync()
        {
            return await NetworkHelper.GetIpAddressAsnyc();
        }
        public void Create(PositionViewModel viewModel)
        {
            try
            {
                //DTO >> Data Transfer Object in here (from View Model to Data Model)
                PositionEntity positionEntity = new PositionEntity()
                {
                    Id = Guid.NewGuid().ToString(), //for primary key value
                    Code = viewModel.Code,
                    Description = viewModel.Description,
                    Level = viewModel.Level,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "system",
                    IsActive = true,
                    Ip = GetIPAsync().Result
                };
                _unitOfWork.PositionRepository.Create(positionEntity);
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
                PositionEntity positionEntity = _unitOfWork.PositionRepository.GetAll(w => w.Id == Id).FirstOrDefault();
                if (positionEntity is not null)
                {
                    positionEntity.IsActive = false;
                    _unitOfWork.PositionRepository.Delete(positionEntity);
                    _unitOfWork.Commit();
                }
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
            }
        }

        public IEnumerable<PositionViewModel> GetAll()
        {
            return _unitOfWork.PositionRepository.GetAll(w => w.IsActive).Select(s => new PositionViewModel
            {
                Id = s.Id,
                Code = s.Code,
                Description = s.Description,
                Level = s.Level
            }).ToList();
        }

        public PositionViewModel GetById(string id)
        {
            return _unitOfWork.PositionRepository.GetAll(w => w.IsActive && w.Id == id).Select(s => new PositionViewModel
            {
                Id = s.Id,
                Code = s.Code,
                Description = s.Description,
                Level = s.Level
            }).FirstOrDefault();

        }

        public async void Update(PositionViewModel viewModel)
        {
            try
            {
                PositionEntity existingPosition = _unitOfWork.PositionRepository.GetAll(w => w.IsActive && w.Id == viewModel.Id).FirstOrDefault();

                if (existingPosition is not null)
                {
                    existingPosition.Description = viewModel.Description;
                    existingPosition.Level = viewModel.Level;
                    existingPosition.UpdatedAt = DateTime.Now;
                    existingPosition.UpdatedBy = "system";
                    existingPosition.Ip = await NetworkHelper.GetIpAddressAsnyc();

                    _unitOfWork.PositionRepository.Update(existingPosition);
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

using HRMS.Web.Models.DataModels;
using HRMS.Web.Models.ViewModels;
using HRMS.Web.Repositories.Domain;
using HRMS.Web.Services;
using HRMS.Web.UnitOfWorks;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.UnitTests.Domain.Position
{
    public class PositionServiceUnitTest
    {
        public Mock<IPositionService> positionServiceMock = new Mock<IPositionService>();

        public Mock<IPositionRepository> positionRepositoryMock = new Mock<IPositionRepository>();

        public Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();

        [Fact]
        public void ShouldCreatedPosition()
        {
            //1) Arrange
            //Mock
            var expectedPositionViewModel = new PositionViewModel()
            {
                Code = "POO1",
                Description="Position 1",
                Level = 1
            };

            var expectedPositionEntity = new PositionEntity()
            {
                Id = Guid.NewGuid().ToString(), 
                Code = expectedPositionViewModel.Code,
                Description = expectedPositionViewModel.Description,
                Level = expectedPositionViewModel.Level,
                CreatedAt = DateTime.Now,
                CreatedBy = "system",
                IsActive = true,
                Ip = "127.0.0.1"
            };
            positionRepositoryMock.Setup(r => r.Create(expectedPositionEntity));
            unitOfWorkMock.Setup(u => u.PositionRepository).Returns(positionRepositoryMock.Object);
            //2) Act
            var positionService = new PositionService(unitOfWorkMock.Object);

            //3) Assert
            var exception = Record.Exception(()=>positionService.Create(expectedPositionViewModel));
            Assert.Null(exception);
        }
        [Fact]
        public void ShouldAllPosition()
        {
            //1) Arrange
            //Mock
            var expectedViewModels = new List<PositionViewModel>()
            {
                new PositionViewModel()
                {
                    Code = "POO1",
                    Description="Position 1",
                    Level = 1,
                },
                new PositionViewModel()
                {
                    Code = "POO2",
                    Description="Position 2",
                    Level = 2
                }
            };
            var expectedPositionEntity = new List<PositionEntity>
            {
                new PositionEntity
                {
                    Id = Guid.NewGuid().ToString(),
                    Code = "POO1",
                    Description = "Position 1",
                    Level = 1,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "system",
                    IsActive = true,
                    Ip = "127.0.0.1"
                },
                new PositionEntity
                {
                    Id = Guid.NewGuid().ToString(),
                    Code = "POO2",
                    Description = "Position 2",
                    Level =2,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "system",
                    IsActive = true,
                    Ip = "127.0.0.1"
                }
            };
            positionRepositoryMock.Setup(r => r.GetAll(w=>w.IsActive)).Returns(expectedPositionEntity);
            unitOfWorkMock.Setup(u => u.PositionRepository).Returns(positionRepositoryMock.Object);

            //2) Act
            var positionService = new PositionService(unitOfWorkMock.Object);
            var actualResults = positionService.GetAll();
            //3) Assert
            //Assert.Equal(expectedViewModels.Count, actualResults.Count());
            //Assert.Equal(expectedViewModels.First().Id, actualResults.First().Id);
            //Assert.Equal(expectedViewModels.First().Code, actualResults.First().Code);

            for(int i=0; i< expectedViewModels.Count; i++)
            {
                var expected = expectedViewModels[i];
                var actual = actualResults.ElementAt(i);
                Assert.Equal(expected.Code, actual.Code);
                Assert.Equal(expected.Description, actual.Description);
                Assert.Equal(expected.Level, actual.Level);
            }
        }
    }
}

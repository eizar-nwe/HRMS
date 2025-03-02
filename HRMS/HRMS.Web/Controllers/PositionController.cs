using HRMS.Web.DAO;
using HRMS.Web.Models.DataModels;
using HRMS.Web.Models.ViewModels;
using HRMS.Web.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HRMS.Web.Controllers
{
    public class PositionController : Controller
    {
        //Dependency Injection dbContext;
        private readonly HRMSWebDbContext _db;
        public PositionController(HRMSWebDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Entry()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Entry(PositionViewModel positionVM)
        {
            try
            {

                //DTO >> Data Transfer Object in here (from View Model to Data Model)
                PositionEntity positionEntity = new PositionEntity()
                {
                    Id = Guid.NewGuid().ToString(), //for primary key value
                    Code = positionVM.Code,
                    Description = positionVM.Description,
                    Level = positionVM.Level,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "system",
                    IsActive = true,
                    Ip =await NetworkHelper.GetIpAddressAsync()
                };

                _db.Positions.Add(positionEntity); //adding the record to the context
                _db.SaveChanges(); //saving the data to the database in here

                TempData["Msg"] = "Data has been saved successfully.";
                TempData["IsErrorOccur"] = false;
            }
            catch (Exception e)
            {
                TempData["Msg"] = "Oh, sorry error was occured when record is created time.";
                TempData["IsErrorOccur"] = true;
            }
            return RedirectToAction("List");
        }

        public IActionResult List()
        {
            IList<PositionViewModel> positionViews = _db.Positions.Where(w => w.IsActive).Select(s => new PositionViewModel
            {
                Id=s.Id,
                Code=s.Code,
                Description=s.Description,
                Level=s.Level
            }).ToList();

            return View(positionViews);
        }

        //position/delete?Id=1
        public IActionResult Delete(string id)
        {
            try
            {
                PositionEntity positionEntity = _db.Positions.Where(w => w.Id == id).FirstOrDefault();
                if (positionEntity is not null)
                {
                    positionEntity.IsActive = false;
                    _db.SaveChanges();
                    TempData["Msg"] = "Data has been deleted successfully.";
                    TempData["IsErrorOccur"] = false;
                }

            }
            catch (Exception e)
            {
                TempData["Msg"] = "Oh, sorry error was occured when record is deleted time.";
                TempData["IsErrorOccur"] = true;
            }

            return RedirectToAction("List");
        }
        public IActionResult Edit(string id)
        {
            PositionViewModel positionView = _db.Positions.Where(w => w.IsActive && w.Id == id).Select(s => new PositionViewModel
            {
                Id = s.Id,
                Code = s.Code,
                Description = s.Description,
                Level = s.Level
            }).FirstOrDefault();

            return View(positionView);
        }

        [HttpPost]
        public async Task<IActionResult> Update(PositionViewModel positionVM)
        {
            try
            {                
                PositionEntity existingPosition = _db.Positions.Where(w => w.IsActive && w.Id == positionVM.Id).FirstOrDefault();

                if(existingPosition is not null)
                {
                    existingPosition.Description = positionVM.Description;
                    existingPosition.Level = positionVM.Level;
                    existingPosition.UpdatedAt = DateTime.Now;
                    existingPosition.UpdatedBy = "system";
                    existingPosition.Ip = await NetworkHelper.GetIpAddressAsync();
                }
                _db.Positions.Update(existingPosition); //updating the record to the context
                _db.SaveChanges(); //saving the data to the database in here

                TempData["Msg"] = "Data has been updated successfully.";
                TempData["IsErrorOccur"] = false;
            }
            catch (Exception e)
            {
                TempData["Msg"] = "Oh, sorry error was occured when record is updated time.";
                TempData["IsErrorOccur"] = true;
            }
            return RedirectToAction("List");
        }
    }
}

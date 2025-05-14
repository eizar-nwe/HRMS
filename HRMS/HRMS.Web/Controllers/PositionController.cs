using HRMS.Domain.DAO;
using HRMS.Domain.Models.DataModels;
using HRMS.Domain.Models.ViewModels;
using HRMS.Services;
using HRMS.Domain.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HRMS.Web.Controllers
{
    public class PositionController : Controller
    {
        private readonly IPositionService _positionService;

        //Dependency Injection Service;

        public PositionController(IPositionService positionService)
        {
            this._positionService = positionService;
        }

        [HttpGet]
        [Authorize(Roles ="HR")]
        public IActionResult Entry()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "HR")]
        public async Task<IActionResult> Entry(PositionViewModel positionVM)
        {
            try
            {
                _positionService.Create(positionVM);
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

        public IActionResult List() => View(_positionService.GetAll().ToList());
        //{
        //IList<PositionViewModel> positionViews = _db.Positions.Where(w => w.IsActive).Select(s => new PositionViewModel
        //{
        //    Id=s.Id,
        //    Code=s.Code,
        //    Description=s.Description,
        //    Level=s.Level
        //}).ToList();

        //    return View(positionViews);
        //}

        //position/delete?Id=1
        [Authorize(Roles = "HR")]
        public IActionResult Delete(string id)
        {
            try
            {
                _positionService.Delete(id);
                TempData["Msg"] = "Data has been deleted successfully.";
                TempData["IsErrorOccur"] = false;                
            }
            catch (Exception e)
            {
                TempData["Msg"] = "Oh, sorry error was occured when record is deleted time.";
                TempData["IsErrorOccur"] = true;
            }

            return RedirectToAction("List");
        }
        [Authorize(Roles = "HR")]
        public IActionResult Edit(string id) => View(_positionService.GetById(id));
        //{
            //PositionViewModel positionView = _db.Positions.Where(w => w.IsActive && w.Id == id).Select(s => new PositionViewModel
            //{
            //    Id = s.Id,
            //    Code = s.Code,
            //    Description = s.Description,
            //    Level = s.Level
            //}).FirstOrDefault();

        //    return View(positionView);
        //}

        [HttpPost]
        [Authorize(Roles = "HR")]
        public async Task<IActionResult> Update(PositionViewModel positionVM)
        {
            try
            {
                _positionService.Update(positionVM);
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

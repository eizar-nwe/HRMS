﻿using HRMS.Domain.Models.ViewModels;
using HRMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Web.Controllers
{
    public class AttendanceMasterController : Controller
    {
        private readonly IAttendanceMasterService _attMstrService;
        private readonly IEmployeeService _empeServie;
        private readonly IDepartmentService _deptServiec;
        private readonly IShiftService _shiftService;

        public AttendanceMasterController(IAttendanceMasterService attMstrService,IEmployeeService empeServie,IDepartmentService deptServiec,IShiftService shiftService)
        {
            this._attMstrService = attMstrService;
            this._empeServie = empeServie;
            this._deptServiec = deptServiec;
            this._shiftService = shiftService;
        }
        public IActionResult List() => View(_attMstrService.GetAll().ToList());
        public IActionResult DayEndProcess()
        {
            AttendanceMasterViewModel attMstrVM = new AttendanceMasterViewModel
            {
                EmployeeViewModels = _empeServie.GetAll().ToList(),
                DepartmentViewModels = _deptServiec.GetAll().ToList(),
                ShiftViewModels = _shiftService.GetAll().ToList()
            };

            return View(attMstrVM);
        }
        [HttpPost]
        public IActionResult DayEndProcess(AttendanceMasterViewModel attMstrVM)
        {
            try
            {
                _attMstrService.Delete(attMstrVM.AttendanceDate, attMstrVM.ToDate, attMstrVM.DepartmentId, attMstrVM.EmployeeId);
                _attMstrService.DayEndProcess(attMstrVM);
                ViewBag.Info = "successfully save a record to the system";
            }
            catch (Exception ex)
            {
                ViewBag.Info = "Error occur when  saving a record  to the system";
            }
            return RedirectToAction("List");
        }
    }
}

﻿namespace HRMS.Domain.Models.ViewModels
{
    public class EmployeeViewModel
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DOE { get; set; }
        public DateTime? DOR { get; set; }
        public string? Address { get; set; }
        public decimal BasicSalary { get; set; }
        public string Phone { get; set; }
        public string DepartmentId { get; set; } //create/update process
        public string PositionId { get; set; } //create/update process
        public string DepartmentInfo { get; set; } //to list for showing 
        public string PositionInfo { get; set; } //to list for showing 
        public string UserId { get; set; }
        //to use select box view
        public IList<PositionViewModel> PositionViewModels { get; set; }
        public IList<DepartmentViewModel> DepartmentViewModels { get; set; }


    }
}

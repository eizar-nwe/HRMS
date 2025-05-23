﻿namespace HRMS.Domain.Models.ViewModels
{
    public class AttendanceMasterViewModel
    {
        public string Id { get; set; }
        public DateTime AttendanceDate { get; set; }
        public TimeSpan InTime { get; set; }
        public TimeSpan OutTime { get; set; }
        public string EmployeeId { get; set; }
        public string DepartmentId { get; set; }
        public string ShiftId { get; set; }
        public bool IsLate { get; set; }
        public bool IsEarlyOut { get; set; }
        public bool IsLeave { get; set; }

        public DateTime ToDate { get; set; }
        public string EmployeeInfo { get; set; }
        public string DepartmentInfo { get; set; }
        public string ShiftInfo { get; set; }

        public IList<EmployeeViewModel> EmployeeViewModels { get; set; }
        public IList<DepartmentViewModel> DepartmentViewModels{ get; set; }
        public IList<ShiftViewModel> ShiftViewModels { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Domain.Models.DataModels
{
    [Table("Payroll")]
    public class PayrollEntity:BaseEntity
    {
        public required DateTime FromDate { get; set; }
        public required DateTime ToDate { get; set; }
        public required string EmployeeId { get; set; }
        public required string DepartmentId { get; set; }
        public decimal IncomeTax { get; set; }
        public decimal GrossPay { get; set; }
        public decimal NetPay { get; set; }
        public decimal Allowance { get; set; }
        public decimal Deduction { get; set; }
        public decimal AttendanceDays { get; set; }
        public decimal AttendanceDeduction { get; set; }
        public decimal PayPerDay { get; set; }

    }
}

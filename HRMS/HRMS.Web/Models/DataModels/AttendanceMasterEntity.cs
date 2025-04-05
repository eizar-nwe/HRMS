using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Web.Models.DataModels
{
    [Table("AttendanceMaster")]
    public class AttendanceMasterEntity :BaseEntity
    {
        public required DateTime AttendanceDate { get; set; }
        public required TimeSpan InTime { get; set; }
        public required TimeSpan OutTime { get; set; }
        public required string EmployeeId { get; set; }
        public required string DepartmentId { get; set; }
        public required string ShiftId { get; set; }
        public required bool IsLate { get; set; }
        public required bool IsEarlyOut { get; set; }
        public required bool IsLeave { get; set; }
    }
}

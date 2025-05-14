using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Domain.Models.DataModels
{
    [Table("AttendanceMaster")]
    public class AttendanceMasterEntity :BaseEntity
    {
        public DateTime AttendanceDate { get; set; }
        public TimeSpan InTime { get; set; }
        public TimeSpan OutTime { get; set; }
        public string EmployeeId { get; set; }
        public string DepartmentId { get; set; }
        public string ShiftId { get; set; }
        public bool IsLate { get; set; }
        public bool IsEarlyOut { get; set; }
        public bool IsLeave { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Domain.Models.DataModels
{
    [Table("Shift")]
    public class ShiftEntity:BaseEntity
    {
        public required string Name { get; set; }
        public required TimeSpan InTime { get; set; }
        public required TimeSpan OutTime { get; set; }
        public required TimeSpan LateAfter{ get; set; }
        public required TimeSpan EarlyOutBefore { get; set; }
        public required string AttendancePolicyId { get; set; }
    }
}

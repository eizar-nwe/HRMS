using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Domain.Models.DataModels
{
    [Table("Position")] //Model Attribute
    public class PositionEntity:BaseEntity
    {
        [MaxLength(200)] //length char 10
        public required string Code { get; set; }
        public required string Description { get; set; }
        public int? Level { get; set; }
       
    }
}

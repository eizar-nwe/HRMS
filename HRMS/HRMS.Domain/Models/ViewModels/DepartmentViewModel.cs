namespace HRMS.Domain.Models.ViewModels
{
    public class DepartmentViewModel
    {
        public string? Id { get; set; } //for delete & Update Process
        public string Code { get; set; }
        public string Description { get; set; }
        public string? ExtensionPhone { get; set; }
    }
}

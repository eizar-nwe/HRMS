namespace HRMS.Web.Models.ViewModels
{
    public class PositionViewModel
    {
        public string Id { get; set; } //for delete & Update Process
        public string Code { get; set; }
        public string Description { get; set; }
        public int? Level { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Domain.Models.ViewModels
{
    public class DepartmentViewModelV2:DepartmentViewModel
    {
        public decimal Incomebuget { get; set; }
        public decimal Expensebuget { get; set; }
    }
}

using HRMS.Domain.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Services
{
    public interface IAuthService
    {
        Task<(int, string)> AuthLogin(AuthViewModel authViewModel);
    }
}

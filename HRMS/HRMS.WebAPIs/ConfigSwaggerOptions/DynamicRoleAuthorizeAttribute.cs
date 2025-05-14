using Microsoft.AspNetCore.Authorization;

namespace HRMS.WebAPIs.ConfigSwaggerOptions
{
    public class DynamicRoleAuthorizeAttribute: AuthorizeAttribute
    {
        public DynamicRoleAuthorizeAttribute(string roleKey)
        {
            Policy = $"RolePolicy:{roleKey}";
        }
    }    
}

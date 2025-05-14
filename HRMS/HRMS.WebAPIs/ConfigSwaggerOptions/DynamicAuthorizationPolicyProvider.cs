using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;

namespace HRMS.WebAPIs.ConfigSwaggerOptions
{
    public class DynamicAuthorizationPolicyProvider: DefaultAuthorizationPolicyProvider
    {
        private readonly IConfiguration _configuration;

        public DynamicAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options,IConfiguration configuration):base(options)
        {
            this._configuration = configuration;
        }
        public override async Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (policyName.StartsWith("RolePolicy:"))
            {
                var roleKey = policyName.Substring("RolePolicy:".Length);
                var roles = _configuration.GetSection($"ApplicationRoles:{roleKey}").Get<string[]>();

                if (roles != null && roles.Any())
                {
                    var policy = new AuthorizationPolicyBuilder()
                        .RequireRole(roles)
                        .Build();
                    return await Task.FromResult(policy);
                }
            }
            return await base.GetPolicyAsync(policyName);
        }
    }
}

using HRMS.Domain.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(IConfiguration config,UserManager<IdentityUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            this._config = config;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public async Task<(int, string)> AuthLogin(AuthViewModel authViewModel)
        {
            var loginUser = await _userManager.FindByNameAsync(authViewModel.UserName);
            if(loginUser == null)
            {
                return (401, "Invalid User");
            }

            var validPassword = await _userManager.CheckPasswordAsync(loginUser,authViewModel.Password);
            if (!validPassword)
            {
                return (401, "Invalid password");
            }

            var userRoles = await _userManager.GetRolesAsync(loginUser);
            
            //set the claim name and role to generate token
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,loginUser.UserName),                    
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            foreach(var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            string generatedToken = GenerateToken(authClaims);
            return (200, generatedToken);
             
            //---------Default Value-------------
            /*if (authViewModel.userName == "Admin" && authViewModel.password == "Admin123")
            {
                //set the claim name and role to generate token
                var authClaim = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,"Admin"),
                    new Claim(ClaimTypes.Role,"HR"),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                };

                string generatedToken = GenerateToken(authClaim);

                return Task.FromResult((200, generatedToken));
            }
            else
            {
                return Task.FromResult((401, "Unauthorized"));
            }*/
        }
        private string GenerateToken(IList<Claim> claims)
        {
            var authkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["jwt:SecretKey"]));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                //Expires = DateTime.UtcNow.AddHours(1),
                Expires = DateTime.UtcNow.AddMinutes(3),
                Issuer = _config["jwt:Issuer"],
                Audience = _config["jwt:Audience"],
                SigningCredentials = new SigningCredentials(authkey, SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}   

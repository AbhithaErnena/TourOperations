using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TourMgmtAPI.Accounts;
namespace TourMgmtAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AuthUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _config;

        public AuthService(UserManager<AuthUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _config = config;
        }

        public (int, string) Register(RegisterModel model)
        {
            var userExists = _userManager.FindByNameAsync(model.UserName).Result;
            if (userExists != null) return (0, "User already exists");

            var user = new AuthUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = _userManager.CreateAsync(user, model.Password).Result;
            if (!result.Succeeded) return (0, "User creation failed");

            if (!_roleManager.RoleExistsAsync(model.Role).Result)
                _roleManager.CreateAsync(new IdentityRole(model.Role)).Wait();

            _userManager.AddToRoleAsync(user, model.Role).Wait();
            return (1, "User registered successfully");
        }

        public (int, string) Login(LoginModel model)
        {
            var user = _userManager.FindByNameAsync(model.UserName).Result;
            if (user != null && _userManager.CheckPasswordAsync(user, model.Password).Result)
            {
                var roles = _userManager.GetRolesAsync(user).Result;
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
                foreach (var role in roles)
                    claims.Add(new Claim(ClaimTypes.Role, role));

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));
                var token = new JwtSecurityToken(
                    issuer: _config["JWT:ValidIssuer"],
                    audience: _config["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: claims,
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

                return (1, new JwtSecurityTokenHandler().WriteToken(token));
            }

            return (0, "Invalid credentials");
        }
    }

}

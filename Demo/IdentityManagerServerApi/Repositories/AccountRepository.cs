using Azure.Identity;
using IdentityManagerServerApi.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SharedClassLibrary.Contracts;
using SharedClassLibrary.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static SharedClassLibrary.DTOs.ServiceResponse;

namespace IdentityManagerServerApi.Repositories
{
    public class AccountRepository(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration config
        ) : IUserAccount
    {
        public async Task<ServiceResponse.GeneralResposne> CreateAccount(UserDto userDto)
        {
            if (userDto is null) return new GeneralResposne(false, "Model is Empty");
            var newUser = new ApplicationUser()
            {
                Name = userDto.Name,
                Email = userDto.Email,
                PasswordHash = userDto.Password,
                UserName = userDto.Name
            };
            var user = await userManager.FindByEmailAsync( newUser.Email );
            if (user is not null) return new GeneralResposne(false, "User registered already");

            var createUser = await userManager.CreateAsync(newUser, userDto.Password);
            if (!createUser.Succeeded) return new GeneralResposne(false, "Error occured.. please try again");


            var checkAdmin = await roleManager.FindByNameAsync("Admin");
            if(checkAdmin is null)
            {
                await roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
                await userManager.AddToRoleAsync(newUser, "Admin");
                return new GeneralResposne(true, "Account Created");
            }
            else
            {
                var checkUser = await roleManager.FindByNameAsync("User");
                if (checkUser is null)
                    await roleManager.CreateAsync(new IdentityRole() { Name = "User" });

                await userManager.AddToRoleAsync(newUser, "User");
                return new GeneralResposne(true, "Account Created");
                
            }
        }

        public async Task<ServiceResponse.LoginResposne> LoginAccount(LoginDto loginDto)
        {
            if (loginDto == null)
                return new LoginResposne(false, null, "Login container is empty");

            var getUser = await userManager.FindByEmailAsync(loginDto.Email);
            if(getUser is null)
                return new LoginResposne(false, null, "User not found");

            bool checkUserPassword = await userManager.CheckPasswordAsync(getUser, loginDto.Password);
            if (!checkUserPassword)
                return new LoginResposne(false, null, "Invalid email/password");

            var getUserRole = await userManager.GetRolesAsync(getUser);
            var userSession = new UserSession(getUser.Id,getUser.Name, getUser.Email, getUserRole.First());
            string token = GenerateToken(userSession);
            return new LoginResposne(true, token, "Login completed");
        }

        private string GenerateToken(UserSession user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
              };

            var token = new JwtSecurityToken(config["Jwt:Issuer"],
                config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

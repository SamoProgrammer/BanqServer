using Banq.Authentication;
using Banq.Authentication.Models;
using Banq.Database.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace Banq.Controllers
{
    [EnableCors("CORSPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;


        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;

        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {

            var user = await userManager.FindByNameAsync(model.Username);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ByYM000OLlMQG6VVVp1OH7Xzyr7gHuw1qvUC5dcGt3SNM"));

                var token = new JwtSecurityToken(
                    issuer: "http://localhost:61955",
                    audience: "http://localhost:4200",
                    expires: DateTime.Now.AddDays(2),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    userId = user.Id.ToString(),
                    roles = userRoles
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("Register-Manager")]
        public async Task<IActionResult> RegisterManager([FromBody] ManagerRegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            Manager user = new Manager()
            {
                Biography = model.Biography,
                PhoneNumber = model.PhoneNumber,
                Name = model.Name,
                Family = model.Family,
                UserName = model.Username,
                PersonnelCode = model.PersonnelCode
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await roleManager.RoleExistsAsync(UserRoles.Manager))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Manager));
            if (!await roleManager.RoleExistsAsync(UserRoles.Teacher))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Teacher));
            if (!await roleManager.RoleExistsAsync(UserRoles.Supervisor))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Supervisor));

            if (await roleManager.RoleExistsAsync(UserRoles.Manager))
            {
                await userManager.AddToRoleAsync(user, UserRoles.Manager);
            }
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("Register-Teacher")]
        public async Task<IActionResult> RegisterTeacher([FromBody] TeacherRegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            Teacher user = new Teacher()
            {
                Biography = model.Biography,
                PhoneNumber = model.PhoneNumber,
                Name = model.Name,
                Family = model.Family,
                UserName = model.Username,
                PersonnelCode = model.PersonnelCode,
                WantsToCheckOtherQuestions = model.WantsToCheckOtherQuestions
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await roleManager.RoleExistsAsync(UserRoles.Manager))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Manager));
            if (!await roleManager.RoleExistsAsync(UserRoles.Teacher))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Teacher));
            if (!await roleManager.RoleExistsAsync(UserRoles.Supervisor))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Supervisor));

            if (await roleManager.RoleExistsAsync(UserRoles.Teacher))
            {
                await userManager.AddToRoleAsync(user, UserRoles.Teacher);
            }
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("Register-Admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new ApplicationUser()
            {
                PhoneNumber = model.PhoneNumber,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await roleManager.RoleExistsAsync(UserRoles.Manager))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Manager));
            if (!await roleManager.RoleExistsAsync(UserRoles.Teacher))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Teacher));
            if (!await roleManager.RoleExistsAsync(UserRoles.Supervisor))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Supervisor));

            if (await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await userManager.AddToRoleAsync(user, UserRoles.Admin);
            }

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("Register-Supervisor")]
        public async Task<IActionResult> RegisterSupervisor([FromBody] RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new ApplicationUser()
            {
                PhoneNumber = model.PhoneNumber,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await roleManager.RoleExistsAsync(UserRoles.Manager))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Manager));
            if (!await roleManager.RoleExistsAsync(UserRoles.Teacher))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Teacher));
            if (!await roleManager.RoleExistsAsync(UserRoles.Supervisor))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Supervisor));

            if (await roleManager.RoleExistsAsync(UserRoles.Supervisor))
            {
                await userManager.AddToRoleAsync(user, UserRoles.Supervisor);
            }

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }


    }
}

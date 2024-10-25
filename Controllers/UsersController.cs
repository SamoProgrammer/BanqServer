﻿using Banq.Authentication;
using Banq.Authentication.Models;
using Banq.Database;
using Banq.Database.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly DatabaseContext _context;


        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IWebHostEnvironment hostEnvironment, DatabaseContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _hostEnvironment = hostEnvironment;
            _context = context;

        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {

            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var userRole = userRoles.First();

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, userRoles.First())
                };

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ByYM000OLlMQG6VVVp1OH7Xzyr7gHuw1qvUC5dcGt3SNM"));

                var token = new JwtSecurityToken(
                    issuer: "http://localhost:61955",
                    audience: "http://localhost:4200",
                    expires: DateTime.Now.AddDays(2),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );
                switch (userRole)
                {
                    case UserRoles.Teacher:
                        Teacher teacher = await _context.Teachers.Include(x => x.User).Where(x => x.User.UserName == user.UserName).FirstAsync();
                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo,
                            userId = user.Id.ToString(),
                            roles = userRoles,
                            bio = teacher.Biography,
                            name = teacher.Name,
                            family = teacher.Family
                        });
                    case UserRoles.Manager:
                        Manager manager = await _context.Managers.Include(x => x.User).Where(x => x.User.UserName == user.UserName).FirstAsync();
                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo,
                            userId = user.Id.ToString(),
                            roles = userRoles,
                            bio = manager.Biography,
                            name = manager.Name,
                            family = manager.Family
                        });
                    case UserRoles.Admin:
                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo,
                            userId = user.Id.ToString(),
                            roles = userRoles,
                            username = user.UserName
                        });
                    case UserRoles.Supervisor:
                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo,
                            userId = user.Id.ToString(),
                            roles = userRoles,
                            username = user.UserName
                        });
                }


            }
            return Unauthorized();
        }

        [Authorize(Roles = $"{UserRoles.Manager},{UserRoles.Teacher}")]
        [HttpPost("UploadUserImage")]
        public async Task<IActionResult> UploadUserImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file was uploaded");
            }


            var fileExtension = Path.GetExtension(file.FileName).ToLower();

            switch (fileExtension)
            {
                case ".png":
                    break; ;
                default:
                    return BadRequest("Only png allowed");
            }
            string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "UserImages");

            string filePath = Path.Combine(uploadsFolder, User.Claims.Where(x => x.Type == ClaimTypes.Name).First() + fileExtension);
            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return Ok();
        }

        [Authorize(Roles = $"{UserRoles.Manager},{UserRoles.Teacher}")]
        [HttpPost("GetUserImage")]
        public async Task<IActionResult> GetUserImage()
        {
            string userImagesFolder = Path.Combine(_hostEnvironment.WebRootPath, "UserImages");
            if (System.IO.File.Exists(userImagesFolder + "/" + User.Claims.Where(x => x.Type == ClaimTypes.Name).First() + ".png"))
            {
                var fileStream = System.IO.File.OpenRead(userImagesFolder + "/" + User.Claims.Where(x => x.Type == ClaimTypes.Name).First() + ".png");
                return File(fileStream, "application/octet-stream", fileStream.Name, enableRangeProcessing: true);
            }
            else
            {
                return NotFound();
            }
        }



        [HttpPost]
        [Route("Register-Manager")]
        public async Task<IActionResult> RegisterManager([FromBody] ManagerRegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            var userResult = await _userManager.CreateAsync(new ApplicationUser
            {
                PhoneNumber = model.PhoneNumber,
                UserName = model.Username
            }, model.Password);

            if (!userResult.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.Manager))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Manager));
            if (!await _roleManager.RoleExistsAsync(UserRoles.Teacher))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Teacher));
            if (!await _roleManager.RoleExistsAsync(UserRoles.Supervisor))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Supervisor));

            var user = await _userManager.FindByNameAsync(model.Username);

            if (await _roleManager.RoleExistsAsync(UserRoles.Manager))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Manager);
            }

            Manager manager = new Manager()
            {
                Biography = model.Biography,
                User = user,
                Name = model.Name,
                Family = model.Family,
                PersonnelCode = model.PersonnelCode
            };

            await _context.Managers.AddAsync(manager);
            await _context.SaveChangesAsync();
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("Register-Teacher")]
        public async Task<IActionResult> RegisterTeacher([FromBody] TeacherRegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            var userResult = await _userManager.CreateAsync(new ApplicationUser
            {
                PhoneNumber = model.PhoneNumber,
                UserName = model.Username
            }, model.Password);

            if (!userResult.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.Manager))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Manager));
            if (!await _roleManager.RoleExistsAsync(UserRoles.Teacher))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Teacher));
            if (!await _roleManager.RoleExistsAsync(UserRoles.Supervisor))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Supervisor));

            var user = await _userManager.FindByNameAsync(model.Username);

            await _userManager.AddToRoleAsync(user, UserRoles.Teacher);

            Teacher teacher = new Teacher()
            {
                Biography = model.Biography,
                Name = model.Name,
                User = user,
                Family = model.Family,
                PersonnelCode = model.PersonnelCode,
                WantsToCheckOtherQuestions = model.WantsToCheckOtherQuestions
            };

            await _context.Teachers.AddAsync(teacher);
            await _context.SaveChangesAsync();
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("Register-Admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new ApplicationUser()
            {
                PhoneNumber = model.PhoneNumber,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.Manager))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Manager));
            if (!await _roleManager.RoleExistsAsync(UserRoles.Teacher))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Teacher));
            if (!await _roleManager.RoleExistsAsync(UserRoles.Supervisor))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Supervisor));

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("Register-Supervisor")]
        public async Task<IActionResult> RegisterSupervisor([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new ApplicationUser()
            {
                PhoneNumber = model.PhoneNumber,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.Manager))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Manager));
            if (!await _roleManager.RoleExistsAsync(UserRoles.Teacher))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Teacher));
            if (!await _roleManager.RoleExistsAsync(UserRoles.Supervisor))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Supervisor));

            if (await _roleManager.RoleExistsAsync(UserRoles.Supervisor))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Supervisor);
            }

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> CheckToken()
        {
            var username = User.Claims.Where(x => x.Type == ClaimTypes.Name).First().Value;
            var user = await _userManager.FindByNameAsync(username);
            List<String> userRoles = new List<string>();
            foreach (var claim in User.Claims)
            {
                if (claim.Type == ClaimTypes.Role)
                {
                    userRoles.Add(claim.Value);
                }
            }
            switch (userRoles[0])
            {
                case UserRoles.Teacher:
                    Teacher teacher = await _context.Teachers.Include(x => x.User).Where(x => x.User.UserName == user.UserName).FirstAsync();
                    return Ok(new
                    {
                        userId = user.Id.ToString(),
                        roles = userRoles,
                        bio = teacher.Biography,
                        name = teacher.Name,
                        family = teacher.Family
                    });
                case UserRoles.Manager:
                    Manager manager = await _context.Managers.Include(x => x.User).Where(x => x.User.UserName == user.UserName).FirstAsync();
                    return Ok(new
                    {

                        userId = user.Id.ToString(),
                        roles = userRoles,
                        bio = manager.Biography,
                        name = manager.Name,
                        family = manager.Family
                    });
                case UserRoles.Admin:
                    return Ok(new
                    {

                        userId = user.Id.ToString(),
                        roles = userRoles,
                        username = user.UserName
                    });
                case UserRoles.Supervisor:
                    return Ok(new
                    {

                        userId = user.Id.ToString(),
                        roles = userRoles,
                        username = user.UserName
                    });
                default:
                    return Ok();
            }
        }
    }
}

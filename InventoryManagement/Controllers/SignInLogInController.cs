using InventoryManagement.Data;
using InventoryManagement.Models.DTO;
using InventoryManagement.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignInLogInController : ControllerBase
    {

        private readonly InventoryManagementDbContext dbcontext;

        public SignInLogInController(InventoryManagementDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        [HttpPost("UserRegister")]

        public IActionResult UserRegister(UserSignInDto userSignInDto)
        {

            try
            {
                var existingUserName = dbcontext.UserAccounts.FirstOrDefault(u => u.UserName == userSignInDto.UserName);

                if (existingUserName != null)
                {
                    return BadRequest(new { message = $"User with the same username already exists." });
                }

                var existingEmail = dbcontext.UserAccounts.FirstOrDefault(u => u.Email == userSignInDto.Email);

                if (existingEmail != null)
                {
                    return BadRequest(new { message = $"User with the same email already exists." });
                }

                var userAccount = new userAccount
                {
                    UserId = Guid.NewGuid(),
                    UserName = userSignInDto.UserName,
                    Password = userSignInDto.Password,
                    Email = userSignInDto.Email,
                    PhoneNumber = userSignInDto.PhoneNumber,
                    CreatedBy = userSignInDto.CreatedBy,
                    CreatedDate = DateTime.UtcNow,
                    IsActive = true
                };

                dbcontext.UserAccounts.Add(userAccount);

                var userRole = new userRole
                {
                    UserRoleId = Guid.NewGuid().ToString(),
                    UserId = userAccount.UserId,
                    RoleId = userSignInDto.RoleId,
                    CreatedBy = userSignInDto.CreatedBy,
                    CreatedDate = DateTime.UtcNow,
                    LastUpdatedBy = userSignInDto.CreatedBy,
                    IsActive = true
                };

                dbcontext.UserRoles.Add(userRole);
                dbcontext.SaveChanges();

                return Ok(new
                {
                    message = "User registered successfully.",
                    UserName = userSignInDto.UserName,
                    Password = userSignInDto.Password,
                    Email = userSignInDto.Email,
                    PhoneNumber = userSignInDto.PhoneNumber,
                    RoleId = userRole.RoleId,
                    CreatedBy = userSignInDto.CreatedBy

                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Internal server error: {ex.Message}" });
            }
        }

        [HttpPost("UserLogin")]

        public IActionResult UserLogin(UserLogInDto userLogInDto)
        {
            try
            {
                var user = dbcontext.UserAccounts.FirstOrDefault(u => u.UserName == userLogInDto.UserName && u.Password == userLogInDto.Password);
                if (user == null)
                {
                    return NotFound(new { message = $"Invalid username or password." });
                }

                var userRole = dbcontext.UserRoles.FirstOrDefault(ur => ur.UserId == user.UserId);
                if (userRole == null)
                {
                    return NotFound(new { message = "User not found!" });
                }

                string roleName = "";

                switch (userRole.RoleId)
                {
                    case 1:
                        roleName = "Owner";
                        break;
                    case 2:
                        roleName = "Admin";
                        break;
                    case 3:
                        roleName = "Department Head";
                        break;
                    case 4:
                        roleName = "Marketing";
                        break;
                    case 5:
                        roleName = "HR";
                        break;
                    case 6:
                        roleName = "Employee";
                        break;
                    case 7:
                        roleName = "Customer";
                        break;

                    default:
                        return BadRequest(new { message = "Invalid user role!" });
                }

                return Ok(new
                {
                    message = "User logged in successfully.",
                    UserName = user.UserName,
                    roleName = roleName,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Internal server error: {ex.Message}" });
            }
        }

        [HttpPut("UpdateUser/{UserName}")]

        public IActionResult UpdateUser([FromRoute] string UserName, [FromBody] UserSignInDto userSignInDto)
        {
            try
            {
                var user = dbcontext.UserAccounts.FirstOrDefault(u => u.UserName == UserName);

                if (user == null)
                {
                    return NotFound(new { message = $"User not found." });
                }

                user.UserName = userSignInDto.UserName;
                user.Password = userSignInDto.Password;
                user.Email = userSignInDto.Email;
                user.PhoneNumber = userSignInDto.PhoneNumber;

                dbcontext.SaveChanges();

                return Ok(new { message = "User Updated Successfully!" });
            }

            catch (Exception ex)
            {
                return BadRequest(new { message = $"Internal server error: {ex.Message}" });
            }
        }

        [HttpDelete("DeleteUser/{UserName}")]

        public IActionResult DeleteUser([FromRoute] string UserName)
        {
            try
            {
                var user = dbcontext.UserAccounts.FirstOrDefault(u => u.UserName == UserName);
                if (user == null)
                {
                    return NotFound(new { message = $"User not found." });
                }

                user.IsActive = false; // Mark the user as inactive instead of deleting it

                var userRole = dbcontext.UserRoles.FirstOrDefault(ur => ur.UserId == user.UserId);

                if (userRole != null)
                {
                    userRole.IsActive = false;
                }

                dbcontext.SaveChanges();

                return Ok(new { message = "User Deleted Successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Internal server error: {ex.Message}" });
            }
        }
    }
}

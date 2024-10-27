using CompanyApp.Application.Interfaces;
using CompanyApp.Domain.Dto.UserDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CompanyApp.Presentation.Controllers
{
    /// <summary>
    /// User Controlle
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICompanyService _companyService;
        private readonly IRoleService _roleService;
        /// <summary>
        /// initialize IUserService
        /// initialize ICompanyService
        /// initialize IRoleService
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="companyService"></param>
        /// <param name="roleService"></param>
        public UserController(IUserService userService, ICompanyService companyService, IRoleService roleService)
        {
            _userService = userService;
            _companyService = companyService;
            _roleService = roleService;
        }

        /// <summary>
        /// Get all company users for admin only
        /// </summary>
        /// <returns>List of Users</returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("GetUsers")]
        [ProducesResponseType(typeof(IEnumerable<UserDto>) ,StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var data = await _userService.GetAll();

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        /// <summary>
        /// List all flat users of a company for user accounts only
        /// </summary>
        /// <returns>List of users only</returns>
        [Authorize(Roles = "User")]
        [HttpGet("GetUsers/UserOnly")]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetUserListForUserAccount()
        {
            try
            {
                var companyId = User.FindFirstValue("CompanyId");
                var data = await _userService.GetAllForUserAccount(Convert.ToInt32(companyId));
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        /// <summary>
        /// Add company user for admin only
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("AddUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> AddUser([FromBody] AddUpdateUserDto dto)
        {
            try
            {
                var existingUser = await _userService.GetUserByUsername(dto.Username);
                if (existingUser != null)
                {
                    return BadRequest("Username already exists.");
                }

                var role = await _roleService.GetRoleById((int)dto.Role);
                if (role == null)
                {
                    return BadRequest("Role does not exist");
                }

                var company = await _companyService.GetCompanyById(dto.CompanyId);
                if (company == null)
                {
                    return BadRequest("Company not found.");
                }

                await _userService.Add(dto);
                return Ok(new { Message = "User registered successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        /// <summary>
        /// Update company user for admin only
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> UpdateUser([FromQuery] Guid id, [FromBody] AddUpdateUserDto dto)
        {
            try
            {
                var existingUser = await _userService.GetById(id);
                if (existingUser == null)
                {
                    return BadRequest("No User Found.");
                }

                var role = await _roleService.GetRoleById((int)dto.Role);
                if (role == null)
                {
                    return BadRequest("Role does not exist");
                }

                var existingUserName = await _userService.CheckExistIfUserName(dto.Username, id);
                if (existingUserName)
                {
                    return BadRequest("Username already exists.");
                }

                var company = await _companyService.GetCompanyById(dto.CompanyId);
                if (company == null)
                {
                    return BadRequest("Company not found.");
                }
                
                await _userService.Update(dto, id);
                return Ok(new { Message = "User updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        /// <summary>
        /// Delete company users for admin only
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteUser([FromQuery] Guid id)
        {
            try
            {
                var user = await _userService.GetById(id);
                if (user == null)
                {
                    return BadRequest("No user found.");
                }

                await _userService.Delete(id);
                return Ok(new { Message = "User deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}

using APIBaseline.DTOs;
using APIBaseline.Models;
using APIBaseline.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIBaseline.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;
		private readonly IUserRoleService _userRoleService;

		public UserController(IUserService userService, IUserRoleService userRoleService)
		{
			_userService = userService;
			_userRoleService = userRoleService;
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetUser(int id)
		{
			var user = await _userService.GetUserAsync(id);
			return user == null ? NotFound() : Ok(user);
		}

		[HttpGet]
		public async Task<IActionResult> GetUsers([FromQuery] string nameFilter, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
		{
			var users = await _userService.GetUsersAsync(nameFilter, pageNumber, pageSize);
			return Ok(users);
		}

		[HttpPost]
		public async Task<IActionResult> CreateUser([FromBody] UserRoleDTO request)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);

			var user = new User
			{
				Id = 2, //You can make it into GUID or integer auto increment
				Name = request.Name,
			};

			var roles = new List<UserRole>();
			foreach (var role in request.Roles)
			{
				roles.Add(new UserRole
				{
					Id = 2, //You can make it into GUID or integer auto increment
					RoleName = role,
					UserId = user.Id
				});
			}

			await _userService.CreateUserAsync(user, roles);
			return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
		{
			if (id != user.Id) return BadRequest();

			await _userService.UpdateUserAsync(user);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteUser(int id)
		{
			await _userService.DeleteUserAsync(id);
			return NoContent();
		}
	}
}

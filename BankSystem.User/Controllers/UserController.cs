using BankSystem.DTOS;
using BankSystem.Entities;
using BankSystem.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUsersService _usersService) : ControllerBase
    {

        // GET: api/Users
        [HttpGet]
            public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
            {
                var users = await _usersService.GetAllAsync();
                return Ok(users);
            }

            // GET: api/Users/username
            [HttpGet("{userName}")]
            public async Task<ActionResult<User>> GetUser(string userName)
            {
                var user = await _usersService.FindAsync(userName);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }

            // POST: api/Users/authenticate
            [HttpPost("authenticate")]
            public async Task<ActionResult<User>> Authenticate([FromBody] LoginRequest request)
            {
                var user = await _usersService.FindAsync(request.UserName, request.Password);

                if (user == null)
                {
                    return Unauthorized("Invalid username or password");
                }

                return Ok(user);
            }

            // POST: api/Users
            [HttpPost]
            public async Task<ActionResult<User>> CreateUser([FromBody] User user)
            {
                if (string.IsNullOrEmpty(user.UserName))
                {
                    return BadRequest("Username is required");
                }

                var success = await _usersService.AddAsync(user);

                if (!success)
                {
                    return Conflict("Username already exists");
                }

                return CreatedAtAction(nameof(GetUser), new { userName = user.UserName }, user);
            }

            // PUT: api/Users/username
            [HttpPut("{userName}")]
            public async Task<IActionResult> UpdateUser(string userName, [FromBody] User user)
            {
                if (userName != user.UserName)
                {
                    return BadRequest("Username mismatch");
                }

                var success = await _usersService.UpdateAsync(user);

                if (!success)
                {
                    return NotFound();
                }

                return NoContent();
            }

            // DELETE: api/Users/username
            [HttpDelete("{userName}")]
            public async Task<IActionResult> DeleteUser(string userName)
            {
                var success = await _usersService.DeleteAsync(userName);

                if (!success)
                {
                    return NotFound();
                }

                return NoContent();
            }
        }
    }



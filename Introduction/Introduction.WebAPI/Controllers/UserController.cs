using Introduction.Common;
using Introduction.Model;
using Introduction.Service;
using Introduction.Service.Common;
using Introduction.WebAPI.RestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Npgsql;
namespace Introduction.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] User user)
        {
            try
            {
                bool successfulPost = await _service.PostAsync(user);
                if(!successfulPost)
                    return BadRequest();
                return Ok("Adding successful...");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            try
            {              
                bool successfulDelete = await _service.DeleteAsync(id);
                if (!successfulDelete)
                    return BadRequest();
                return Ok("Deleting successful...");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateUser/{id}")]
        public async Task<ActionResult> PutAsync(Guid id, User user)
        {
            try
            {
                bool successfulPut = await _service.PutAsync(id, user);
                if (!successfulPut)
                    return BadRequest();
                return Ok("Update successful...");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult> GetAsync(Guid id)
        {
            try
            {
                User user = new User();
                user = await _service.GetAsync(id);             
               
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetUserAndPosts/{id}")]
        public async Task<ActionResult> GetUserAndPostsAsync(Guid id)
        {
            try
            {
                List<FacebookPost> posts = new List<FacebookPost>();
                posts = await _service.GetUserAndPostsAsync(id);
                
                if (posts == null)
                {
                    return NotFound();
                }
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAllAsync()
        {
            try
            {
                List<User> users = new List<User>();
                users = await _service.GetAllAsync();

                if (users == null)
                {
                    return NotFound();
                }
                return Ok(users);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
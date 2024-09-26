using Autofac.Core;
using Introduction.Model;
using Introduction.Service;
using Introduction.Service.Common;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Introduction.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FacebookPostController : ControllerBase
    {
        private IFacebookPostService _service;
        public FacebookPostController(IFacebookPostService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] FacebookPost facebookPost)
        {
            try
            {
                bool successfulPost = await _service.PostAsync(facebookPost);
                if (!successfulPost)
                    return BadRequest();
                return Ok("Adding successful...");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
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

        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(Guid id, FacebookPost facebookPost)
        {
            try
            {
                if (!await _service.PutAsync(id, facebookPost))
                    return BadRequest();
                return Ok("Update successful...");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                FacebookPost facebookPost = new FacebookPost();
                facebookPost = await _service.GetByIdAsync(id);

                if (facebookPost == null)
                {
                    return NotFound();
                }
                return Ok(facebookPost);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User.Domain.Dtos;
using User.Domain.Services;
using User_GraphQL.Schema.Mutations;

namespace User_GraphQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginInputDto payload)
        {
            try
            {
                var result  =  await  _service.Login(payload);
                return Ok(result);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"Error : {ex.Message}");
            }
            return BadRequest();
            
        }
    }
}

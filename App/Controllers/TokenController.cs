using App.Entities;
using App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.Controllers
{
    [Route("ws/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly JwtService _jwtService;

        public TokenController(JwtService jwtService)
        {
            this._jwtService = jwtService;
        }

        // POST: api/Token/Authenticate
        /// <summary>
        /// Gerar Token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Route("Authenticate")]
        [AllowAnonymous]
        [HttpPost]                
        public ActionResult<ReturnDTO> AuthenticateUser([FromBody] UserWs user)
        {
            Task<ReturnDTO> returnDTO = this._jwtService.AuthenticateUser(user);

            if (returnDTO.Result.IsSuccess)
            {
                return new OkObjectResult(returnDTO.Result);
            }

            return new BadRequestObjectResult(returnDTO.Result);
        }
    }
}

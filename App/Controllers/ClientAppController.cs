using App.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[ApiExplorerSettings(IgnoreApi = true)]
    public class ClientAppController : ControllerBase
    {
        // GET: api/ClientApp/IsTest
        /// <summary>
        /// End Point IsTest
        /// </summary>
        /// <param></param>
        /// <returns>IsTest<returns>
        [Route("IsTest")]
        [Authorize]
        [HttpGet]
        public ActionResult<ReturnDTO> IsTest()
        {
            return new ReturnDTO(true, "OK", null);
        }
    }
}

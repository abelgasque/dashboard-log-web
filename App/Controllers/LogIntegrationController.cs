using App.Entities;
using App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.Controllers
{
    [Route("ws/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class LogIntegrationController : ControllerBase
    {
        private readonly LogIntegrationService _logIntegrationService;

        public LogIntegrationController(LogIntegrationService logIntegrationService)
        {
            this._logIntegrationService = logIntegrationService;
        }

        // GET: ws/LogIntegration/GetLogIntegrationForChartDynamic
        /// <summary>
        /// End Point que exibe grafico de linha mensal do historico do sistema
        /// </summary>
        [Route("GetLogIntegrationForChartDynamic")]
        [Authorize]
        [HttpGet]
        public ActionResult<ReturnDTO> GetLogIntegrationForChartMonth([FromQuery]  bool pMustFilterYear)
        {
            Task<ReturnDTO> returnDTO = this._logIntegrationService.GetLogIntegrationForChartDynamic(pMustFilterYear);

            if (returnDTO.Result.IsSuccess)
            {
                return new OkObjectResult(returnDTO.Result);
            }

            return new BadRequestObjectResult(returnDTO.Result);
        }

        // GET: ws/LogIntegration/GetLogIntegrationDay
        /// <summary>
        /// End Point consulta de logs diarios do sistema
        /// </summary>
        [Route("GetLogIntegrationDay")]
        [Authorize]
        [HttpGet]
        public ActionResult<ReturnDTO> GetLogIntegrationDay()
        {
            Task<ReturnDTO> returnDTO = this._logIntegrationService.GetLogIntegrationDay();

            if (returnDTO.Result.IsSuccess)
            {
                return new OkObjectResult(returnDTO.Result);
            }

            return new BadRequestObjectResult(returnDTO.Result);
        }
    }
}

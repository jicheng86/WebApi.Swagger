using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using WebApi.Swagger.DomainModel;
using WebApi.Swagger.Service;

namespace WebApi.Swagger.Controllers
{
    /// <summary>
    /// JwtAuthentication jwt验证控制器
    /// </summary>
    [Route("api/JwtAuthentication")]
    [ApiController]
    public class JwtAuthenticationController : ControllerBase
    {
        private readonly ILogger<JwtAuthenticationController> logger;
        private readonly IJwtAuthenticateService jwtAuthenticateService;

        public JwtAuthenticationController(ILogger<JwtAuthenticationController> logger, IJwtAuthenticateService jwtAuthenticateService)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.jwtAuthenticateService = jwtAuthenticateService ?? throw new ArgumentNullException(nameof(jwtAuthenticateService));
        }

        [AllowAnonymous]
        [HttpPost(), Route("requestToken")]
        public ActionResult RequestToken([FromBody] RequestDTO request)
        {
            logger.LogDebug("LogDebug日志，请求JwtAuthenticateService/RequestToken");
            logger.LogInformation("LogInformation日志，请求JwtAuthenticateService/RequestToken");
            logger.LogWarning("ogWarning日志，请求JwtAuthenticateService/RequestToken");
            logger.LogError("LogError日志，请求JwtAuthenticateService/RequestToken");
            logger.LogCritical("请求LogError日志，请求JwtAuthenticateService/RequestToken");
            logger.LogTrace("LogError日志，请求JwtAuthenticateService/RequestToken");
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Request");
            }

            string token;
            if (jwtAuthenticateService.IsAuthenticated(request, out token))
            {
                return Ok(token);
            }

            return BadRequest("Invalid Request");
        }

    }
}
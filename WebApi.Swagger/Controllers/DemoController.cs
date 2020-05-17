using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Swagger.Controllers
{
    /// <summary>
    /// Demo地址
    /// </summary>
    [Route("api/v1/Demo")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly ILogger<DemoController> logger;

        public DemoController(ILogger<DemoController> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        /// <summary>
        /// GET: api/Demo
        /// </summary>
        /// <returns>字符串数组</returns>
        [HttpGet(Name = nameof(GetValues))]
        // [Authorize]
        public IEnumerable<string> GetValues()
        {
            logger.LogDebug("请求LogDebug日志，GetValues", nameof(GetValues));
            logger.LogInformation("请求LogInformation日志，GetValues", nameof(GetValues));
            logger.LogWarning("请求LogWarning日志，GetValues", nameof(GetValues));
            logger.LogError("请求LogError日志，GetValues", nameof(GetValues));
            logger.LogCritical("请求LogError日志，GetValues", nameof(GetValues));
            logger.LogTrace("请求LogError日志，GetValues", nameof(GetValues));
            return new string[] { "value1", "value2", "3", "4" };
        }

        // GET: api/Demo/5
        /// <summary>
        /// 获取value
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>value</returns>
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Demo
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Demo/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

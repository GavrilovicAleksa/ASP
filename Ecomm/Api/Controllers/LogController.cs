using Application;
using Application.Queries.Log;
using Application.Searches;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : Controller
    {
        private readonly UseCaseExecutor executor;
         
        public LogController(UseCaseExecutor executor)
        {
            this.executor = executor;
        }

        [HttpGet]
        public IActionResult Get(
            [FromQuery] LogSearch search, [FromServices] ISearchLogsQuery query)
        {
            return Ok(executor.ExecuteQuery(query, search));
        }
    }
}

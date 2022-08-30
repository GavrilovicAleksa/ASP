using Application;
using Application.Commands.Brand;
using Application.DataTransfer;
using Application.DataTransfer.BrandDataTransfer;
using Application.Queries.Brand;
using Application.Searches;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/brands")]
    [ApiController]
    public class BrandController : Controller
    {
        private readonly UseCaseExecutor executor;

        public BrandController(UseCaseExecutor executor)
        {
            this.executor = executor;
        }

        [HttpGet]
        public IActionResult Get(
            [FromQuery] BrandSearch search, [FromServices] ISearchBrandsQuery query)
        {
            return Ok(executor.ExecuteQuery(query, search));
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(int id,
            [FromServices] BrandSearch search, [FromServices] IGetSingleBrandQuery query)
        {
            search.Id = id;
            return Ok(executor.ExecuteQuery(query, search));
        }

        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] BrandDto dto, [FromServices] ICreateBrandCommand command)
        {
            executor.ExecuteCommand(command, dto);
            return StatusCode(201);
        }

        [Authorize]
        [HttpPut]
        public IActionResult Put(BrandDto dto, [FromServices] IUpdateBrandCommand command)
        { 
            executor.ExecuteCommand(command, dto);
            return StatusCode(204);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromBody] RemoveEntityDto dto, [FromServices] IRemoveBrandCommand command)
        {
            dto.Id = id;
            executor.ExecuteCommand(command, dto);
            return StatusCode(204);
        }
    } 
}

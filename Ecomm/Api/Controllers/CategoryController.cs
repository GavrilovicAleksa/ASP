using Application;
using Application.Commands.Category;
using Application.DataTransfer;
using Application.DataTransfer.Category;
using Application.Queries.Category;
using Application.Searches;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly UseCaseExecutor executor;

        public CategoryController(UseCaseExecutor executor)
        {
            this.executor = executor;
        }

        [HttpGet]
        public IActionResult Get(
            [FromQuery] CategorySearch search,
            [FromServices] ISearchCategoriesQuery query)
        {
            return Ok(executor.ExecuteQuery(query, search));
        } 

        [HttpGet("{id}")]
        public IActionResult Get(int id,
            [FromServices] CategorySearch search,
            [FromServices] ISearchCategoriesQuery query)
        { 
            search.Id = id;
            return Ok(executor.ExecuteQuery(query, search));
        } 
         
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CategoryDto dto,
            [FromServices] ICreateCategoryCommand command)
        {
            executor.ExecuteCommand(command, dto);
            return StatusCode(201);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] CategoryDto dto,
            [FromServices] IUpdateCategoryCommand command)
        {
            executor.ExecuteCommand(command, dto);
            return StatusCode(204); 
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, RemoveEntityDto dto,
            [FromServices] IRemoveCategoryCommand command)
        {
            dto.Id = id;
            executor.ExecuteCommand(command, dto);
            return StatusCode(204);
        }
    }
}

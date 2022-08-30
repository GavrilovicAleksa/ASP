using Application;
using Application.Commands.Product;
using Application.DataTransfer;
using Application.DataTransfer.Product;
using Application.Queries.Product;
using Application.Searches;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/products")]
    public class ProductController : Controller
    {
        private readonly UseCaseExecutor executor;

        public ProductController(UseCaseExecutor executor)
        {
            this.executor = executor;
        }

        [HttpGet]
        //[Authorize]
        public IActionResult Get(
            [FromQuery] ProductSearch search,
            [FromServices] ISearchProductsQuery query)
        {
            return Ok(executor.ExecuteQuery(query, search));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id,
            [FromServices] ProductSearch search,
           [FromServices] IGetSingleProductQuery query)
        {
            search.Id = id;
            return Ok(executor.ExecuteQuery(query, search));
        } 

        [HttpPost]
        [Authorize]
        public void Post([FromBody] ProductDto dto,
            [FromServices] ICreateProductCommand command)
        {
            executor.ExecuteCommand(command, dto);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public void Delete([FromBody] RemoveEntityDto dto,
            [FromServices] IRemoveProductCommand command)
        {
            executor.ExecuteCommand(command, dto);
        } 

        [HttpPut("{id}")] 
        [Authorize]
        public void Put(int id, [FromBody] ProductDto dto,
            [FromServices] IUpdateProductCommand command)
        {
            dto.Id = id; 
            executor.ExecuteCommand(command, dto);
        }
    }
}

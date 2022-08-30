using Application;
using Application.Commands.Cart;
using Application.DataTransfer;
using Application.DataTransfer.CartDataTransfer;
using Application.Queries.Cart;
using Application.Searches;
using Implementation.Commands.Cart;
using Implementation.Queries.Cart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/carts")]
    [ApiController]
    public class CartController : Controller
    {
        private readonly UseCaseExecutor executor;

        public CartController(UseCaseExecutor executor)
        {
            this.executor = executor;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get(
            [FromQuery] CartSearch search,
            [FromServices] IGetCartQuery query)
        { 
            return Ok(executor.ExecuteQuery(query, search));
        }

        [HttpPost]
        public void Post([FromBody] CartDto dto,
            [FromServices] IAddCartItemCommand command)
        {
            executor.ExecuteCommand(command, dto);
        } 

        [HttpDelete("{id}")]
        public void Delete([FromBody] RemoveEntityDto dto,
            [FromServices] IRemoveCartItemCommand command)
        {
            executor.ExecuteCommand(command, dto);
        }
    }
}

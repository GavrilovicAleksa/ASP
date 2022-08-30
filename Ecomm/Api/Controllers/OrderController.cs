using Application;
using Application.Commands.Order;
using Application.DataTransfer;
using Application.DataTransfer.Order;
using Application.Queries.Order;
using Application.Searches;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly UseCaseExecutor executor;

        public OrderController(UseCaseExecutor executor)
        {
            this.executor = executor;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get([FromQuery] OrderSearch search, [FromServices] IGetOrderQuery query)
        {
            return Ok(executor.ExecuteQuery(query, search));
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] OrderDto dto, [FromServices] IAddOrderItemCommand command)
        {
            executor.ExecuteCommand(command, dto);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(int id, [FromBody] OrderDto dto, [FromServices] IUpdateOrderCommand command)
        { 
            dto.Id = id;
            executor.ExecuteCommand(command, dto);
            return StatusCode(200); 
        } 

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int Id, [FromBody] RemoveEntityDto dto, [FromServices] IRemoveOrderCommand command)
        {
            dto.Id = Id;
            executor.ExecuteCommand(command, dto);
            return StatusCode(204);
        }
    }
}

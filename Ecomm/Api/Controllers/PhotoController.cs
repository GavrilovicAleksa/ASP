using Application;
using Application.Commands.Brand;
using Application.Commands.Photo;
using Application.DataTransfer;
using Application.DataTransfer.PhotoDataTransfer;
using Application.Queries.Brand;
using Application.Queries.Photo;
using Application.Searches;
using Implementation.Queries.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/photos")]
    [ApiController]
    public class PhotoController : Controller
    {
        private readonly UseCaseExecutor executor;

        public PhotoController(UseCaseExecutor executor)
        {
            this.executor = executor;
        }

        [HttpGet]
        public IActionResult Get(
            [FromQuery] PhotoSearch search,
            [FromServices] ISearchPhotosQuery query)
        {
            return Ok(executor.ExecuteQuery(query, search));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id, 
            [FromServices] PhotoSearch search,
           [FromServices] IGetSinglePhotoQuery query)
        {
            search.Id = id;
            return Ok(executor.ExecuteQuery(query, search));
        }

        [HttpPost]
        [Authorize]
        public void Post([FromBody] PhotoDto dto,
            [FromServices] ICreatePhotoCommand command)
        {
            executor.ExecuteCommand(command, dto);
        }

        [HttpDelete("{id}")]
        [Authorize] 
        public void Delete(int id, [FromBody] RemoveEntityDto dto,
            [FromServices] IRemovePhotoCommand command)
        {
            dto.Id = id;
            executor.ExecuteCommand(command, dto);
        }

        [HttpPut("{id}")]
        [Authorize] 
        public void Put(int id, PhotoDto dto,
            [FromServices] IUpdatePhotoCommand command)
        {
            dto.Id = id;
            executor.ExecuteCommand(command, dto);
        }
    }
}

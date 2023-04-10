using Application.Features.Addresses.Commands.Add;
using Application.Features.Addresses.Commands.Delete;
using Application.Features.Addresses.Commands.HardDelete;
using Application.Features.Addresses.Commands.Update;
using Application.Features.Addresses.Queries.GetAll;
using Application.Features.Addresses.Queries.GetById;
using Application.Features.Cities.Queries.GetAll;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class AddressesController : ApiControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddAsync(AddressAddCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAllAsync(AddressGetAllQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            return Ok(await Mediator.Send(new AddressGetByIdQuery(id, null)));
        }

        [HttpPut("Update {id}")]
        public async Task<IActionResult> UpdateAsync(AddressUpdateCommand command, Guid id)
        {
            if(id != command.Id)
            {
                return BadRequest();
            }

            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("Delete {id}")]
        public async Task<IActionResult> DeleteAsync(AddressUpdateCommand command, Guid id)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            return Ok(await Mediator.Send(command));
        }
        
        [HttpDelete("HardDelete {id}")]
        public async Task<IActionResult> HardDeleteAsync(Guid id)
        {
            return Ok(await Mediator.Send(new AddressHardDeleteCommand(id)));
        }
    }
}

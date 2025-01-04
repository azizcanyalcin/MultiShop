﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Application.Features.CQRS.Handlers.AdressHandlers;
using MultiShop.Order.Application.Features.CQRS.Queries.AddressQueires;

namespace MultiShop.Order.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly GetAddressQueryHandler getAddressQueryHandler;
        private readonly GetAddressByIdQueryHandler getAddressByIdQueryHandler;
        private readonly CreateAddressCommandHandler createAddressCommandHandler;
        private readonly UpdateAddressCommandHandler updateAddressCommandHandler;
        private readonly DeleteAddressCommandHandler deleteAddressCommandHandler;

        public AddressesController(GetAddressQueryHandler getAddressQueryHandler, GetAddressByIdQueryHandler getAddressByIdQueryHandler, CreateAddressCommandHandler createAddressCommandHandler, UpdateAddressCommandHandler updateAddressCommandHandler, DeleteAddressCommandHandler deleteAddressCommandHandler)
        {
            this.getAddressQueryHandler = getAddressQueryHandler;
            this.getAddressByIdQueryHandler = getAddressByIdQueryHandler;
            this.createAddressCommandHandler = createAddressCommandHandler;
            this.updateAddressCommandHandler = updateAddressCommandHandler;
            this.deleteAddressCommandHandler = deleteAddressCommandHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAddresses()
        {
            var values = await getAddressQueryHandler.Handle();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddressById(int id)
        {
            var values = await getAddressByIdQueryHandler.Handle(new GetAddressByIdQuery(id));
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAddress(CreateAddressCommand command)
        {

            await createAddressCommandHandler.Handle(command);

            return Ok("Address added succesfully!");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAddress(UpdateAddressCommand command)
        {
            await updateAddressCommandHandler.Handle(command);

            return Ok("Address updated succesfully!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            await deleteAddressCommandHandler.Handle(new DeleteAddressCommand(id));

            return Ok("Address deleted succesfully!");
        }

    }
}

using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.AdressHandlers
{
    public class DeleteAddressCommandHandler
    {
        private readonly IRepository<Address> repository;

        public DeleteAddressCommandHandler(IRepository<Address> repository)
        {
            this.repository = repository;
        }

        public async Task Handle(DeleteAddressCommand command)
        {
            var value = await repository.GetByIdAsync(command.AddressId);

            await repository.DeleteAsync(value);
        }
    }
}

using MultiShop.Order.Application.Features.CQRS.Queries.AddressQueires;
using MultiShop.Order.Application.Features.CQRS.Results.AddressResults;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.AdressHandlers
{
    public class GetAddressByIdQueryHandler
    {
        private readonly IRepository<Address> repository;

        public GetAddressByIdQueryHandler(IRepository<Address> repository)
        {
            this.repository = repository;
        }
        public async Task<GetAddressByIdQueryResult> Handle(GetAddressByIdQuery query)
        {
            var values = await repository.GetByIdAsync(query.AddressId);

            return new GetAddressByIdQueryResult
            {
                AddressId = values.AddressId,
                City = values.City,
                Detail = values.Detail,
                District = values.District,
                UserId = values.UserId
            };
        }
    }
}

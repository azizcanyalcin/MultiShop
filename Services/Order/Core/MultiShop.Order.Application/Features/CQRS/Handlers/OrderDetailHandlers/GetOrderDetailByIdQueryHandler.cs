using MultiShop.Order.Application.Features.CQRS.Queries.OrderDetailQueries;
using MultiShop.Order.Application.Features.CQRS.Results.OrderDetailResults;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers
{
    public class GetOrderDetailByIdQueryHandler
    {
        private readonly IRepository<OrderDetail> repository;

        public GetOrderDetailByIdQueryHandler(IRepository<OrderDetail> repository)
        {
            this.repository = repository;
        }
        public async Task<GetOrderDetailByIdQueryResult> Handle(GetOrderDetailByIdQuery query)
        {
            var values = await repository.GetByIdAsync(query.OrderDetailId);

            return new GetOrderDetailByIdQueryResult
            {
                OrderDetailId = values.OrderDetailId,
                OrderingId = values.OrderingId,
                ProductId = values.ProductId,
                ProductName = values.ProductName,
                ProductAmount = values.ProductAmount,
                ProductPrice = values.ProductPrice,
                ProductTotalPrice = values.ProductTotalPrice
            };
        }
    }
}

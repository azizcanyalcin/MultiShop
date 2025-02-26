﻿using Dapper;
using MultiShop.Discount.Context;
using MultiShop.Discount.Dtos;

namespace MultiShop.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly DapperContext context;

        public DiscountService(DapperContext context)
        {
            this.context = context;
        }

        public async Task CreateCouponAsync(CreateCouponDto createCouponDto)
        {
            string query = "insert into Coupons(Code,Rate,IsActive,ValidDate) values (@code, @rate, @isActive, @validDate)";

            var parameters = new DynamicParameters();

            parameters.Add("@code", createCouponDto.Code);
            parameters.Add("@rate", createCouponDto.Rate);
            parameters.Add("@isActive", createCouponDto.IsActive);
            parameters.Add("@validDate", createCouponDto.ValidDate);

            using(var connection = context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }

        }

        public async Task DeleteCouponAsync(int id)
        {
            string query = "Delete From Coupons Where Id = @couponId";

            var parameters = new DynamicParameters();

            parameters.Add("@couponId", id);

            using (var connection = context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultCouponDto>> GetAllCouponsAsync()
        {
            string query = "Select * From Coupons";

            using (var connection = context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultCouponDto>(query);
                return values.ToList();
            }
        }

        public async Task<GetCouponByIdDto> GetCouponByIdAsync(int id)
        {
            string query = "Select * From Coupons Where Id = @couponId";

            var parameters = new DynamicParameters();

            parameters.Add("@couponId", id);

            using (var connection = context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<GetCouponByIdDto>(query,parameters);
                return values;
            }
        }

        public async Task UpdateCouponAsync(UpdateCouponDto updateCouponDto)
        {
            string query = "Update Coupons Set Code=@code, Rate=@rate, IsActive=@isActive, ValidDate=@validDate Where Id=@couponId";
            
            var parameters = new DynamicParameters();

            parameters.Add("@code", updateCouponDto.Code);
            parameters.Add("@rate", updateCouponDto.Rate);
            parameters.Add("@isActive", updateCouponDto.IsActive);
            parameters.Add("@validDate", updateCouponDto.ValidDate);
            parameters.Add("@couponId", updateCouponDto.Id);

            using (var connection = context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}

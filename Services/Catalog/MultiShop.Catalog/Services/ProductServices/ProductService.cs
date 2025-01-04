using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.ProductDtos;
using MultiShop.Catalog.Dtos.ProductDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> productCollection;
        private readonly IMapper mapper;
        public ProductService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            productCollection = database.GetCollection<Product>(databaseSettings.ProductCollectionName);
            this.mapper = mapper;

        }
        public async Task CreateProductAsync(CreateProductDto createProductDto)
        {
            var value = mapper.Map<Product>(createProductDto);
            await productCollection.InsertOneAsync(value);
        }

        public async Task DeleteProductAsync(string id)
        {
            await productCollection.DeleteOneAsync(x => x.ProductId == id);
        }

        public async Task<List<ResultProductDto>> GetAllProductAsync()
        {
            var values = await productCollection.Find(x => true).ToListAsync();
            return mapper.Map<List<ResultProductDto>>(values);
        }

        public async Task<GetProductByIdDto> GetProductByIdAsync(string id)
        {
            var values = await productCollection.Find<Product>(x => x.ProductId == id).FirstOrDefaultAsync();
            return mapper.Map<GetProductByIdDto>(values);

        }

        public async Task UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            var values = mapper.Map<Product>(updateProductDto);

            await productCollection.FindOneAndReplaceAsync(x => x.ProductId == updateProductDto.ProductId, values);
        }
    }
}

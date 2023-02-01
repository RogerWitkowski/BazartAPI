using System.Collections;
using Bazart.Models.Dto.ProductDto;
using Microsoft.AspNetCore.JsonPatch;

namespace Bazart.Repository.Repository.IRepository
{
    public interface IProductRepository
    {
        public Task<ProductDto> GetProductByIsAsync(string userId, int productId);

        public Task<IEnumerable> GetProductsByCategoryNameAsync(string userId, string categoryName);

        public Task<string> CreateProductAsync(string userId, CreateProductDto createProductDto);

        public Task DeleteProductAsync(string userId, int productId);

        public Task UpdateProductAsync(string userId, int productId, UpdateProductDto updateProductDto);

        public Task UpdatePartialProduct(string userId, int productId, JsonPatchDocument<UpdateProductDto> updatePatchProduct);
    }
}
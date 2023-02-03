using System.Collections;
using AutoMapper;
using Bazart.DataAccess.DataAccess;
using Bazart.ErrorHandlingMiddleware.Exceptions;
using Bazart.Models.Dto.ProductDto;
using Bazart.Models.Model;
using Bazart.Repository.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Bazart.Repository.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly BazartDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(BazartDbContext dbContext, IMapper mapper, ILogger<ProductRepository> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ProductDto> GetProductByIsAsync(string userId, int productId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException("User not found");
            }
            var product = await _dbContext
                .Products
                .Include(c => c.Category)
                .Include(u => u.CreatedBy)
                .FirstOrDefaultAsync(p => p.Id == productId && p.CreatedById == userId);

            if (product is null)
            {
                throw new NotFoundException("Sorry! Product not found.");
            }

            var productDto = _mapper.Map<ProductDto>(product);
            return productDto;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsByCategoryNameAsync(string userId, string categoryName)
        {
            var products = await _dbContext
                .Products
                .Include(c => c.Category)
                .Include(u => u.CreatedBy)
                .Where(c => c.Category.Name.ToLower() == categoryName.ToLower() && c.CreatedById == userId)
                .ToListAsync();

            var productsDto = _mapper.Map<List<ProductDto>>(products);
            if (!productsDto.Any())
            {
                throw new NotFoundException("Products not found");
            }

            return productsDto;
        }

        public async Task<string> CreateProductAsync(string userId, CreateProductDto createProductDto)
        {
            var user = await _dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user is null)
            {
                throw new NotFoundException("User not found");
            }

            var product = _mapper.Map<Product>(createProductDto);
            product.CreatedById = userId;

            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return product.Id.ToString();
        }

        public async Task DeleteProductAsync(string userId, int productId)
        {
            _logger.LogError($"Product with id {productId} DELETE action invoked");

            var productToDelete = await _dbContext
                .Products
                .Include(c => c.Category)
                .FirstOrDefaultAsync(p => p.Id == productId && p.CreatedById == userId);

            if (productToDelete is null)
            {
                throw new Exception("Sorry! Product not found.");
            }

            var categoryToDelete = await Task.FromResult(_dbContext
                .Categories
                .Include(p => p.Products)
                .FirstOrDefault(c => c.Id == productToDelete.CategoryId));

            if (categoryToDelete is null)
            {
                throw new NotFoundException("Sorry! Product not found.");
            }

            await Task.FromResult(_dbContext.Products.Remove(productToDelete));
            await Task.FromResult(_dbContext.Categories.Remove(categoryToDelete));
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(string userId, int productId, UpdateProductDto updateProductDto)
        {
            var productToUpdate = await _dbContext
                .Products
                .Include(c => c.Category)
                .FirstOrDefaultAsync(p => p.Id == productId && p.CreatedById == userId);

            if (productToUpdate is null)
            {
                throw new NotFoundException("Sorry! Product not found.");
            }

            productToUpdate.Name = updateProductDto.Name;
            productToUpdate.Description = updateProductDto.Description;
            productToUpdate.Price = updateProductDto.Price;
            productToUpdate.IsForSell = updateProductDto.IsForSell;
            productToUpdate.ImageUrl = updateProductDto.ImageUrl;
            productToUpdate.Category.Name = updateProductDto.CategoryName;

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdatePartialProduct(string userid, int productId, JsonPatchDocument<UpdateProductDto> updatePatchProduct)
        {
            var productToUpdate = await _dbContext
                .Products
                .Include(c => c.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == productId && p.CategoryId == p.Category.Id && p.CreatedById == userid);

            if (productToUpdate is null)
            {
                throw new NotFoundException("Sorry! Product not found.");
            }

            var map = _mapper.Map<UpdateProductDto>(productToUpdate);

            updatePatchProduct.ApplyTo(map);

            var model = _mapper.Map<Product>(map);
            model.CreatedById = userid;

            await Task.FromResult(_dbContext.Update(model));

            await _dbContext.SaveChangesAsync();
        }
    }
}
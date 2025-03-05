using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApi.Models;
using ProductApi.Models.DTO;
using System;

namespace ProductApi.DataAccess
{
    public class ProductData : IProductDataAccess
    {
        private readonly ProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductData(ProductRepository productRepository, IMapper mapper)
        {
            this._productRepository = productRepository;
            this._mapper = mapper;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            var Products = await _productRepository.Products.ToListAsync();
            return Products;
        }

        public async Task<Product?> GetProductById(Guid guid)
        {
            var product = await _productRepository.Products.FirstOrDefaultAsync(x => x.ID == guid);
            return product;
        }

        public async Task<List<Product>> GetClassProduct(string productClass)
        {
            var products = await _productRepository.Products
            .Where(p => p.ProductClass.Contains(productClass))
            .ToListAsync();
            return products;
        }

        public async Task<Product> InsertProduct(Product product)
        {
            product.ID = Guid.NewGuid();
            product.DateTimeCreated = DateTime.Now;
            product.DateTimeUpdated = DateTime.Now;

            _productRepository.Products.Add(product); // Add to the DbSet
            await _productRepository.SaveChangesAsync(); // Save changes to the database
            return product;
        }

        public async Task<Product?> UpdateProduct(Guid id, ProductDTO productValue)
        {
            var product = await _productRepository.Products.FindAsync(id);

            if (product == null)
            {
                return null;
            }
            product.DateTimeUpdated = DateTime.Now;

            var productUpd = _mapper.Map(productValue, product);

            await _productRepository.SaveChangesAsync();
            
            return productUpd;
        }

        public async Task<bool> Exists(Guid id)
        {
            var product = await _productRepository.Products.FirstOrDefaultAsync(p => p.ID == id);

            if (product == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task Delete(Guid id)
        {
            var product = await _productRepository.Products.FindAsync(id);

            if(product != null)
            {
                _productRepository.Products.Remove(product);
                await _productRepository.SaveChangesAsync();
            }
        }
    }
}

using AutoMapper;
using ProductApi.Models;
using ProductApi.Models.DTO;

namespace ProductApi.DataAccess
{
    public interface IProductDataAccess
    {
        Task<List<Product>> GetAllProducts();
        Task<Product?> GetProductById(Guid guid);
        Task<Product> InsertProduct(Product product);
        Task<List<Product>> GetClassProduct(string productClass);
        Task<Product?> UpdateProduct(Guid id, ProductDTO productValue);
        Task<bool> Exists(Guid id);
        Task Delete(Guid id);
    }
}

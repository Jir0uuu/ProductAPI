using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProductApi.DataAccess;
using ProductApi.Models;
using ProductApi.Models.DTO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProductApi
{
    public class ProductEndpoints
    {
        public static void Register(WebApplication app)
        {
            var products = app.MapGroup("/products");

            products.MapGet("/", GetAllProducts);
            products.MapGet("/{id}", GetSpecificProduct);
            products.MapGet("/class/{Name}", GetClassProduct);
            products.MapPost("/", CreateProduct);
            products.MapPut("/update/{id:guid}", Update);
            products.MapDelete("/{id:guid}", Delete);
        }

        private static async Task<Results<NoContent, NotFound>> Delete(Guid id ,IDataAccess dataAccess)
        {
            var exist = await dataAccess.Exists(id);
            if(!exist)
            {
                return TypedResults.NotFound();
            }
            await dataAccess.Delete(id);
            return TypedResults.NoContent();
        }

        private static async Task<Results<Ok<Product>, NotFound>> Update(Guid id, ProductDTO productDTO, IDataAccess dataAccess, IMapper mapper)
        {
            var updatedProduct = await dataAccess.UpdateProduct(id, productDTO, mapper);
            if(updatedProduct == null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(updatedProduct);
        }

        static async Task<Created<Product>> CreateProduct(ProductDTO productDTO, IDataAccess dataAccess, IMapper mapper)
        {
            var product = mapper.Map<Product>(productDTO);
            var newProduct = await dataAccess.InsertProduct(product);
            return TypedResults.Created($"/products/{newProduct.ID}", newProduct);
        }

        private static async Task<Results<Ok<List<Product>>, NotFound>> GetClassProduct(string Name, IDataAccess dataAccess)
        {
            var list = await dataAccess.GetClassProduct(Name);
            if(list == null || list.Count == 0)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(list);
        }

        static async Task<Results<Ok<Product>, NotFound>> GetSpecificProduct(string id, IDataAccess dataAccess)
        {
            var product = await dataAccess.GetProductById(Guid.Parse(id));
            if (product == null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(product);
        }

        static async Task<IResult> GetAllProducts(IDataAccess dataAccess)
        {
            var list = await dataAccess.GetAllProducts();
            return Results.Ok(list);
        }
    }
}

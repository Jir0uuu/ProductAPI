using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using ProductApi.DataAccess;
using ProductApi.Models.DTO;
using ProductApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;

namespace ProductApi
{
    public class UserEndpoints
    {
        public static void Register(WebApplication app)
        {
            var products = app.MapGroup("/users");

            #region Required Endpoints
            products.MapPost("/register", RegisterUser);
            products.MapPost("/login", LoginUser);
            #endregion
        }

        static async Task<IResult> RegisterUser([FromBody]RegisterDTO userInput, IUserDataAccess dataAccess)
        {
            var response = await dataAccess.Register(userInput);
            return Results.Ok( new { message = response });
        }

        static async Task<Results<Ok<ResponseLoginDTO>, NotFound>> LoginUser([FromBody]LoginDTO user, IUserDataAccess dataAccess)
        {
            var response = await dataAccess.Login(user);
            if (response.message == null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(response);
        }

    }
}

using AutoMapper;
using ProductApi.Models;
using ProductApi.Models.DTO;

namespace ProductApi.DataAccess
{
    public interface IUserDataAccess
    {
        Task<string> Register(RegisterDTO user);
        Task<ResponseLoginDTO> Login(LoginDTO userInput);
    }
}

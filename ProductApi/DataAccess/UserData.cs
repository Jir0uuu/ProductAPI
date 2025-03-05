using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProductApi.Models;
using ProductApi.Models.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductApi.DataAccess
{
    public class UserData : IUserDataAccess
    {
        private readonly ProductRepository _productRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserData(ProductRepository productRepository, IConfiguration configuration, IMapper mapper)
        {
            this._productRepository = productRepository;
            this._configuration = configuration;
            this._mapper = mapper;
        }
        public async Task<ResponseLoginDTO> Login(LoginDTO userInput)
        {
            var user = await _productRepository.authenticateUser
                .FirstOrDefaultAsync(user => user.EmailID == userInput.Email);
            
            if(user != null)
            {
                bool passwordIsNotCorrect = BCrypt.Net.BCrypt.Verify(userInput.Password, user.Password);
                if (!passwordIsNotCorrect)
                {
                    return new ResponseLoginDTO
                    {
                        token = null,
                        message = "Password Incorrect"
                    };
                }

                string token = GenerateToken(user);
                return new ResponseLoginDTO
                {
                    token = token,
                    message = "Succesfully Logged In"
                };
            }
            else {
                return new ResponseLoginDTO
                {
                    token = null,
                    message = "User not found"
                };
            }
        }

        private string GenerateToken(Users user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim("Fullname", user.Username)
            };

            var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: userClaims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> Register(RegisterDTO userInput)
        {

            var user = await _productRepository.authenticateUser
                .FirstOrDefaultAsync(user => user.EmailID == userInput.EmailID);

            if (user != null)
            {
                return "User already exists";
            }
            else
            {
                var mappedUsers = _mapper.Map<Users>(userInput);
                mappedUsers.Password = BCrypt.Net.BCrypt.HashPassword(userInput.Password);
                _productRepository.Add(mappedUsers);
                await _productRepository.SaveChangesAsync();
                return "User created";
            }
        }

        
    }
}

using Entities;
using Medium_Clone_WebAPI.DataAccess.Interfaces;
using Medium_Clone_WebAPI.Models;
using Medium_Clone_WebAPI.Models.JwtSettingsModel;
using Medium_Clone_WebAPI.Services.interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Medium_Clone_WebAPI.Services.MCServices
{
    public class UserService : ICurrentUserService
    {
        private readonly IRepository<CurentUser> _curentUserRepo;
        private IOptions<JwtSettings> _jwtSettings;
        public UserService(IRepository<CurentUser> curentUserRepo, IOptions<JwtSettings> jwtSettings)
        {
            this._curentUserRepo = curentUserRepo;
            this._jwtSettings = jwtSettings;
        }
        public IEnumerable<CurentUserDto> GetAll()
        {
            try
            {
                var users = _curentUserRepo.GetAll();
                return users.Select(u => new CurentUserDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email,
                    CreatedAt = u.CreatedAt,
                    UpdateAt = u.UpdateAt,
                    Bio = u.Bio,
                    Image = u.Image,
                    Token = "Token"
                }).ToList();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
            
        }

        public CurentUserDto GetById(int id)
        {
            try
            {
                var user = _curentUserRepo.GetById(id);
                if(user == null)
                {
                    throw new NotImplementedException("User does not exist");
                }
                var token = GenereteToken(user);
                return new CurentUserDto()
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    CreatedAt = user.CreatedAt,
                    UpdateAt = user.UpdateAt,
                    Bio = user.Bio,
                    Image = user.Image,
                    Token = token
                };
            }
            catch(NotImplementedException ex)
            {
                throw new NotImplementedException(ex.Message);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public CurentUserDto RegisterNewUSer(RegisterDto register)
        {
            try
            {
                if(register == null)
                {
                    throw new NotImplementedException("Register can not be empty");
                }
                else
                {
                    string dateNow = DateTime.Now.ToString("yyyy-MM-dd");
                    
                    var addNewUser = new CurentUser
                    {
                        Username = register.Username,
                        Password = register.Password,
                        Email = register.Email,
                        CreatedAt = dateNow,
                        UpdateAt = dateNow,
                        Bio = null,
                        Image = null
                    };
                    _curentUserRepo.Add(addNewUser);
                    var user = _curentUserRepo.GetByUsername(addNewUser.Username);
                    if(user == null)
                    {
                        throw new Exception("Database error: the user is not registered");
                    }
                    var userById = GetById(user.Id);
                    return userById;
                }
            }
            catch (NotImplementedException ex)
            {
                throw new NotImplementedException(ex.Message);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        private string GenereteToken(CurentUser User)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = Encoding.ASCII.GetBytes(_jwtSettings.Value.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, User.Username),
                    new Claim(ClaimTypes.Email, User.Email),
                    new Claim(ClaimTypes.NameIdentifier, User.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(_jwtSettings.Value.ExpireDays),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret),SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

using Entities;
using Medium_Clone_WebAPI.DataAccess.Interfaces;
using Medium_Clone_WebAPI.Models;
using Medium_Clone_WebAPI.Services.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medium_Clone_WebAPI.Services.MCServices
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IRepository<CurentUser> _curentUserRepo;
        public CurrentUserService(IRepository<CurentUser> curentUserRepo)
        {
            this._curentUserRepo = curentUserRepo;
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
                return new CurentUserDto()
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    CreatedAt = user.CreatedAt,
                    UpdateAt = user.UpdateAt,
                    Bio = user.Bio,
                    Image = user.Image,
                    Token = "Token"
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

        public void RegisterNewUSer(RegisterDto register)
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
    }
}

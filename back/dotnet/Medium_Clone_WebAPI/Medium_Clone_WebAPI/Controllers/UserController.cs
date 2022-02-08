using Medium_Clone_WebAPI.Models;
using Medium_Clone_WebAPI.Services.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medium_Clone_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ICurrentUserService _currentUserService;
        public UserController(ICurrentUserService currentUserService)
        {
            this._currentUserService = currentUserService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CurentUserDto>> Get()
        {
            try
            {
                var users = _currentUserService.GetAll();
                return Ok(users);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<CurentUserDto> Get(int id)
        {
            try
            {
                var user = _currentUserService.GetById(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public ActionResult Post(RegisterDto register)
        {
            try
            {
                _currentUserService.RegisterNewUSer(register);
                var user = _currentUserService.GetAll().LastOrDefault();
                return Created($"~api/employees/{user.Id}", user);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

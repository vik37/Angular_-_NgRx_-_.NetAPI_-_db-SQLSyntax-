using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medium_Clone_WebAPI.Models
{
    public class CurentUserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string CreatedAt { get; set; }
        public string UpdateAt { get; set; }
        public string Bio { get; set; }
        public string Image { get; set; }
        public string Token { get; set; }
    }
}

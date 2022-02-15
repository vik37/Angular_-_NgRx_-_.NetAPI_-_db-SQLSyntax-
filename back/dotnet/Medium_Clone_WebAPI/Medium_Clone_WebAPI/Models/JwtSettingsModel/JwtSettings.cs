using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medium_Clone_WebAPI.Models.JwtSettingsModel
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public int ExpireDays { get; set; }
    }
}

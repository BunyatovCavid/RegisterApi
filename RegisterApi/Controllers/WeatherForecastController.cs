using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RegisterApi.Crud;
using RegisterApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        Register register;

        public WeatherForecastController()
        {
            register = new Register();
        }

        [HttpGet]
        public IEnumerable<RegisterModel> Get()
        {
            var return_value = register.Back_Get();
            return return_value;
        }

        [HttpPost]
        public IEnumerable<RegisterModel> Post([FromQuery]RegisterModel model)
        {
            var return_value = register.RegisterPost(model);
            return return_value;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileTmp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FileTmp.Controllers.Api
{
    [Route("Api/[Controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        public DataController()
        {

        }

        [AllowAnonymous]
        [HttpPut("AddData")]

        public async Task<DataModel> AddData([FromBody] DataModel data)
        {
            return new DataModel
            {
                FirstName = data.FirstName,
                LastName = data.LastName
            };

        }


    }
}


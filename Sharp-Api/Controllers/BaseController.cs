using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebFramework.Filters;

namespace Sharp_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiResultFilter]
    public class BaseController : ControllerBase
    {
    }
}

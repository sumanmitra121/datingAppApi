using Microsoft.AspNetCore.Mvc;
using TestApi.Helpers;

namespace TestApi.Controllers
{
    [ServiceFilter(typeof(loggedUserActivity))]
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        
    }
}
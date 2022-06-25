
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApi.DATA;
using TestApi.Entities;

namespace TestApi.Controllers
{
    // [ApiController]
    // [Route("api/[controller]")]
    public class usersController : BaseApiController
    {
        private readonly ApplicationDbContext __dbContext;
        public usersController(ApplicationDbContext _dbContext)
        {
            __dbContext = _dbContext;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
             return   await __dbContext.users.ToListAsync();
        }

         [Authorize]
        //api/users/3
         [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUserById(int id)
        {
            return  await __dbContext.users.FindAsync(id);
            
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApi.DATA;
using TestApi.Entities;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class usersController : ControllerBase
    {
        private readonly ApplicationDbContext __dbContext;
        public usersController(ApplicationDbContext _dbContext)
        {
            __dbContext = _dbContext;
            //HELLO SUMAN MITRA
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
             return   await __dbContext.users.ToListAsync();
        }

        //api/users/3
         [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUserById(int id)
        {
            return  await __dbContext.users.FindAsync(id);
            
        }
    }
}
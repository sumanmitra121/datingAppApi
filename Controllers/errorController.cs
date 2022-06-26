
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestApi.DATA;
using TestApi.Entities;

namespace TestApi.Controllers
{
    public class errorController : BaseApiController
    {
        public ApplicationDbContext _dbContext ;
        public errorController(ApplicationDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        
        [Authorize]
        [HttpGet("Auth")]
        public ActionResult<string> GetSecret(){
               return "Secret Text";
        } 
         [HttpGet("not-found")]
        public ActionResult<AppUser> GetNotFound(){
              var thing = _dbContext.users.Find(-1);
              return thing == null ? NotFound() : Ok(thing);
        }  
        [HttpGet("server-error")]
        public ActionResult<string> GetServerError(){
         
                var thing = _dbContext.users.Find(-1);
                var thingtoreturn =  thing.ToString();
                return thingtoreturn;
        }  
        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest(){
               return BadRequest("Not A Good Request");
        }  
    }
}
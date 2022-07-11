using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using TestApi.Interfaces;

namespace TestApi.Helpers
{
    public class loggedUserActivity : IAsyncActionFilter
    {
        private readonly IUserRepository _userRepo;

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        { 
             var resultContext = await next();
             if(!resultContext.HttpContext.User.Identity.IsAuthenticated) return;
               var _email =resultContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
               var repo = resultContext.HttpContext.RequestServices.GetService<IUserRepository>();
               var user= await repo.GetUserByEmailAsync(_email);
               user.last_active= DateTime.Now;
               await repo.SaveAllAsync();
        }
    }
}
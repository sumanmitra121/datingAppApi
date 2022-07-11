using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TestApi.Extensions
{
    public static class claimsPrincipleExtensions
    {
        public static string Getusername(this ClaimsPrincipal user){
             return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
          public static int Getuserid(this ClaimsPrincipal user){
             return int.Parse( user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }
    }
}
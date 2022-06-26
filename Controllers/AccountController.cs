using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApi.DATA;
using TestApi.DTOs;
using TestApi.Entities;
using System.Security.Cryptography;
using System.Text;
using TestApi.Interfaces;

namespace TestApi.Controllers
{
    public class AccountController :BaseApiController
    {

        private readonly ApplicationDbContext __dbContext;
        private readonly ITokenService __tknService;

        public AccountController(ApplicationDbContext _dbContext,ITokenService tknService)
        {
             __tknService = tknService;
            __dbContext = _dbContext;
        }
        [HttpPost("register")]
        public async Task<ActionResult<userDto>> userRegister(registreduser _userDtls){
                if(await checkExist(_userDtls.Email)) return BadRequest("Error! Email is already exist, please try with another Email");
                using var hmac = new HMACSHA512();
                var user = new AppUser{
                    
                    UserName  = _userDtls.UserName.ToLower(),
                    Email = _userDtls.Email,
                    Phone = _userDtls.Phone,
                    passwordhash = hmac.ComputeHash(Encoding.UTF8.GetBytes(_userDtls.password)),
                    passwordSalt = hmac.Key
                };
                __dbContext.users.Add(user);
                await __dbContext.SaveChangesAsync();
                return new userDto{
                     UserName  = user.UserName,
                    Email = user.Email,
                    Phone = user.Phone,
                    Token = __tknService.CreateToken(user)
                };
        }

        [HttpPost("login")]
        public async Task<ActionResult<userDto>> userlogin(loggedUser _userDtls){
                  var _user = await __dbContext.users.SingleOrDefaultAsync
                  (x=> x.Email == _userDtls.Email.ToLower());

                  if(_user == null){
                      return Unauthorized("Invalid Email");
                  }
                 using var hmac = new HMACSHA512(_user.passwordSalt);
                 var computHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(_userDtls.password));
                for(int i=0;i<computHash.Length;i++){
                     if(computHash[i]!= _user.passwordhash[i]) return Unauthorized("Invalid Password");
                }
              return new userDto{
                    Id = _user.Id,
                    UserName  = _user.UserName,
                    Email = _user.Email,
                    Phone = _user.Phone,
                    Token = __tknService.CreateToken(_user)
                };
        }
        private async Task<bool> checkExist(string email){
            return await __dbContext.users.AnyAsync(x => x.Email.ToLower() == email.ToLower());
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApi.DATA;
using TestApi.DTOs;
using TestApi.Entities;
using TestApi.Interfaces;

namespace TestApi.Controllers
{
    [Authorize]
    public class usersController : BaseApiController
    {
        // private readonly ApplicationDbContext __dbContext;
        // public usersController(ApplicationDbContext _dbContext)
        // {
        //     __dbContext = _dbContext;
        // }
        private readonly IMapper _mapper;

        private readonly IUserRepository _userRepo;
        public usersController(IUserRepository userRepo,IMapper mapper)
        {
            _mapper = mapper;
            _userRepo = userRepo;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<MemeberDto>>> GetUsers()
        {
            /*1st way*/
            //  var users = await _userRepo.GetUserAsync();
            //  var _users = _mapper.Map<IEnumerable<MemeberDto>>(users);
            //  return Ok(_users);

            /* 2nd way*/
            return Ok( await _userRepo.GetMembersAsync());
        }

         
        //api/users/3
        //  [HttpGet("{id}")]
        // public async Task<ActionResult<AppUser>> GetUserById(int id)
        // {
        //     return  await _userRepo.GetUserByIdAsync(id); 
        // }
        //api/users/Suman Mitra
        [HttpGet("{username}")]
        public async Task<ActionResult<MemeberDto>> getUserByName(string username)
        {
            /* 1st way*/
            //  var users =  await _userRepo.GetUserByNameAsync(username);
            //  return _mapper.Map<MemeberDto>(users); 

            /* 2nd way*/
            var users = await _userRepo.GetMemberAsync(username);
            return users;
        }
    }
}
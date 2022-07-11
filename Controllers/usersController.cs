
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApi.DATA;
using TestApi.DTOs;
using TestApi.Entities;
using TestApi.Extensions;
using TestApi.Helpers;
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
        private readonly IPhotoService photoService;
        private readonly IUserRepository _userRepo;
        public usersController(IUserRepository userRepo,IMapper mapper,IPhotoService photoService)
        {
            _mapper = mapper;
            this.photoService = photoService;
            _userRepo = userRepo;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<MemeberDto>>> GetUsers([FromQuery]userParams u_params)
        {
            /*1st way*/
            //  var users = await _userRepo.GetUserAsync();
            //  var _users = _mapper.Map<IEnumerable<MemeberDto>>(users);
            //  return Ok(_users);

            /* 2nd way*/
                var _email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var _user = await _userRepo.GetUserByEmailAsync(_email);
              u_params.CurrentUsername =  _user.UserName;
              if(string.IsNullOrEmpty(u_params.gender))
               u_params.gender = _user.gender == "male" ? "female" : "male";
             var user = await _userRepo.GetMembersAsync(u_params);
              Response.AddPaginationheader(user.CurrentPage,
              user.PageSize,
              user.TotalCount,
              user.TotalPages);
            return Ok(user);
        }

         
        //api/users/3
        //  [HttpGet("{id}")]
        // public async Task<ActionResult<AppUser>> GetUserById(int id)
        // {
        //     return  await _userRepo.GetUserByIdAsync(id); 
        // }
        //api/users/Suman Mitra
        [HttpGet("{username}", Name ="getUserByName")]
        public async Task<ActionResult<MemeberDto>> getUserByName(string username)
        {
            /* 1st way*/
            //  var users =  await _userRepo.GetUserByNameAsync(username);
            //  return _mapper.Map<MemeberDto>(users); 

            /* 2nd way*/
            var users = await _userRepo.GetMemberAsync(username);
            return users;
        }
         [HttpPut]
         public async Task<ActionResult> updateUser(memberUpdateDto _member){

               var _email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
               var user = await _userRepo.GetUserByEmailAsync(_email);
               _mapper.Map(_member,user);
               _userRepo.Update(user);
               if(await _userRepo.SaveAllAsync()){return NoContent();}
               return BadRequest("Failed to Update");
        }
       
        [HttpPost("addPhoto")]
         public async Task<ActionResult<photoDTO>> AddPhoto(IFormFile file){
                var _email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
               var user = await _userRepo.GetUserByEmailAsync(_email);
               var result =  await photoService.AddPhotoAsync(file);          
               if(result.Error != null) return BadRequest(result.Error.Message);

               var  photo = new photo
               {
                   url = result.SecureUrl.AbsoluteUri,
                   publicId = result.PublicId

               };
               if(user.photos.Count == 0){
                   photo.isMain = true;
               }
               user.photos.Add(photo);
               if(await _userRepo.SaveAllAsync()){
                //    return _mapper.Map<photoDTO>(photo);
                  return CreatedAtRoute("getUserByName",new {username = user.UserName}, _mapper.Map<photoDTO>(photo));

               }
               return BadRequest("problem adding photo");
        }
        [HttpPut("SetMainPhoto/{photoId}")]
         public async Task<ActionResult> setMainPhoto(int photoId){

               var _email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
               var user = await _userRepo.GetUserByEmailAsync(_email);
               var photo = user.photos.FirstOrDefault(x => x.Id == photoId);
               if(photo.isMain){ return BadRequest("This is already your main photo");}
               var currentMain = user.photos.FirstOrDefault(x => x.isMain);
               if(currentMain!=null) currentMain.isMain = false;
               photo.isMain = true;
               if(await _userRepo.SaveAllAsync()){
                   return NoContent();
               }
               return BadRequest("failed to set main photo");
         }
          [HttpDelete("delete_photo/{photoId}")]
            public async Task<ActionResult> DeletePhoto(int photoId)
            {
                 var _email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                  var user = await _userRepo.GetUserByEmailAsync(_email);
                    var photo = user.photos.FirstOrDefault(x => x.Id == photoId);
                   if(photo==null){ return NotFound();}
                   if(photo.isMain){ return BadRequest("You can not delete main photo");}
                   if(photo.publicId != null)
                   {
                       var res = await photoService.DeletePhotoAsync(photo.publicId);
                       if(res.Error != null){ return BadRequest(res.Error.Message);}

                   }
                   user.photos.Remove(photo);
                  if(await _userRepo.SaveAllAsync()){
                        return Ok();
                  }
                   return BadRequest("Failed To Delete photo");
            }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TestApi.DTOs;
using TestApi.Entities;
using TestApi.Helpers;
using TestApi.Interfaces;

namespace TestApi.DATA
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext __context;
        private readonly IMapper _mapper;
        public UserRepository(ApplicationDbContext _context,IMapper mapper)
        {
            _mapper = mapper;
            __context = _context;

        }

        public async  Task<MemeberDto> GetMemberAsync(string username)
        {
            return await __context.users.Where(x=>x.UserName == username).
            ProjectTo<MemeberDto>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
        }


        public async Task<Pagedlist<MemeberDto>> GetMembersAsync(userParams userParams)
        {
        //   var query =   __context.users
        //    .ProjectTo<MemeberDto>(_mapper.ConfigurationProvider).
        //    AsNoTracking().AsQueryable();

              var query=  __context.users.AsQueryable();
              query = query.Where(x => x.UserName != userParams.CurrentUsername);
              query =query.Where(x=> x.gender == userParams.gender);
              var minDob =  DateTime.Today.AddYears(-userParams.maxAge - 1);
              var maxDob =  DateTime.Today.AddYears(-userParams.minAge);
              query = userParams.orderBy switch{

                   "created_at" => query.OrderByDescending( u => u.created_at),
                   _ => query.OrderByDescending( u => u.last_active)
              };
              query = query.Where(u => u.dateOfbirth >= minDob && u.dateOfbirth <= maxDob);
              
    

                return await Pagedlist<MemeberDto>.createAsync(
           query.ProjectTo<MemeberDto>
           (_mapper.ConfigurationProvider).
           AsNoTracking(),
           userParams.pageNumber,
           userParams.pageSize);
        }

        public async Task<IEnumerable<AppUser>> GetUserAsync()
        {

            return await __context.users
            .Include(x => x.photos)
            .ToListAsync();
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await __context.users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByNameAsync(string username)
        {
             return await __context.users
             .Include(x => x.photos)
             .SingleOrDefaultAsync(x => x.UserName == username);
        }

         public async Task<AppUser> GetUserByEmailAsync(string email)
        {
             return await __context.users
             .Include(x => x.photos)
             .SingleOrDefaultAsync(x => x.Email == email);
        }

        public async Task<bool> SaveAllAsync()
        {
             return await __context.SaveChangesAsync() > 0 ;
            
        }

        public void Update(AppUser user)
        {
             __context.Entry(user).State = EntityState.Modified;
        }
    }
}
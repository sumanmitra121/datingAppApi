using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TestApi.DTOs;
using TestApi.Entities;
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

        public async Task<IEnumerable<MemeberDto>> GetMembersAsync()
        {
           return await  __context.users.ProjectTo<MemeberDto>(_mapper.ConfigurationProvider).ToListAsync();
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
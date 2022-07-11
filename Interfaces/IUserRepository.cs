
using TestApi.DTOs;
using TestApi.Entities;
using TestApi.Helpers;

namespace TestApi.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<AppUser>> GetUserAsync();

        Task<AppUser> GetUserByIdAsync(int id);

        Task<AppUser> GetUserByNameAsync(string username);
         Task<AppUser> GetUserByEmailAsync(string email);
         Task<MemeberDto> GetMemberAsync(string username);
          Task<Pagedlist<MemeberDto>> GetMembersAsync(userParams userParams);
    

    }
}
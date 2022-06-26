
using TestApi.DTOs;
using TestApi.Entities;

namespace TestApi.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<AppUser>> GetUserAsync();

        Task<AppUser> GetUserByIdAsync(int id);

        Task<AppUser> GetUserByNameAsync(string username);

         Task<MemeberDto> GetMemberAsync(string username);
          Task<IEnumerable<MemeberDto>> GetMembersAsync();

    }
}
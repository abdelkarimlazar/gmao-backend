using GmaoIntentApi.DTOs;

namespace GmaoIntentApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<PagedResultDto<UserDto>> GetUsersAsync(UserQueryDto query);
        Task<UserDto?> GetUserByIdAsync(int id);
        Task<UserDto> CreateUserAsync(CreateUserDto dto);
        Task<bool> UpdateUserAsync(int id, UpdateUserDto dto);
        Task<bool> DeleteUserAsync(int id);
    }
}

using GmaoIntentApi.DTOs;
using GmaoIntentApi.Models;
using GmaoIntentApi.Repositories.Interfaces;
using GmaoIntentApi.Services.Interfaces;

namespace GmaoIntentApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<PagedResultDto<UserDto>> GetUsersAsync(UserQueryDto query)
        {
            var (items, totalCount) = await _userRepository.GetPagedAsync(query.Search, query.PageNumber, query.PageSize);
            return new PagedResultDto<UserDto>
            {
                Items = items.Select(Map).ToArray(),
                PageNumber = query.PageNumber,
                PageSize = query.PageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)query.PageSize)
            };
        }

        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user is null ? null : Map(user);
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto dto)
        {
            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = dto.PasswordHash,
                Role = dto.Role
            };

            await _userRepository.AddAsync(user);
            return Map(user);
        }

        public async Task<bool> UpdateUserAsync(int id, UpdateUserDto dto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user is null)
            {
                return false;
            }

            user.FullName = dto.FullName;
            user.Email = dto.Email;
            user.PasswordHash = dto.PasswordHash;
            user.Role = dto.Role;

            await _userRepository.UpdateAsync(user);
            return true;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user is null)
            {
                return false;
            }

            await _userRepository.DeleteAsync(user);
            return true;
        }

        private static UserDto Map(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role
            };
        }
    }
}

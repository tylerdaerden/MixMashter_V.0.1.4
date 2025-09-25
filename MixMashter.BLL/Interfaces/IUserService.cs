using MixMashter.Models.Entities;
using MixMashter.Models.Enums;


namespace MixMashter.BLL.Interfaces
{
    public interface IUserService
    {
        Task<User?> RegisterAsync(string firstname, string lastname, string username, string email, string password, Role role);
        Task<User?> LoginAsync(string email, string password);
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task<IEnumerable<User>> GetAllAsync();        
        Task<bool> UpdateAsync(User user);
        Task<bool> DeleteAsync(int id);
        Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword);
        Task<bool> DeleteProfileAsync(int userId);
        Task<bool> SetRoleAsync(int userId, Role newRole);
    }
}


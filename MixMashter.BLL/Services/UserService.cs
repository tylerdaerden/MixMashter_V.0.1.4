using MixMashter.BLL.Interfaces;
using MixMashter.DAL.Repositories.Interfaces;
using MixMashter.Models.Entities;
using MixMashter.Models.Enums;
using System.Text.RegularExpressions;

namespace MixMashter.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> RegisterAsync(string firstname, string lastname, string username, string email, string password, Role role = Role.User)
        {
            var existingUser = await _userRepository.GetByEmailAsync(email);
            if (existingUser != null)
                return null;

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var newUser = new User
            {
                Firstname = firstname,
                Lastname = lastname,
                Username = username,
                Email = email,
                PasswordHash = hashedPassword,
                Role = role,
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddAsync(newUser);

            return newUser;
        }

        public async Task<User?> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null) return null;

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if (!isPasswordValid) return null;

            return user;
        }

        public async Task<User?> GetByIdAsync(int id) => await _userRepository.GetByIdAsync(id);

        public async Task<User?> GetByEmailAsync(string email) => await _userRepository.GetByEmailAsync(email);

        public async Task<IEnumerable<User>> GetAllAsync() => await _userRepository.GetAllAsync();

        // Nouveaux CRUD
        public async Task<bool> UpdateAsync(User user)
        {
            var existing = await _userRepository.GetByIdAsync(user.UserId);
            if (existing == null) return false;

            // Mise à jour manuelle pour éviter d'écraser des champs critiques
            existing.Firstname = user.Firstname;
            existing.Lastname = user.Lastname;
            existing.Username = user.Username;
            existing.Email = user.Email;
            existing.Role = user.Role;

            if (!string.IsNullOrEmpty(user.PasswordHash))
                existing.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

            await _userRepository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _userRepository.GetByIdAsync(id);
            if (existing == null) return false;

            await _userRepository.DeleteAsync(existing);
            return true;
        }

        public async Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return false;

            // Vérifier l’ancien mot de passe
            bool valid = BCrypt.Net.BCrypt.Verify(currentPassword, user.PasswordHash);
            if (!valid) return false;

            // Hash du nouveau mot de passe
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await _userRepository.UpdateAsync(user);
            return true;
        }

        public async Task<bool> DeleteProfileAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return false;

            await _userRepository.DeleteAsync(user);
            return true;
        }

        public async Task<bool> SetRoleAsync(int userId, Role newRole)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return false;

            user.Role = newRole;
            await _userRepository.UpdateAsync(user);
            return true;
        }

        //Méthodes de validation : 
        /// <summary>
        /// récupération et adaptation d'une méthode IsValidName déjà utilisée et validée précedemment en SGBD et en stage
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public bool IsValidName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) &&
                   name.Length >= 2 &&              
                   name.Length <= 100 &&
                   Regex.IsMatch(name, @"^[A-Za-zÀ-ÖØ-öø-ÿ' -]+$");
        }

        /// <summary>
        /// ici récupération d'une méthode IsValidEmail sur base d'une méthode Regex déjà utilisée et validée précedemment en SGBD et en stage      
        /// Returns true if the email is valid; otherwise, false.
        /// </summary>
        /// <param name="mailtocheck"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;

            return Regex.IsMatch(
                email,
                @"^[^@\s]+@[^@\s]+\.[A-Za-z]{2,}$",
                RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// récupération et adaptation d'une méthode IsValidName déjà utilisée et validée précedemment en SGBD et en stage
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) return false;

            // Ex: min 8 caractères, 1 maj, 1 min, 1 chiffre, 1 spécial
            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$");
            return regex.IsMatch(password);
        }


    }
}

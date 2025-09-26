using FluentAssertions;
using MixMashter.BLL.Services;
using Xunit;

namespace MixMashter.Tests
{
    public class UserServiceTests
    {
        
        [Theory]
        [InlineData("a", false)]              // trop court
        [InlineData("Ab", true)]              // min 2 ok
        [InlineData("Jean", true)]            // normal
        [InlineData("O'Neil", true)]          // apostrophe
        [InlineData("Van Damme", true)]       // espace
        [InlineData("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA", false)] // trop long
        [InlineData("Jean123", false)]        // chiffre interdit
        [InlineData("Jean!", false)]          // caractère spécial interdit
        public void IsValidName_ShouldValidateNames(string name, bool expected)
        {
            // Arrange
            var service = new UserService(null!);

            // Act
            var result = service.IsValidName(name);

            // Assert
            result.Should().Be(expected);
        }


        [Theory]
        [InlineData("test@example.com", true)]          // format standard
        [InlineData("user.name@domain.co", true)]       // sous-domaine simple
        [InlineData("user+alias@domain.com", true)]     // alias Gmail
        [InlineData("user@sub.domain.com", true)]       // sous-domaine complexe
        [InlineData("invalid@", false)]                 // manque domaine
        [InlineData("@example.com", false)]             // manque nom utilisateur
        [InlineData("userexample.com", false)]          // manque @
        [InlineData("user@.com", false)]                // domaine invalide
        [InlineData("user@domain", false)]              // pas de TLD
        [InlineData("", false)]                         // vide
        [InlineData("   ", false)]                      // espaces
        public void IsValidEmail_ShouldValidateEmails(string email, bool expected)
        {
            // Arrange
            var service = new UserService(null!);

            // Act
            var result = service.IsValidEmail(email);

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData("Password1!", true)]          // valide : maj, min, chiffre, spécial
        [InlineData("Passw0rd$", true)]           // valide
        [InlineData("password1!", false)]         // manque majuscule
        [InlineData("PASSWORD1!", false)]         // manque minuscule
        [InlineData("Password!!", false)]         // manque chiffre
        [InlineData("Password1", false)]          // manque caractère spécial
        [InlineData("Pwd1!", false)]              // trop court (< 8)
        [InlineData("        ", false)]           // seulement espaces
        [InlineData("", false)]                   // vide
        public void IsValidPassword_ShouldValidatePasswords(string password, bool expected)
        {
            // Arrange
            var service = new UserService(null!);

            // Act
            var result = service.IsValidPassword(password);

            // Assert
            result.Should().Be(expected);
        }





    }
}

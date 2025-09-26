using FluentAssertions;
using MixMashter.BLL.Services;
using Xunit;

namespace MixMashter.Tests
{
    public class MashupServiceTests
    {
        [Theory]
        [InlineData("mp3", true)]
        [InlineData("wav", true)]
        [InlineData("flac", true)]
        [InlineData("MP3", true)]         // insensible à la casse
        [InlineData("ogg", false)]        // format non autorisé
        [InlineData("", false)]           // vide
        [InlineData("   ", false)]        // espaces
        [InlineData(null, false)]         // null
        public void IsValidFormat_ShouldValidateFormats(string? format, bool expected)
        {
            // Arrange
            var service = new MashupService(null!);

            // Act
            var result = service.IsValidFormat(format ?? string.Empty);

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData("https://test.com/mashup.mp3", true)]
        [InlineData("http://test.org/file.wav", true)]
        [InlineData("ftp://test.com/file.flac", true)]
        [InlineData("invalid-url", false)]           // pas une URL valide
        [InlineData("www.example.com/file.mp3", false)] // manque protocole
        [InlineData("", false)]                      // vide
        [InlineData("   ", false)]                   // espaces
        public void IsValidUrlLink_ShouldValidateUrls(string url, bool expected)
        {
            // Arrange
            var service = new MashupService(null!);

            // Act
            var result = service.IsValidUrlLink(url);

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData("https://test.com/cover.jpg", true)]
        [InlineData("http://test.com/cover.png", true)]
        [InlineData("https://test.com/cover.gif", true)]
        [InlineData("https://test.com/cover.bmp", true)]
        [InlineData("https://test.com/cover.txt", false)] // mauvaise extension
        [InlineData("invalid-url", false)]                   // pas une URL
        [InlineData("", false)]                              // vide
        [InlineData("   ", false)]                           // espaces
        public void IsValidCoverImage_ShouldValidateCoverImages(string url, bool expected)
        {
            // Arrange
            var service = new MashupService(null!);

            // Act
            var result = service.IsValidCoverImage(url);

            // Assert
            result.Should().Be(expected);
        }
    }
}

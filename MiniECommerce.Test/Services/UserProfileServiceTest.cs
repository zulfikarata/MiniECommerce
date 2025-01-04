using Moq;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

public class UserProfileServiceTest
{
    private readonly UserProfileService _userProfileService;

    public UserProfileServiceTest()
    {
        _userProfileService = new UserProfileService();
    }

    [Fact]
    public async Task UploadProfilePhotoAsync_ShouldSaveFileAndReturnFileName_WhenValidInputProvided()
    {
        var userId = Guid.NewGuid();
        var fileName = "test.jpg";
        var fileContent = "Fake file content for testing.";
        var fileStream = new MemoryStream();
        var writer = new StreamWriter(fileStream);
        await writer.WriteAsync(fileContent);
        await writer.FlushAsync();
        fileStream.Position = 0;

        var result = await _userProfileService.UploadProfilePhotoAsync(userId, fileStream, fileName);

        Assert.NotNull(result);
        Assert.Contains(userId.ToString(), result);
        Assert.Contains(fileName, result);

        string uploadedFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "uploads", "profile_photos", result);
        if (File.Exists(uploadedFilePath))
        {
            File.Delete(uploadedFilePath); 
        }
    }

    [Fact]
    public async Task UploadProfilePhotoAsync_ShouldThrowArgumentException_WhenFileStreamIsNull()
    {
        var userId = Guid.NewGuid();
        string fileName = "test.jpg";

        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _userProfileService.UploadProfilePhotoAsync(userId, null, fileName));
        Assert.Equal("Invalid file stream. (Parameter 'fileStream')", exception.Message);
    }

    [Fact]
    public async Task UploadProfilePhotoAsync_ShouldThrowArgumentException_WhenFileNameIsEmpty()
    {
        var userId = Guid.NewGuid();
        var fileStream = new MemoryStream();

        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _userProfileService.UploadProfilePhotoAsync(userId, fileStream, string.Empty));
        Assert.Equal("File name cannot be null or empty. (Parameter 'fileName')", exception.Message);
    }

    [Fact]
    public async Task UploadProfilePhotoAsync_ShouldThrowArgumentException_WhenFileStreamIsEmpty()
    {
        var userId = Guid.NewGuid();
        string fileName = "test.jpg";
        var fileStream = new MemoryStream(); 

        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _userProfileService.UploadProfilePhotoAsync(userId, fileStream, fileName));
        Assert.Equal("Invalid file stream. (Parameter 'fileStream')", exception.Message);
    }

}

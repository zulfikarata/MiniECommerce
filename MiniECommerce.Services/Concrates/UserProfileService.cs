using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MiniECommerce.Services.Abstracts;

public class UserProfileService : IUserProfileService
{
    private readonly string _uploadFolder;

    public UserProfileService()
    {
        _uploadFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "uploads", "profile_photos");
        if (!Directory.Exists(_uploadFolder))
        {
            Directory.CreateDirectory(_uploadFolder);
        }
    }

    public async Task<string> UploadProfilePhotoAsync(Guid userId, Stream fileStream, string fileName)
    {
        if (fileStream == null || fileStream.Length == 0)
            throw new ArgumentException("Invalid file stream.", nameof(fileStream));

        if (string.IsNullOrEmpty(fileName))
            throw new ArgumentException("File name cannot be null or empty.", nameof(fileName));

        string safeFileName = $"{userId}_{Path.GetFileName(fileName)}";
        string filePath = Path.Combine(_uploadFolder, safeFileName);

        using (var output = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        {
            await fileStream.CopyToAsync(output);
        }

        return safeFileName; 
    }
}
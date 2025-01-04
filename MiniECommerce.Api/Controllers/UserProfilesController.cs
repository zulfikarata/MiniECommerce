using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniECommerce.Services.Abstracts;

namespace MiniECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfilesController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;
        public UserProfilesController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        [HttpPost("{userId}/upload-photo")]
        public async Task<IActionResult> UploadPhoto(Guid userId, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Invalid file.");

            using (var stream = file.OpenReadStream())
            {
                var fileName = await _userProfileService.UploadProfilePhotoAsync(userId, stream, file.FileName);
                return Ok(new { Message = "File uploaded successfully.", FileName = fileName });
            }
        }
    }
}

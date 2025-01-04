using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Services.Abstracts
{
    public interface IUserProfileService
    {
        Task<string> UploadProfilePhotoAsync(Guid userId, Stream fileStream, string fileName);
    }
}

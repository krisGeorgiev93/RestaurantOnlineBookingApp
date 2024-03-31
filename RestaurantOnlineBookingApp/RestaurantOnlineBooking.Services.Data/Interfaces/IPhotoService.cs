using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using RestaurantOnlineBookingApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOnlineBooking.Services.Data.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);

        Task<Photo> GetPhotoByIdAsync(string id);

        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}

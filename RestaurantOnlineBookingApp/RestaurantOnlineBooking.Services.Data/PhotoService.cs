namespace RestaurantOnlineBooking.Services.Data
{
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    using RestaurantOnlineBooking.Services.Data.Interfaces;
    using RestaurantOnlineBookingApp.Data;
    using RestaurantOnlineBookingApp.Data.Configurations;
    using RestaurantOnlineBookingApp.Data.Models;

    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary cloudinary;
        private readonly RestaurantBookingDbContext dbContext;
        public PhotoService(IOptions<CloudinarySettings> config, RestaurantBookingDbContext dbContext)
        {
            var acc = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
                );
            this.cloudinary = new Cloudinary(acc);
            this.dbContext = dbContext;
        }
        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            string imageUrl = null;
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                };
                uploadResult = await cloudinary.UploadAsync(uploadParams);
                imageUrl = uploadResult.SecureUrl.ToString();
            }
            return uploadResult;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {

            var deleteParams = new DeletionParams(publicId);
            return await this.cloudinary.DestroyAsync(deleteParams);

        }

        public async Task<Photo> GetPhotoByIdAsync(string id)
        {
            return await dbContext.Photos.FirstOrDefaultAsync(p => p.Id.ToString() == id);
        }
    }
}

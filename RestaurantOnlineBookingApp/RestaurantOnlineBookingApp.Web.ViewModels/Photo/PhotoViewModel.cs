using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOnlineBookingApp.Web.ViewModels.Photo
{
    public class PhotoViewModel
    {
        [Required]
        public Guid RestaurantId { get; set; }

        [Required(ErrorMessage = "Please select an image.")]
        public IFormFile Image { get; set; } = null!;
    }
}

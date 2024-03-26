
namespace RestaurantOnlineBookingApp.Web.ViewModels.Restaurant
{
    using Microsoft.AspNetCore.Http;
    using RestaurantOnlineBookingApp.Web.ViewModels.Category;
    using RestaurantOnlineBookingApp.Web.ViewModels.City;
    using System.ComponentModel.DataAnnotations;
    

    using static RestaurantOnlineBookingApp.Common.ValidationConstants.Restaurant;
    public class RestaurantFormModel
    {
        public RestaurantFormModel()
        {
            this.Categories = new HashSet<SelectCategoryFormModel>();
            this.Cities = new HashSet<SelectCityFormModel>();   
        }

        [Required]
        [MaxLength(NameMaxLength)]
        [MinLength(NameMinLength)]
        public string Name { get; set; } = null!;
        [Required]
        [MaxLength(AddressMaxLength)]
        [MinLength(AddressMinLength)]
        public string Address { get; set; } = null!;
        [Required]
        [MaxLength(DescriptionMaxLength)]
        [MinLength(DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]       
        public IFormFile Image { get; set; } = null!;       

        [Required]
        [Range(1, 300)]
        public int Capacity { get; set; }

        [Required]
        [Display(Name = "Starting Time")]
        public TimeSpan StartingTime {  get; set; }

        [Required]
        [Display(Name = "Ending Time")]

        public TimeSpan EndingTime { get; set; }  

        public Guid OwnerId { get; set; }

        [Required]
        [Display(Name = "City")]
        public int CityId { get; set; }

        public IEnumerable<SelectCityFormModel> Cities { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public IEnumerable<SelectCategoryFormModel> Categories { get; set; }

    }
}

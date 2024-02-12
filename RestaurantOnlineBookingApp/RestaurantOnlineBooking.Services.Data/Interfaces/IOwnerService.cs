using RestaurantOnlineBookingApp.Web.ViewModels.Owner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOnlineBooking.Services.Data.Interfaces
{
    public interface IOwnerService
    {
        Task<bool> OwnerExistByIdAsync(string id);

        Task<bool> OwnerExistsByPhoneNumberAsync(string phoneNumber);

        Task<string?> OwnerIdByUserIdAsync(string userId);

        Task Create(string userId, JoinOwnerFormModel model);
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantOnlineBookingApp.Web.Areas.AdminArea.Controllers
{
    using static Common.ApplicationConstants;

    [Area(AdminAreaName)]
    [Authorize(Roles =AdminRoleName)]
    public class BaseAdminController : Controller
    {
       
    }
}

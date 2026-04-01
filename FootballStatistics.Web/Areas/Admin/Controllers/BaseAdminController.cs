using FootballStatistics.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballStatistics.Web.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = ApplicationRoles.Administrator)]
    public class BaseAdminController : Controller
    {
   
    }
}

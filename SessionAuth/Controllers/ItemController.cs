using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SessionAuth.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

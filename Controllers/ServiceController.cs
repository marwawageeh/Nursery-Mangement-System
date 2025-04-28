using Microsoft.AspNetCore.Mvc;
using mvc_project.Filtrers;
using System.Security.Claims;

namespace mvc_project.Controllers
{
    public class ServiceController : Controller
    {
        //[Authorize]
        [HandelError]
        public IActionResult TestAuth()
        {
            if (User.Identity.IsAuthenticated == true)
            {

                Claim IDClaim = User.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

                Claim AddressClaim = User.Claims.FirstOrDefault(c => c.Type == "UserAddress");

                string id = IDClaim.Value;

                string name = User.Identity.Name;
                return Content($"welcome {name} \n ID= {id}");
            }
            return Content("Welcome Guest");
        }
    }
}

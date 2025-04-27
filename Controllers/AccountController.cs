using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using mvc_project.Filtrers;
using mvc_project.Models;
using mvc_project.ModelView;
using System.Security.Claims;

namespace mvc_project.Controllers
{
    [HandelError]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController
            (UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//requets.form['_requetss]
        public async Task<IActionResult> SaveRegister
            (RegisterViewModel UserViewModel)
        {
            if (ModelState.IsValid)
            {
                //Mapping
                ApplicationUser appUser = new ApplicationUser();
                appUser.Address = UserViewModel.Address;
                appUser.UserName = UserViewModel.UserName;
                appUser.PasswordHash = UserViewModel.Password;

                //save database
                IdentityResult result =
                    await userManager.CreateAsync(appUser, UserViewModel.Password);
                if (result.Succeeded)
                {
                    //assign to role
                    await userManager.AddToRoleAsync(appUser, "Admin");//السطر ده لما اكون عاوزه كل الى يدخل adminيبقى عاوزه اغير ال
                    //Cookie
                    await signInManager.SignInAsync(appUser, false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View("Register", UserViewModel);
        }

        public IActionResult Login()
        {
            return View("Login");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]//requets.form['_requetss]
        public async Task<IActionResult> SaveLogin(LoginUserViewModel userViewModel)
        {
            if (ModelState.IsValid == true)
            {
                //check found 
                ApplicationUser appUser =
                    await userManager.FindByNameAsync(userViewModel.Name);
                if (appUser != null)
                {
                    bool found =
                         await userManager.CheckPasswordAsync(appUser, userViewModel.Password);
                    if (found == true)
                    {
                        List<Claim> Claims = new List<Claim>();

                        Claims.Add(new Claim("UserAddress", appUser.Address));
                        await signInManager.SignInWithClaimsAsync(appUser, userViewModel.RememberMe, Claims);
                        //await signInManager.SignInAsync(appUser, userViewModel.RememberMe);
                        return RedirectToAction("Index", "Home");
                    }

                }
                ModelState.AddModelError("", "Username OR PAssword wrong");
                //create cookie
            }
            return View("Login", userViewModel);
        }

        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return View("register");
        }
    }
}

using IKEA.DAL.Models.Identity;
using IKEA.PL.ViewModel.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        #region Services
        public AccountController(UserManager<ApplicationUser> userManager)
        {

                this.userManager = userManager;
        }
        #endregion
        #region SignUp
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel signUpViewModel)
        {
            if(!ModelState.IsValid)
                    return BadRequest();

            var User =await userManager.FindByNameAsync(signUpViewModel.UserName);

            if(User is not null)
            {
                ModelState.AddModelError(nameof(SignUpViewModel.UserName), "This Username is already in Use for another Account");
                return View(signUpViewModel);
            }

            User = new ApplicationUser()
            {
                FName = signUpViewModel.FirstName,
                LName = signUpViewModel.LastName,
                UserName = signUpViewModel.UserName,
                Email = signUpViewModel.Email,
                IsAgree = signUpViewModel.IsAgree,
            };
            var Result =await userManager.CreateAsync(User, signUpViewModel.Password); 
            if(Result.Succeeded)
                return RedirectToAction(nameof(LogIn));

            foreach (var error in Result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(signUpViewModel);



        }
        #endregion

        #region LogIn
        public IActionResult LogIn()
        {
            return View();
        } 
        #endregion
    }
}

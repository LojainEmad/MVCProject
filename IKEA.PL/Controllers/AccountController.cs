using IKEA.DAL.Models.Identity;
using IKEA.PL.ViewModel.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace IKEA.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        #region Services
        public AccountController(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager)
        {

                this.userManager = userManager;
            this.signInManager = signInManager;
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

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var User = await userManager.FindByEmailAsync(loginViewModel.Email);

            if(User is not null)
            {
                var result =await signInManager.PasswordSignInAsync(User, loginViewModel.Password ,loginViewModel.RememberMe ,true);

                if (result.IsNotAllowed)
                    ModelState.AddModelError(string.Empty, "Your Account Is Not Confirmed ");

                if (result.IsLockedOut)
                    ModelState.AddModelError(string.Empty, "Your Account Is Locked!");

                if(result.Succeeded)
                    return RedirectToAction(nameof(HomeController.Index) , "Home");

            }

            ModelState.AddModelError(String.Empty, "Invalid Login Attempt!");
            return View(loginViewModel);
        }
        #endregion
    }
}

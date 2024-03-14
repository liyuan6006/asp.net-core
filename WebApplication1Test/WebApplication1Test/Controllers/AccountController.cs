using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1Test.Models;
using WebApplication1Test.Repository;

namespace WebApplication1Test.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        [Route("signup")]
        public IActionResult Signup()
        {
            return View();
        }

        [Route("signup")]
        [HttpPost]
        public async Task<IActionResult> Signup(SignUpUserModel userModel)
        {
            if(ModelState.IsValid)
            {
                var result = await _accountRepository.CreateUserAsync(userModel);

                if (!result.Succeeded)
                {
                    foreach (var errorMesaage in result.Errors)
                    {
                        ModelState.AddModelError("", errorMesaage.Description);
                    }
                    return View(userModel);
                }
               
                ModelState.Clear();
            }
            return View();
        }


        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(SignInModel signInModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.PasswordSignInAsync(signInModel);

                if (result.Succeeded)
                {
                    // Redirect to a protected page or the home page upon successful sign-in
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(signInModel);
                }
            }

            // If ModelState is not valid, return the view with validation errors
            return View();
        }
    }
}

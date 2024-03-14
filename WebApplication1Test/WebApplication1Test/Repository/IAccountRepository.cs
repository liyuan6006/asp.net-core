using Microsoft.AspNetCore.Identity;
using WebApplication1Test.Models;

namespace WebApplication1Test.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateUserAsync(SignUpUserModel userModel);
        Task<SignInResult> PasswordSignInAsync(SignInModel signInModel);
    }
}
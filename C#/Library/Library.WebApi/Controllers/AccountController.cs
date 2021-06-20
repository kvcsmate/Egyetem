using Library.Persistence;
using Library.Persistence.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace Library.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly  SignInManager<ApplicationUser> _signInManager;

        public AccountController(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }
        //api/Account/login
        //
        [HttpPost]
        public  async Task<IActionResult> Login(LoginDto login)
        {
            var user =  await _signInManager.UserManager.FindByNameAsync(login.UserName);
            var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, false, false);
            bool isauth = await _signInManager.UserManager.IsInRoleAsync(user, "Librarian");
            if (result.Succeeded && isauth)
            {
                return Ok();
            }
            return Unauthorized("Login failed!");
        }
        //api/Account/Logout
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}

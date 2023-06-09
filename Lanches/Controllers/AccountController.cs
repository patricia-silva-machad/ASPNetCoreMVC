﻿using Lanches.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Lanches.Controllers {
    public class AccountController : Controller 
    {
      //injeção de instancias
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager) 
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        // [HttpGet]
        public IActionResult Login(string returnUrl) 
        {
            return View(new LoginViewModel() 
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost] //post é obrigatorio colocar o verbo http
        public async Task<IActionResult> Login(LoginViewModel loginVM) 
        {
            //verifica se o modelState esta valido, se nao estiver retorna a view LoginVM
            if(!ModelState.IsValid) 
                return View(loginVM);

            // se for valido, vai tentar localizar o usuario pelo nome.
            var user = await _userManager.FindByNameAsync(loginVM.UserName);

            if(user !=null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                if(result.Succeeded) 
                {
                    if(string.IsNullOrEmpty(loginVM.ReturnUrl)) 
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return Redirect(loginVM.ReturnUrl);
                }
            }

            ModelState.AddModelError("", "Falha ao realizar o login!!");
            return View(loginVM);

        }
    }
}

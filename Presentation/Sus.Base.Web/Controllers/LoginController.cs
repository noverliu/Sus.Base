using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sus.Base.Web.Model.Login;
using Sus.Base.Services.User;
using Sus.Base.Core.Domain.User;
using Sus.Base.Services.Authentication;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Sus.Base.Web.Controllers
{
    public class LoginController : Controller
    {
        private IUserIdentifyService _userIdentifyService;
        private IUserService _userService;
        private IAuthenticationService _authService;
        public LoginController(IUserIdentifyService userIdentifyService,
            IUserService userService,
            IAuthenticationService authService)
        {
            _userIdentifyService = userIdentifyService;
            _userService = userService;
            _authService = authService;
        }
        [AllowAnonymous]
        // GET: /<controller>/
        public IActionResult Login()
        {
            var model = new LoginModel();
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(LoginModel model)
        {
            var a = model;
            var user = _userService.GetUserByName(model.Username);
            model.ShowWarning = true;
            if (ModelState.IsValid)
            {
                var result=_userIdentifyService.ValidateUser(user, model.Password);
                switch (result)
                {
                    case LoginResult.Successful:
                        _authService.SignIn(HttpContext, user,model.Remember);
                        return RedirectToAction("Index","Home");
                    case LoginResult.UserNotExists:
                        ModelState.AddModelError("", "用户不存在");break;
                    case LoginResult.WrongPassword:
                        ModelState.AddModelError("", "密码错误");break;
                }
            }
            
            return View(model);
        }
    }
}

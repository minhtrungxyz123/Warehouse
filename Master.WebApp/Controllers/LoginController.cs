using Master.WebApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Warehouse.Data.Entities;
using Warehouse.Data.Repositories;

namespace Master.WebApp.Controllers
{
    public class LoginController : Controller
    {
        #region Fields

        private readonly IRepositoryEF<CreatedBy> _createdByrepositoryEF;

        public LoginController(IRepositoryEF<CreatedBy> repositoryEF)
        {
            _createdByrepositoryEF = repositoryEF;
        }

        #endregion Fields

        #region Method

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index([Bind] LoginViewModel user, string returnUrl)
        {
            returnUrl ??= Url.Content("~/");
            if (ModelState.IsValid)
            {
                if (ValidateAdmin(user.AccountName, user.Password))
                {
                    var users = _createdByrepositoryEF.Get(a => a.AccountName == user.AccountName).SingleOrDefault();
                    if (users != null)
                    {
                        var userClaims = new List<Claim>()
                        {
                                new Claim("AccountName", users.AccountName),
                                new Claim("Email", users.Email),
                                new Claim("Id", users.Id),
                        };
                        var userIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var authProperties = new AuthenticationProperties
                        {
                            AllowRefresh = true,
                            IsPersistent = true
                        };
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(userIdentity), authProperties);
                        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                           && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        {
                            return Redirect(returnUrl);
                        }
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, " Username does not exist ");
                        return View(user);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không chính xác.");
                    return View(user);
                }
            }
            return View(user);
        }

        public IActionResult FalseLogin()
        {
            return View();
        }

        #endregion Method

        #region Unitiels

        [AllowAnonymous]
        public bool ValidateAdmin(string username, string password)
        {
            var admin = _createdByrepositoryEF.Get(a => a.AccountName.Equals(username)).SingleOrDefault();
            return admin != null && new PasswordHasher<CreatedBy>().VerifyHashedPassword(new CreatedBy(), admin.Password, password) == PasswordVerificationResult.Success;
        }

        #endregion Unitiels
    }
}
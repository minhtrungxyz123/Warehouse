using Master.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Master.WebApp.Controllers
{
    public class LoginController : Controller
    {
        #region Fields

        private readonly ICreatedByService _createdByService;
        public LoginController(ICreatedByService createdByService)
        {
            _createdByService = createdByService;
        }

        #endregion

        #region Method
        public IActionResult Index()
        {
            return View();
        }



        #endregion

        #region Unitiels

        //[AllowAnonymous]
        //public async bool ValidateAdmin(string username, string password)
        //{
        //    var admin = _createdByService.GetAll(a => a.AccountName.Equals(username)).SingleOrDefault();
        //    return admin != null && new PasswordHasher<User>().VerifyHashedPassword(new User(), admin.Password, password) == PasswordVerificationResult.Success;
        //}

        #endregion Unitiels
    }
}

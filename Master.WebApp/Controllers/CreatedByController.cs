using Master.WebApp.ApiClient;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Data.Entities;
using Warehouse.Model.CreatedBy;

namespace Master.WebApp.Controllers
{
    [Authorize]
    public class CreatedByController : BaseController
    {
        #region Fields

        private readonly ICreatedByApiClient _createdByApiClient;

        public CreatedByController(ICreatedByApiClient createdByApiClient)
        {
            _createdByApiClient = createdByApiClient;
        }

        #endregion Fields

        #region List

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new GetCreatedByPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _createdByApiClient.GetPagings(request);
            ViewBag.Keyword = keyword;
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data.ResultObj);
        }

        #endregion List

        #region Method

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Token");
            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return ViewComponent("CreateCreatedBy");
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatedByModel request)
        {
            if (!ModelState.IsValid)
                return View(request);

            request.DateCreate = DateTime.Now;
            request.DateRegister = DateTime.Now;
            var hashedPassword = new PasswordHasher<CreatedBy>().HashPassword(new CreatedBy(), request.Password);
            request.Password = hashedPassword;

            var result = await _createdByApiClient.Create(request);

            if (result)
            {
                TempData["result"] = "Thêm mới thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm mới thất bại");
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string createdById)
        {
            var result = await _createdByApiClient.GetById(createdById);
            if (result.IsSuccessed)
            {
                var user = result.ResultObj;
                var updateRequest = new CreatedByModel()
                {
                    AccountName = user.AccountName,
                    FullName = user.FullName,
                    Id = createdById,
                    Avarta = user.Avarta,
                    DateCreate = user.DateCreate,
                    DateRegister = user.DateRegister,
                    Email = user.Email,
                    Password = user.Password,
                    Role = user.Role,
                };
                return ViewComponent("EditCreatedBy", updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CreatedByModel request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _createdByApiClient.Edit(request.Id, request);
            if (result)
            {
                TempData["result"] = "Sửa thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm mới thất bại");
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (!ModelState.IsValid)
                return View();
            var result = await _createdByApiClient.Delete(id);
            if (result)
            {
                TempData["result"] = "Xóa thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Xóa không thành công");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Detail(string createdById)
        {
            var result = await _createdByApiClient.GetById(createdById);
            if (result.IsSuccessed)
            {
                var user = result.ResultObj;
                var updateRequest = new CreatedByModel()
                {
                    AccountName = user.AccountName,
                    FullName = user.FullName,
                    Id = createdById,
                    Avarta = user.Avarta,
                    Role = user.Role,
                    Password = user.Password,
                    Email = user.Email,
                    DateRegister = user.DateRegister,
                    DateCreate = user.DateCreate,
                };
                return ViewComponent("DetailCreatedBy", updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        #endregion Method
    }
}
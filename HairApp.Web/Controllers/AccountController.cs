﻿using HairApp.Common.Entities;
using HairApp.Common.Enums;
using HairApp.Common.Responses;
using HairApp.Web.Data;
using HairApp.Web.Data.Entities;
using HairApp.Web.Helpers;
using HairApp.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Vereyon.Web;

namespace HairApp.Web.Controllers
{

    public class AccountController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly IBlobHelper _blobHelper;
        private readonly IMailHelper _mailHelper;
        //private readonly IConverterHelper _converterHelper;
        private readonly IFlashMessage _flashMessage;

        public AccountController(DataContext context, IUserHelper userHelper, ICombosHelper combosHelper, IBlobHelper blobHelper, IMailHelper mailHelper,
            /*IConverterHelper converterHelper,*/ IFlashMessage flashMessage)
        {
            _context = context;
            _userHelper = userHelper;
            _combosHelper = combosHelper;
            _blobHelper = blobHelper;
            _mailHelper = mailHelper;
            //_converterHelper = converterHelper;
            _flashMessage = flashMessage;
        }


        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Users
        //        .Include(u => u.Neighborhood)
        //        .ToListAsync());
        //}


        [HttpGet]
        public IActionResult Create()
        {
            AddUserViewModel model = new AddUserViewModel
            {

                Departaments = _combosHelper.GetComboDepartaments(),
                Cities = _combosHelper.GetComboCities(0),
                Neighborhoods = _combosHelper.GetComboNeighborhoods(0)

            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid imageId = Guid.Empty;

                if (model.ImageFile != null)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "users");
                }

                User user = await _userHelper.AddUserAsync(model, imageId, UserType.Admin);
                if (user == null)
                {
                    _flashMessage.Confirmation(string.Empty, "Este correo electrónico ya está en uso.");
                    model.Departaments = _combosHelper.GetComboDepartaments();
                    model.Cities = _combosHelper.GetComboCities(0);
                    model.Neighborhoods = _combosHelper.GetComboNeighborhoods(0);
                    return View(model);
                }


                string myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                string tokenLink = Url.Action("ConfirmEmail", "Account", new
                {
                    userid = user.Id,
                    token = myToken
                }, protocol: HttpContext.Request.Scheme);

                Response response = _mailHelper.SendMail(model.Username, "Email Confirmacion", $"<h1>Email Confirmacion</h1>" +
                    $"El siguien enlace le pirmitira habilitar el usuario, " +
                    $"por favor haga clic en este enlace:</br></br><a href = \"{tokenLink}\">Confirm Email</a>");
                if (response.IsSuccess)
                {
                    _flashMessage.Confirmation("Las instrucciones para permitir a su usuario se han enviado por correo electrónico.");
                    return View(model);
                }

                _flashMessage.Danger(string.Empty, response.Message);
            }

            //model.Countries = _combosHelper.GetComboCountries();
            //model.Departments = _combosHelper.GetComboDepartments(model.CountryId);
            //model.Cities = _combosHelper.GetComboCities(model.DepartmentId);
            //model.Sections = _combosHelper.GetComboSections();
            return View(model);
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == model.Username);

                if (user == null)
                {

                    _flashMessage.Danger("El usuario no existe.");
                    return View(model);

                }

                if (user.IsActive == false)
                {
                    _flashMessage.Danger("El usuario Esta deshabilitado.");
                    return View(model);
                }

                Microsoft.AspNetCore.Identity.SignInResult result = await _userHelper.LoginAsync(model);
                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(Request.Query["ReturnUrl"].First());
                    }

                    return RedirectToAction("Index", "Home");
                }

                _flashMessage.Danger("Verifique el usuario o la contraseña.");
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult NotAuthorized()
        {
            return View();
        }

        public IActionResult Register()
        {
            AddUserViewModel model = new AddUserViewModel
            {
                Departaments = _combosHelper.GetComboDepartaments(),
                Cities = _combosHelper.GetComboCities(0),
                Neighborhoods = _combosHelper.GetComboNeighborhoods(0)

            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid imageId = Guid.Empty;

                if (model.ImageFile != null)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "users");
                }

                User user = await _userHelper.AddUserAsync(model, imageId, UserType.Usuario);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "This email is already used.");
                    model.Departaments = _combosHelper.GetComboDepartaments();
                    model.Cities = _combosHelper.GetComboCities(0);
                    model.Neighborhoods = _combosHelper.GetComboNeighborhoods(0);
                    return View(model);
                }

                string myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                string tokenLink = Url.Action("ConfirmEmail", "Account", new
                {
                    userid = user.Id,
                    token = myToken
                }, protocol: HttpContext.Request.Scheme);

                Response response = _mailHelper.SendMail(model.Username, "Email Confirmacion", $"<h1>Email Confirmacion</h1>" +
                   $"El siguien enlace le pirmitira habilitar el usuario, " +
                   $"por favor haga clic en este enlace:</br></br><a href = \"{tokenLink}\">Confirm Email</a>");
                if (response.IsSuccess)
                {
                    _flashMessage.Confirmation("Las instrucciones para permitir a su usuario se han enviado por correo electrónico.");
                    return View(model);
                }

                _flashMessage.Danger(string.Empty, response.Message);
            }

            model.Departaments = _combosHelper.GetComboDepartaments();
            model.Cities = _combosHelper.GetComboCities(model.DepartamentId);
            model.Neighborhoods = _combosHelper.GetComboNeighborhoods(model.CityId);

            return View(model);
        }

        public JsonResult GetCities(int departamentId)
        {
            Departament departament = _context.Departaments
                .Include(c => c.Cities)
                .FirstOrDefault(c => c.Id == departamentId);
            if (departament == null)
            {
                return null;
            }

            return Json(departament.Cities.OrderBy(d => d.Name));
        }

        public JsonResult GetNeighborhoods(int cityId)
        {
            City city = _context.Cities
                .Include(d => d.Neighborhoods)
                .FirstOrDefault(d => d.Id == cityId);
            if (city == null)
            {
                return null;
            }

            return Json(city.Neighborhoods.OrderBy(c => c.Name));
        }

        public async Task<IActionResult> ChangeUser()
        {
            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }

            City city = await _context.Cities.FirstOrDefaultAsync(d => d.Neighborhoods.FirstOrDefault(c => c.Id == user.Neighborhood.Id) != null);
            if (city == null)
            {
                city = await _context.Cities.FirstOrDefaultAsync();
            }

            Departament departament = await _context.Departaments.FirstOrDefaultAsync(c => c.Cities.FirstOrDefault(d => d.Id == city.Id) != null);
            if (departament == null)
            {
                departament = await _context.Departaments.FirstOrDefaultAsync();
            }

            EditUserViewModel model = new EditUserViewModel
            {
                Address = user.Address,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                ImageId = user.ImageId,
                Cities = _combosHelper.GetComboCities(departament.Id),
                CityId = city.Id,
                Departaments = _combosHelper.GetComboDepartaments(),
                DepartamentId = departament.Id,
                NeighborhoodId = user.Neighborhood.Id,
                Neighborhoods = _combosHelper.GetComboNeighborhoods(city.Id),
                Id = user.Id,
                Document = user.Document,
                IsActive = user.IsActive

            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeUser(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid imageId = model.ImageId;

                if (model.ImageFile != null)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "users");
                }

                User user = await _userHelper.GetUserAsync(User.Identity.Name);

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Address = model.Address;
                user.PhoneNumber = model.PhoneNumber;
                user.ImageId = imageId;
                user.Neighborhood = await _context.Neighborhoods.FindAsync(model.NeighborhoodId);
                user.Document = model.Document;
                user.IsActive = model.IsActive;


                await _userHelper.UpdateUserAsync(user);
                return RedirectToAction("Index", "Home");
            }

            model.Departaments = _combosHelper.GetComboDepartaments();
            model.Cities = _combosHelper.GetComboCities(model.DepartamentId);
            model.Neighborhoods = _combosHelper.GetComboNeighborhoods(model.CityId);
            return View(model);
        }

        public IActionResult ChangePasswordMVC()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePasswordMVC(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserAsync(User.Identity.Name);
                if (user != null)
                {
                    var result = await _userHelper.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("ChangeUser");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault().Description);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User no found.");
                }
            }

            return View(model);
        }
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return NotFound();
            }

            User user = await _userHelper.GetUserAsync(new Guid(userId));
            if (user == null)
            {
                return NotFound();
            }

            IdentityResult result = await _userHelper.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return NotFound();
            }

            return View();
        }

        public IActionResult RecoverPasswordMVC()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RecoverPasswordMVC(RecoverPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userHelper.GetUserAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "The email doesn't correspont to a registered user.");
                    return View(model);
                }

                string myToken = await _userHelper.GeneratePasswordResetTokenAsync(user);
                string link = Url.Action(
                    "ResetPassword",
                    "Account",
                    new { token = myToken }, protocol: HttpContext.Request.Scheme);
                _mailHelper.SendMail(model.Email,"Password Reset", $"<h1>Password Reset</h1>" +
                    $"To reset the password click in this link:</br></br>" +
                    $"<a href = \"{link}\">Reset Password</a>");
                ViewBag.Message = "The instructions to recover your password has been sent to email.";
                return View();

            }

            return View(model);
        }

        public IActionResult ResetPassword(string token)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            User user = await _userHelper.GetUserAsync(model.UserName);
            if (user != null)
            {
                IdentityResult result = await _userHelper.ResetPasswordAsync(user, model.Token, model.Password);
                if (result.Succeeded)
                {
                    ViewBag.Message = "Password reset successful.";
                    return View();
                }

                ViewBag.Message = "Error while resetting the password.";
                return View(model);
            }

            ViewBag.Message = "User not found.";
            return View(model);
        }
    }

}



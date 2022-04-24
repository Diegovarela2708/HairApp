using HairApp.Common.Entities;
using HairApp.Common.Enums;
using HairApp.Common.Responses;
using HairApp.Web.Data;
using HairApp.Web.Data.Entities;
using HairApp.Web.Helpers;
using HairApp.Web.Models;
using Microsoft.AspNetCore.Authorization;
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

        public AccountController(DataContext context,IUserHelper userHelper,ICombosHelper combosHelper,IBlobHelper blobHelper,IMailHelper mailHelper,
            /*IConverterHelper converterHelper,*/ IFlashMessage flashMessage)
        {
            _context = context;
            _userHelper = userHelper;
            _combosHelper = combosHelper;
            _blobHelper = blobHelper;
            //_mailHelper = mailHelper;
            //_converterHelper = converterHelper;
            _flashMessage = flashMessage;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users
                .Include(u => u.Neighborhood)                                
                .ToListAsync());
        }

        [Authorize(Roles = "Admin")]
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

                Response response = _mailHelper.SendMail(model.Username,"Email Confirmacion", $"<h1>Email Confirmacion</h1>" +
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

                Response response = _mailHelper.SendMail(model.Username,"Email Confirmacion", $"<h1>Email Confirmacion</h1>" +
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

        //public async Task<IActionResult> ChangeUser()
        //{
        //    User user = await _userHelper.GetUserAsync(User.Identity.Name);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    Department department = await _context.Departments.FirstOrDefaultAsync(d => d.Cities.FirstOrDefault(c => c.Id == user.City.Id) != null);
        //    if (department == null)
        //    {
        //        department = await _context.Departments.FirstOrDefaultAsync();
        //    }

        //    Country country = await _context.Countries.FirstOrDefaultAsync(c => c.Departments.FirstOrDefault(d => d.Id == department.Id) != null);
        //    if (country == null)
        //    {
        //        country = await _context.Countries.FirstOrDefaultAsync();
        //    }

        //    EditUserViewModel model = new EditUserViewModel
        //    {
        //        Address = user.Address,
        //        FirstName = user.FirstName,
        //        LastName = user.LastName,
        //        PhoneNumber = user.PhoneNumber,
        //        ImageId = user.ImageId,
        //        Sections = _combosHelper.GetComboSections(),
        //        SectionId = user.Section.Id,
        //        Cities = _combosHelper.GetComboCities(department.Id),
        //        CityId = user.City.Id,
        //        Countries = _combosHelper.GetComboCountries(),
        //        CountryId = country.Id,
        //        DepartmentId = department.Id,
        //        Departments = _combosHelper.GetComboDepartments(country.Id),
        //        Id = user.Id,
        //        Document = user.Document,
        //        IsActive = user.IsActive

        //    };

        //    return View(model);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ChangeUser(EditUserViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Guid imageId = model.ImageId;

        //        if (model.ImageFile != null)
        //        {
        //            imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "users");
        //        }

        //        User user = await _userHelper.GetUserAsync(User.Identity.Name);

        //        user.FirstName = model.FirstName;
        //        user.LastName = model.LastName;
        //        user.Address = model.Address;
        //        user.PhoneNumber = model.PhoneNumber;
        //        user.ImageId = imageId;
        //        user.Section = await _context.Sections.FindAsync(model.SectionId);
        //        user.City = await _context.Cities.FindAsync(model.CityId);
        //        user.Document = model.Document;
        //        user.IsActive = model.IsActive;


        //        await _userHelper.UpdateUserAsync(user);
        //        return RedirectToAction("Index", "Home");
        //    }

        //    model.Cities = _combosHelper.GetComboCities(model.DepartmentId);
        //    model.Countries = _combosHelper.GetComboCountries();
        //    model.Departments = _combosHelper.GetComboDepartments(model.CityId);
        //    model.Sections = _combosHelper.GetComboSections();
        //    return View(model);
        //}

        //public IActionResult ChangePasswordMVC()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> ChangePasswordMVC(ChangePasswordViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userHelper.GetUserAsync(User.Identity.Name);
        //        if (user != null)
        //        {
        //            var result = await _userHelper.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
        //            if (result.Succeeded)
        //            {
        //                return RedirectToAction("ChangeUser");
        //            }
        //            else
        //            {
        //                ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault().Description);
        //            }
        //        }
        //        else
        //        {
        //            ModelState.AddModelError(string.Empty, "User no found.");
        //        }
        //    }

        //    return View(model);
        //}
        //public async Task<IActionResult> ConfirmEmail(string userId, string token)
        //{
        //    if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
        //    {
        //        return NotFound();
        //    }

        //    User user = await _userHelper.GetUserAsync(new Guid(userId));
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    IdentityResult result = await _userHelper.ConfirmEmailAsync(user, token);
        //    if (!result.Succeeded)
        //    {
        //        return NotFound();
        //    }

        //    return View();
        //}

        //public IActionResult RecoverPasswordMVC()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> RecoverPasswordMVC(RecoverPasswordViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        User user = await _userHelper.GetUserAsync(model.Email);
        //        if (user == null)
        //        {
        //            ModelState.AddModelError(string.Empty, "The email doesn't correspont to a registered user.");
        //            return View(model);
        //        }

        //        string myToken = await _userHelper.GeneratePasswordResetTokenAsync(user);
        //        string link = Url.Action(
        //            "ResetPassword",
        //            "Account",
        //            new { token = myToken }, protocol: HttpContext.Request.Scheme);
        //        _mailHelper.SendMail(model.Email,"","", "Password Reset", $"<h1>Password Reset</h1>" +
        //            $"To reset the password click in this link:</br></br>" +
        //            $"<a href = \"{link}\">Reset Password</a>");
        //        ViewBag.Message = "The instructions to recover your password has been sent to email.";
        //        return View();

        //    }

        //    return View(model);
        //}

        //public IActionResult ResetPassword(string token)
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        //{
        //    User user = await _userHelper.GetUserAsync(model.UserName);
        //    if (user != null)
        //    {
        //        IdentityResult result = await _userHelper.ResetPasswordAsync(user, model.Token, model.Password);
        //        if (result.Succeeded)
        //        {
        //            ViewBag.Message = "Password reset successful.";
        //            return View();
        //        }

        //        ViewBag.Message = "Error while resetting the password.";
        //        return View(model);
        //    }

        //    ViewBag.Message = "User not found.";
        //    return View(model);
        //}


        //public async Task<IActionResult> Details(string  id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var user = await _context.Users
        //        .Include(u=> u.Products)
        //        .ThenInclude(p=>p.Category)
        //        .Include(u => u.Products)
        //        .ThenInclude(p => p.Histories)
        //        .Include(u => u.Products)
        //        .ThenInclude(p => p.ProductImages)
        //        .FirstOrDefaultAsync(u => u.Id == id);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(user);
        //}


        //public async Task<IActionResult> DetailsProduct(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var product = await _context.Products
        //        .Include(p => p.User)
        //        .Include(p => p.Histories)
        //        .ThenInclude(h => h.ServiceType)
        //        .Include(p=>p.ProductImages)
        //        .Include(p=>p.Category)
        //        .FirstOrDefaultAsync(p => p.Id == id.Value);

        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(product);
        //}
        //public async Task<IActionResult> AddHistory(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var pet = await _context.Products.FindAsync(id.Value);
        //    if (pet == null)
        //    {
        //        return NotFound();
        //    }

        //    var model = new HistoryViewModel
        //    {
        //        Date = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd HH:mm")),
        //        ProductId = pet.Id,
        //        ServiceTypes = _combosHelper.GetComboServiceTypes(),
        //    };

        //    return View(model);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> AddHistory(HistoryViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var history = await _converterHelper.ToHistoryAsync(model, true);
        //        _context.Histories.Add(history);
        //        await _context.SaveChangesAsync();
        //        _flashMessage.Confirmation("Historia del Producto creada.");
        //        return RedirectToAction($"{nameof(DetailsProduct)}/{model.ProductId}");
        //    }
        //    model.ServiceTypes = _combosHelper.GetComboServiceTypes();
        //    return View(model);
        //}

        //public async Task<IActionResult> EditHistory(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var history = await _context.Histories
        //        .Include(h => h.Product)
        //        .Include(h => h.ServiceType)
        //        .FirstOrDefaultAsync(h => h.Id == id.Value);
        //    if (history == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(_converterHelper.ToHistoryViewModel(history));
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> EditHistory(HistoryViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var history = await _converterHelper.ToHistoryAsync(model, false);
        //        _context.Histories.Update(history);
        //        await _context.SaveChangesAsync();
        //        _flashMessage.Confirmation("Cambios guardados.");
        //        return RedirectToAction($"{nameof(DetailsProduct)}/{model.ProductId}");
        //    }
        //    model.ServiceTypes = _combosHelper.GetComboServiceTypes();
        //    return View(model);
        //}

        //public async Task<IActionResult> DeleteHistory(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var history = await _context.Histories
        //        .Include(h => h.Product)
        //        .FirstOrDefaultAsync(h => h.Id == id.Value);
        //    if (history == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Histories.Remove(history);
        //    await _context.SaveChangesAsync();
        //    _flashMessage.Confirmation("Historia eliminada.");
        //    return RedirectToAction($"{nameof(DetailsProduct)}/{history.Product.Id}");
        //}

        //public async Task<IActionResult> DeleteProduct(int? id)
        //{
        //    var product = await _context.Products
        //            .Include(p => p.User)
        //            .Include(p => p.Histories)
        //            .Include(p => p.ProductImages)
        //            .FirstOrDefaultAsync(p => p.Id == id.Value);

        //    try
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        if (product == null)
        //        {
        //            return NotFound();
        //        }

        //        if (product.Histories.Count > 0)
        //        {
        //            _flashMessage.Danger("The product can't be deleted because it has related records.");
        //            return RedirectToAction($"{nameof(Details)}/{product.User.Id}");
        //        }

        //        _context.Products.Remove(product);
        //        await _context.SaveChangesAsync();
        //        _flashMessage.Confirmation("Producto Eliminado.");
        //        return RedirectToAction($"{nameof(Details)}/{product.User.Id}");
        //    }
        //    catch (Exception ex)
        //    {

        //        _flashMessage.Danger(ex.Message);

        //    }
        //    return RedirectToAction($"{nameof(Details)}/{product.User.Id}");

        //}
    }

}



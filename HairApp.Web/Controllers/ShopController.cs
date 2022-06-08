using HairApp.Common.Entities;
using HairApp.Common.Entities.Enums;
using HairApp.Common.Enums;
using HairApp.Common.Responses;
using HairApp.Web.Data;
using HairApp.Web.Data.Entities;
using HairApp.Web.Helpers;
using HairApp.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vereyon.Web;

namespace HairApp.Web.Controllers
{
    public class ShopController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly IBlobHelper _blobHelper;
        private readonly IMailHelper _mailHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IFlashMessage _flashMessage;

        public ShopController(DataContext context, IUserHelper userHelper, ICombosHelper combosHelper, IBlobHelper blobHelper, IMailHelper mailHelper,
            IConverterHelper converterHelper, IFlashMessage flashMessage)
        {
            _context = context;
            _userHelper = userHelper;
            _combosHelper = combosHelper;
            _blobHelper = blobHelper;
            _mailHelper = mailHelper;
            _converterHelper = converterHelper;
            _flashMessage = flashMessage;
        }

        [Authorize(Roles = "SuperAdmin,Admin,Usuario")]
        public async Task<IActionResult> Index()
        {
            User user = await _userHelper.GetUserAsync(User.Identity.Name);

            if (user.UserType == 0)
            {
                return View(await _context.Shops
                .Include(s => s.Neighborhood)
                .Include(s => s.Services)
                .ToListAsync());
            }
            else if ((int)user.UserType == 1)
            {
                return View(await _context.Shops
                .Include(s => s.Neighborhood)
                .Include(s => s.Services)
                .Where(p => p.User.Id == user.Id)
                .ToListAsync());
            }
            else
            {
                return View(await _context.Shops
               .Include(s => s.Neighborhood)
               .Include(s => s.Services)
               .Where(p => p.Neighborhood.Id == user.Neighborhood.Id)
               .ToListAsync());
            }
           
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult Create()
        {
            ShopViewModel model = new ShopViewModel
            {
                Departaments = _combosHelper.GetComboDepartaments(),
                Cities = _combosHelper.GetComboCities(0),
                Neighborhoods = _combosHelper.GetComboNeighborhoods(0)
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ShopViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userHelper.GetUserAsync(User.Identity.Name);
                try
                {
                    Shop Shop = await _converterHelper.ToShopAsync(model, true,user);

                    if (model.ImageFile != null)
                    {
                        Guid imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "shopimages");
                        Shop.ShopImages = new List<ShopImage>
                        {
                            new ShopImage { ImageId = imageId }
                        };
                    }

                    Shop.User = user;
                    _context.Add(Shop);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            model.Departaments = _combosHelper.GetComboDepartaments();
            model.Cities = _combosHelper.GetComboCities(0);
            model.Neighborhoods = _combosHelper.GetComboNeighborhoods(0);
            return View(model);
        }
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                

                if (id == null)
                {
                    return NotFound();
                }

               

                Shop shop = await _context.Shops
                    .Include(s => s.Neighborhood)
                    .Include(s => s.Services)
                    .FirstOrDefaultAsync(p => p.Id == id);
                if (shop == null)
                {
                    return NotFound();
                }

                City city = await _context.Cities.FirstOrDefaultAsync(d => d.Neighborhoods.FirstOrDefault(c => c.Id == shop.Neighborhood.Id) != null);
                if (city == null)
                {
                    city = await _context.Cities.FirstOrDefaultAsync();
                }

                Departament departament = await _context.Departaments.FirstOrDefaultAsync(c => c.Cities.FirstOrDefault(d => d.Id == city.Id) != null);
                if (departament == null)
                {
                    departament = await _context.Departaments.FirstOrDefaultAsync();
                }

                ShopViewModel model = _converterHelper.ToShopViewModel(shop,city, departament);
                return View(model);
            }
            catch (Exception)
            {

                throw;
            }

           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ShopViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userHelper.GetUserAsync(User.Identity.Name);
                try
                {
                    Shop Shop = await _converterHelper.ToShopAsync(model, false, user);

                    if (model.ImageFile != null)
                    {
                        Guid imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "products");
                        if (Shop.ShopImages == null)
                        {
                            Shop.ShopImages = new List<ShopImage>();
                        }

                        Shop.ShopImages.Add(new ShopImage { ImageId = imageId });
                    }

                    _context.Update(Shop);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            model.Departaments = _combosHelper.GetComboDepartaments();
            model.Cities = _combosHelper.GetComboCities(0);
            model.Neighborhoods = _combosHelper.GetComboNeighborhoods(0);
            return View(model);
        }
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Shop shop = await _context.Shops
                .Include(p => p.ShopImages)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (shop == null)
            {
                return NotFound();
            }

            try
            {
                _context.Shops.Remove(shop);
                await _context.SaveChangesAsync();
                _flashMessage.Confirmation("Producto eliminado");
            }
            catch (Exception ex)
            {
                _flashMessage.Danger(string.Empty, ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "SuperAdmin,Admin,Usuario")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Shop shop = await _context.Shops
                .Include(s => s.Neighborhood)
                .Include(s => s.Services)
                .Include(s=>s.ShopImages)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (shop == null)
            {
                return NotFound();
            }

            return View(shop);
        }
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> AddService(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Shop shop = await _context.Shops.FindAsync(id.Value);
            if (shop == null)
            {
                return NotFound();
            }

            var model = new ServiceViewModel()
            {

                ShopId = shop.Id,
                IsActive = true                
            };

            return View(model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<IActionResult> AddService(ServiceViewModel model)
        {
            if (ModelState.IsValid)
            {
                Shop shop = await _context.Shops
                    .Include(p => p.Services)
                    .FirstOrDefaultAsync(p => p.Id == model.ShopId);
                try
                {
                    if (shop == null)
                    {
                        return NotFound();
                    }

                    shop.Services.Add( new Service { Name = model.Name, IsActive = model.IsActive,Description= model.Description,ServiceTime=model.ServiceTime });
                    _context.Update(shop);
                    await _context.SaveChangesAsync();
                    _flashMessage.Confirmation("El servicio se agrego exitosamente.");
                    return RedirectToAction($"{nameof(Details)}"+"/"+$"{shop.Id}");
                }
                catch (Exception exception)
                {
                    //Panel de contro
                    ModelState.AddModelError(string.Empty, exception.Message);
                }

            }
            return View(model);
        }
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> EditService(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Service service = await _context.Services
                .Include(s=>s.Shop)
                .FirstOrDefaultAsync(s=>s.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            var model = new ServiceViewModel()
            {
                Name = service.Name,
                Description = service.Description,
                IsActive = service.IsActive,
                ServiceTime = service.ServiceTime,
                Shop = service.Shop
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditService(ServiceViewModel model)
        {
            if (ModelState.IsValid)
            {
                Service service = await _context.Services
                    .Include(s => s.Shop)
                    .FirstOrDefaultAsync(p => p.Id == model.Id);
                try
                {
                    if (service == null)
                    {
                        return NotFound();
                    }

                    service.ServiceTime = Convert.ToInt32(model.Time);
                    service.IsActive = model.IsActive;
                    _context.Update(service);
                    await _context.SaveChangesAsync();
                    _flashMessage.Confirmation("El componente se edito exitosamente.");
                    return RedirectToAction($"{nameof(Details)}/{service.Shop.Id}");
                }
                catch (Exception exception)
                {
                    //Panel de contro
                    ModelState.AddModelError(string.Empty, exception.Message);
                }

            }
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

        public IActionResult Booking()
        {
            BookingViewModel model = new BookingViewModel
            {
                Date = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd HH:mm"))
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Booking(BookingViewModel model)
        {
            if (ModelState.IsValid)
            {
                Service service = await _context.Services.FirstOrDefaultAsync(s => s.Id == model.Id);
                model.Status = (char)StatusServices.Reservado;
                model.Service = service;
                User user = await _userHelper.GetUserAsync(User.Identity.Name);
                try
                {
                    Booking booking = await _converterHelper.ToBookingAsync(model, true, user);


                    var bookingDate = await _context.Bookings
                        .Include(b=>b.Service)
                        .Where(a => a.EndDate >= booking.Date && a.Date <= booking.EndDate && a.Service.Id == booking.Service.Id)
                        .FirstOrDefaultAsync();
                    DateTime today = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd HH:mm"));

                    int Prueba = today.CompareTo(booking.DateLocal);

                    if (booking.DateLocal > today)
                    {
                        _flashMessage.Warning("Verifique la fecha.");
                        return View(model);
                    }
                    else if (bookingDate != null)
                    {
                        _flashMessage.Warning("La fecha no esta disponible.");
                        return View(model);
                    }
                    else
                    {
                        //booking.User = user;
                        _context.Add(booking);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }

                    
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }          
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddImage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Shop shop = await _context.Shops.FindAsync(id);
            if (shop == null)
            {
                return NotFound();
            }

            AddShopImageViewModel model = new AddShopImageViewModel { ShopId = shop.Id };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> AddImage(AddShopImageViewModel model)
        {
            if (ModelState.IsValid)
            {


                try
                {
                    Shop shop = await _context.Shops
                    .Include(p => p.ShopImages)
                    .FirstOrDefaultAsync(p => p.Id == model.ShopId);
                    if (shop == null)
                    {
                        return NotFound();
                    }

                    Guid imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "shopimages");
                    if (shop.ShopImages == null)
                    {
                        shop.ShopImages = new List<ShopImage>();
                    }

                    shop.ShopImages.Add(new ShopImage { ImageId = imageId });
                    _context.Update(shop);
                    await _context.SaveChangesAsync();
                    return RedirectToAction($"{nameof(Details)}/{shop.Id}");

                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteImage(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            ShopImage shopimge = await _context.ShopImages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shopimge == null)
            {
                return NotFound();
            }

            Shop shop = await _context.Shops.FirstOrDefaultAsync(p => p.ShopImages.FirstOrDefault(pi => pi.Id == shopimge.Id) != null);

            try
            {

                _context.ShopImages.Remove(shopimge);
                await _context.SaveChangesAsync();
                return RedirectToAction($"{nameof(Details)}/{shop.Id}");
            }
            catch (Exception ex)
            {
                _flashMessage.Danger(ex.Message);
            }
            _flashMessage.Confirmation("Imagen eliminada");
            return RedirectToAction($"{nameof(Details)}/{shop.Id}");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteService(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Service service = await _context.Services
                .FirstOrDefaultAsync(c => c.Id == id.Value);

            if (service == null)
            {
                return NotFound();
            }

            Shop shop = await _context.Shops.FirstOrDefaultAsync(p => p.Services.FirstOrDefault(c => c.Id == service.Id) != null);
            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
            _flashMessage.Confirmation("Servicio eliminado");
            return RedirectToAction($"{nameof(Details)}/{shop.Id}");
        }
    }
}

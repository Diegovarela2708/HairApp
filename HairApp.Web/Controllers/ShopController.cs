using HairApp.Common.Entities;
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

        public async Task<IActionResult> Index()
        {
            return View(await _context.Shops
                .Include(s=>s.Neighborhood)
                .Include(s=>s.Services)
                .ToListAsync());
        }

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
                IsActive = true,
                ServiceTime = 0
            };

            return View(model);

        }

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

            };

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
    }
}

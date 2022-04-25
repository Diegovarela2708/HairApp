using HairApp.Common.Entities;
using HairApp.Common.Enums;
using HairApp.Common.Responses;
using HairApp.Web.Data;
using HairApp.Web.Data.Entities;
using HairApp.Web.Helpers;
using HairApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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
        //private readonly IConverterHelper _converterHelper;
        private readonly IFlashMessage _flashMessage;

        public ShopController(DataContext context, IUserHelper userHelper, ICombosHelper combosHelper, IBlobHelper blobHelper, IMailHelper mailHelper,
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

        public async Task<IActionResult> Edit(int? id)
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

            ShopViewModel model = new ShopViewModel();
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
                .FirstOrDefaultAsync(p => p.Id == id);
            if (shop == null)
            {
                return NotFound();
            }

            return View(shop);
        }

    }
}

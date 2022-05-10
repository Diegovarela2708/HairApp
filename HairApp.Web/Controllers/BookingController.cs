using HairApp.Web.Data;
using HairApp.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vereyon.Web;

namespace HairApp.Web.Controllers
{
    public class BookingController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly IBlobHelper _blobHelper;
        private readonly IMailHelper _mailHelper;
        //private readonly IConverterHelper _converterHelper;
        private readonly IFlashMessage _flashMessage;

        public BookingController(DataContext context, IUserHelper userHelper, ICombosHelper combosHelper, IBlobHelper blobHelper, IMailHelper mailHelper,
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
            return View(await _context.Bookings
                .Include(s => s.Service)
                .ThenInclude(s => s.Shop)
                .Include(s => s.Service)
                .Include(s=>s.User)
                .ToListAsync());
        }
    }
}

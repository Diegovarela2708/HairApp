using HairApp.Web.Data;
using HairApp.Web.Data.Entities;
using HairApp.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IConverterHelper _converterHelper;
        private readonly IFlashMessage _flashMessage;

        public BookingController(DataContext context, IUserHelper userHelper, ICombosHelper combosHelper, IBlobHelper blobHelper, IMailHelper mailHelper,
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
                return View(await _context.Bookings
                .Include(s => s.Service)
                .ThenInclude(s => s.Shop)
                .Include(s => s.Service)
                .Include(s => s.User)
                .ToListAsync());
            }
            else if ((int)user.UserType == 1)
            {
                return View(await _context.Bookings
                .Include(s => s.Service)
                .ThenInclude(s => s.Shop)
                .Include(s => s.Service)
                .Include(s => s.User)
                .Where(p => p.Service.Shop.User.Id == user.Id)
                .ToListAsync());
            }
            else
            {
                return View(await _context.Bookings
                .Include(s => s.Service)
                .ThenInclude(s => s.Shop)
                .Include(s => s.Service)
                .Include(s => s.User)
                .Include(s => s.Service)
                .ThenInclude(s => s.Shop)
                .ThenInclude(s => s.Neighborhood)
                .Where(p => p.User.Id == user.Id)
                .ToListAsync());
            }

        }
    }
}

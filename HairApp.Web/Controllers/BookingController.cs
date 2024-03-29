﻿using HairApp.Web.Data;
using HairApp.Web.Data.Entities;
using HairApp.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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

        [Authorize(Roles = "Usuario,Admin,SuperAdmin")]
        public async Task<IActionResult> ChangeStatus(string id)
        {
            if (id == string.Empty)
            {
                return NotFound();
            }
            string[] vecDatos = id.Split(",");
            char chrTipoTras = Convert.ToChar(vecDatos[1]);
            int intIdTras = Convert.ToInt32(vecDatos[0]);

            Booking booking = await _context.Bookings
                .Include(b => b.Service)
                .Include(b=>b.User)
                .FirstOrDefaultAsync(o => o.Id == intIdTras);

            if (booking == null)
            {
                return NotFound();
            }
            try
            {
                if (chrTipoTras != 'T')
                {
                    booking.Status = chrTipoTras;
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                    _flashMessage.Confirmation("Cambios realizados.");
                }
                else
                {
                    BookingHistory bookingHistory = new BookingHistory();
                    bookingHistory.Service = booking.Service;
                    bookingHistory.Status = chrTipoTras;
                    bookingHistory.User = booking.User;
                    bookingHistory.Date = booking.Date;
                    bookingHistory.EndDate = booking.EndDate;
                    _context.Add(bookingHistory);
                    await _context.SaveChangesAsync();
                    _context.Remove(booking);
                    await _context.SaveChangesAsync();
                    _flashMessage.Confirmation("Reserva Finalizada.");
                }

            }
            catch
            {
                _flashMessage.Confirmation("Error01");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

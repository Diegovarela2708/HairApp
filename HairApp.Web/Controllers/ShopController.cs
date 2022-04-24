using HairApp.Web.Data;
using HairApp.Web.Helpers;
using HairApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vereyon.Web;

namespace HairApp.Web.Controllers
{
    public class ShopController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly CombosHelper _combosHelper;

        //Comertario
        public ShopController(DataContext dataContext, CombosHelper CombosHelper)
        {
            _dataContext = dataContext;
            _combosHelper = CombosHelper;
        }

        [HttpGet]
        public IActionResult Create_shop()
        {
            AddUserViewModel model = new AddUserViewModel
            {

                Departaments = _combosHelper.GetComboDepartaments(),
                Cities = _combosHelper.GetComboCities(0),
                Neighborhoods = _combosHelper.GetComboNeighborhoods(0)

            };

            return View(model);            
        }
    }
}

using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HairApp.Web.Helpers
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboDepartaments();
        IEnumerable<SelectListItem> GetComboCities(int departamentId);
        IEnumerable<SelectListItem> GetComboNeighborhoods(int cityId);

        // Viejo
        //IEnumerable<SelectListItem> GetComboCategories();

        //IEnumerable<SelectListItem> GetComboCountries();

        //IEnumerable<SelectListItem> GetComboDepartments(int countryId);

        //IEnumerable<SelectListItem> GetComboCities(int departmentId);

        //IEnumerable<SelectListItem> GetComboSections();

        //IEnumerable<SelectListItem> GetComboUsers();

        //IEnumerable<SelectListItem> GetOrderStatuses();

        //IEnumerable<SelectListItem> GetComboProducts(string  Id);

        //IEnumerable<SelectListItem> GetComboProducts();

        //IEnumerable<SelectListItem> GetComboRequestTipes();

        //IEnumerable<SelectListItem> GetComboRequestCategories(int requestTypeId);

        //IEnumerable<SelectListItem> GetComboUrgecyTypes(int requestCategotyId);

        //IEnumerable<SelectListItem> GetComboServiceTypes();

        //IEnumerable<SelectListItem> GetComboComplemntTypes();


        ////IEnumerable<SelectListItem> GetRequestCategories();

        ////IEnumerable<SelectListItem> GetRequestTypes();
        ////IEnumerable<SelectListItem> GetUrgencyTypes();



    }

}

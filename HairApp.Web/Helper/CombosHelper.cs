using HairApp.Common.Entities;
using HairApp.Common.Enums;
using HairApp.Web.Data;
using HairApp.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HairApp.Web.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _context;

        public CombosHelper(DataContext context)
        {
            _context = context;

        }

        //public IEnumerable<SelectListItem> GetComboCategories()
        //{
        //    List<SelectListItem> list = _context.Categories.Select(t => new SelectListItem
        //    {
        //        Text = t.Name,
        //        Value = $"{t.Id}"
        //    })
        //        .OrderBy(t => t.Text)
        //        .ToList();

        //    list.Insert(0, new SelectListItem
        //    {
        //        Text = "[Select a category...]",
        //        Value = "0"
        //    });

        //    return list;
        //}

        //public IEnumerable<SelectListItem> GetComboProducts(string id)
        //{
        //    List<SelectListItem> list = new List<SelectListItem>();
        //    User user = _context.Users
        //        .Include(u => u.Products)
        //        .FirstOrDefault(u => u.Id == id);

        //    if (user != null)
        //    {
        //        list = user.Products.Select(u => new SelectListItem
        //        {
        //            Text = u.Name,
        //            Value = $"{u.Id}"
        //        })
        //            .OrderBy(u => u.Text)
        //            .ToList();
        //    }
        //    list.Insert(0, new SelectListItem
        //    {
        //        Text = "Select a product...",
        //        Value = "0"
        //    });

        //    return list;
        //}


        //public IEnumerable<SelectListItem> GetComboCities(int departmentId)
        //{
        //    List<SelectListItem> list = new List<SelectListItem>();
        //    Department department = _context.Departments
        //        .Include(d => d.Cities)
        //        .FirstOrDefault(d => d.Id == departmentId);
        //    if (department != null)
        //    {
        //        list = department.Cities.Select(t => new SelectListItem
        //        {
        //            Text = t.Name,
        //            Value = $"{t.Id}"
        //        })
        //            .OrderBy(t => t.Text)
        //            .ToList();
        //    }

        //    list.Insert(0, new SelectListItem
        //    {
        //        Text = "[Select a city...]",
        //        Value = "0"
        //    });

        //    return list;
        //}

        

        //public IEnumerable<SelectListItem> GetComboUrgecyTypes(int requestCategotyId)
        //{
        //    List<SelectListItem> list = new List<SelectListItem>();

        //    RequestCategory requestCategory = _context.RequestCategories
        //        .Include(d => d.UrgencyTypes)
        //        .FirstOrDefault(d => d.Id == requestCategotyId);

        //    if (requestCategory != null)
        //    {
        //        list = requestCategory.UrgencyTypes.Select(u => new SelectListItem
        //        {
        //            Text = u.Name,
        //            Value = $"{u.Id}"
        //        })
        //            .OrderBy(u => u.Text)
        //            .ToList();
        //    }

        //    list.Insert(0, new SelectListItem
        //    {
        //        Text = "[Select a city...]",
        //        Value = "0"
        //    });

        //    return list;

        //}

        //public IEnumerable<SelectListItem> GetComboRequestTipes()
        //{
        //    List<SelectListItem> list = _context.RequestTypes.Select(r => new SelectListItem
        //    {
        //        Text = r.Name,
        //        Value = $"{r.Id}"
        //    })
        //        .OrderBy(t => t.Text)
        //        .ToList();

        //    list.Insert(0, new SelectListItem
        //    {
        //        Text = "[Select a Type...]",
        //        Value = "0"
        //    });

        //    return list;

        //}

        //public IEnumerable<SelectListItem> GetComboRequestCategories(int requestTypeId)
        //{
        //    List<SelectListItem> list = new List<SelectListItem>();
        //    RequestType requestType = _context.RequestTypes
        //        .Include(r => r.Categories)
        //        .FirstOrDefault(r => r.Id == requestTypeId);

        //    if (requestType != null)
        //    {
        //        list = requestType.Categories.Select(r => new SelectListItem
        //        {
        //            Text = r.Name,
        //            Value = $"{r.Id}"

        //        })
        //            .OrderBy(r => r.Text)
        //            .ToList();

        //        list.Insert(0, new SelectListItem
        //        {
        //            Text = "[Select a Category...]",
        //            Value = "0"
        //        });
        //    }

        //    return list;
        //}

        //public IEnumerable<SelectListItem> GetComboSections()
        //{
        //    List<SelectListItem> list = _context.Sections.Select(s => new SelectListItem
        //    {
        //        Text = s.Name,
        //        Value = $"{s.Id}"
        //    })
        //        .OrderBy(s => s.Text)
        //        .ToList();

        //    list.Insert(0, new SelectListItem
        //    {
        //        Text = "[Select a Section...]",
        //        Value = "0"

        //    });

        //    return list;
        //}

        //public IEnumerable<SelectListItem> GetOrderStatuses()
        //{
        //    return new List<SelectListItem>
        //    {
        //        new SelectListItem { Value = "0", Text = OrderStatus.Pendiente.ToString() },                
        //        new SelectListItem { Value = "1", Text = OrderStatus.Entregado.ToString() },
        //        new SelectListItem { Value = "2", Text = OrderStatus.Confirmado.ToString() },
        //        new SelectListItem { Value = "3", Text = OrderStatus.Cancelado.ToString() }
        //    };
        //}

        //public IEnumerable<SelectListItem> GetComboUsers()
        //{

        //    List<SelectListItem> list = _context.Users.Select(s => new SelectListItem
        //    {
        //        Text = s.FullName,
        //        Value = s.Id
        //    })
        //         .OrderBy(s => s.Text)
        //         .ToList();



        //    list.Insert(0, new SelectListItem
        //    {
        //        Text = "[Select a User...]",
        //        Value = "0"

        //    });

        //    return list;
        //}

        //public IEnumerable<SelectListItem> GetComboServiceTypes()
        //{
        //    var list = _context.ServiceTypes.Select(p => new SelectListItem
        //    {
        //        Text = p.Name,
        //        Value = p.Id.ToString()
        //    }).OrderBy(p => p.Text).ToList();

        //    list.Insert(0, new SelectListItem
        //    {
        //        Text = "(Select a service type...)",
        //        Value = "0"
        //    });

        //    return list;
        //}

        //public IEnumerable<SelectListItem> GetComboProducts()
        //{
        //    List<SelectListItem> list = _context.Products.Where(t=> t.User == null).Select(t =>  new SelectListItem
        //    {

        //        Text = t.Name,
        //        Value = $"{t.Id}"


        //    })

        //        .OrderBy(t => t.Text).ToList();

        //    list.Insert(0, new SelectListItem
        //    {
        //        Text = "[Select a country...]",
        //        Value = "0"
        //    });



        //    return list;
        //}

        //public IEnumerable<SelectListItem> GetComboComplemntTypes()
        //{
        //    List<SelectListItem> list = _context.ComplementTypes.Select(c => new SelectListItem
        //    {

        //        Text = c.Name,
        //        Value = $"{c.Id}"
        //    })
        //        .OrderBy(c => c.Text)
        //        .ToList();

        //    list.Insert(0, new SelectListItem
        //    {
        //        Text = "[Select a un complemento]",
        //        Value = "0"
        //    });

        //    return list;
        //}
        public IEnumerable<SelectListItem> GetComboCities(int departamentId)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            Departament departament = _context.Departaments
                .Include(c => c.Cities)
                .FirstOrDefault(c => c.Id == departamentId);
            if (departament != null)
            {
                list = departament.Cities.Select(t => new SelectListItem
                {
                    Text = t.Name,
                    Value = $"{t.Id}"
                })
                    .OrderBy(t => t.Text)
                    .ToList();
            }

            list.Insert(0, new SelectListItem
            {
                Text = "[selecione una ciudad...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboDepartaments()
        {
            List<SelectListItem> list = _context.Departaments.Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = $"{t.Id}"
            })
                .OrderBy(t => t.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un departamento...]",
                Value = "0"
            });

            return list;
        }


        public IEnumerable<SelectListItem> GetComboNeighborhoods(int cityId)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            City city = _context.Cities
                .Include(d => d.Neighborhoods)
                .FirstOrDefault(d => d.Id == cityId);
            if (city != null)
            {
                list = city.Neighborhoods.Select(t => new SelectListItem
                {
                    Text = t.Name,
                    Value = $"{t.Id}"
                })
                    .OrderBy(t => t.Text)
                    .ToList();
            }

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un barrio...]",
                Value = "0"
            });

            return list;
        }
    }
}

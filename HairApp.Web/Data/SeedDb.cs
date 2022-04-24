
using HairApp.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HairApp.Web.Data
{
    public class SeedDb
    {
        //private readonly DataContext _context;
        ////private readonly IUserHelper _userHelper;
        ////private readonly IBlobHelper _blobHelper;
        //private readonly Random _random;

        //public SeedDb(DataContext context)
        //{
        //    _context = context;
            
        //    _random = new Random();
        //}

        //public async Task SeedAsync()
        //{
        //    await _context.Database.EnsureCreatedAsync();
        //    await CheckCountriesAsync();
        //    //await CheckTequestTypeAsync();
        //    await CheckRolesAsync();
        //    await CheckSectionsAsync();
        //    await CheckUserAsync("1119217542", "Diego Fdo", "Alvarez Varela", "auxsistemas@algamar.com.co", "3107962912", "Pendiente87", UserType.Admin, true, "123456");




        //    //await CheckCategoriesAsync();
        //    //await CheckProductsAsync();
        //    //await CheckAgendasAsync();

        //}

        //private async Task CheckRolesAsync()
        //{
        //    await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
        //    await _userHelper.CheckRoleAsync(UserType.Empleado.ToString());
        //    await _userHelper.CheckRoleAsync(UserType.Jefe.ToString());

        //}

        //private async Task<User> CheckUserAsync(String document, string firstName, string lastName, string email, string phone, string address, UserType userType, bool IsActive, string clave)
        //{
        //    User user = await _userHelper.GetUserAsync(email);
        //    if (user == null)
        //    {
        //        user = new User
        //        {
        //            FirstName = firstName,
        //            LastName = lastName,
        //            Email = email,
        //            UserName = email,
        //            PhoneNumber = phone,
        //            Address = address,
        //            Document = document,
        //            City = _context.Cities.FirstOrDefault(),
        //            UserType = userType,
        //            Section = _context.Sections.FirstOrDefault(),
        //            IsActive = IsActive
        //        };

        //        await _userHelper.AddUserAsync(user, clave);
        //        await _userHelper.AddUserToRoleAsync(user, userType.ToString());

        //        string token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
        //        await _userHelper.ConfirmEmailAsync(user, token);
        //    }

        //    return user;
        //}



        ////TODO: Pendiente




        


       

        //private async Task CheckSectionsAsync()
        //{
        //    if (!_context.Sections.Any())
        //    {
        //        AddSectionAsyn("Sistemas");
        //        AddSectionAsyn("Contabilidad");
        //        AddSectionAsyn("Costos");
        //        AddSectionAsyn("Diseño");
        //        AddSectionAsyn("Comercial");
        //        AddSectionAsyn("Financiero");
        //        AddSectionAsyn("Ensamble");
        //        AddSectionAsyn("Despacho");
        //        AddSectionAsyn("Lamina");
        //        AddSectionAsyn("Herrajes");
        //        AddSectionAsyn("Pintura");
        //        AddSectionAsyn("Alambre");
        //        AddSectionAsyn("Carpinteria");
        //        AddSectionAsyn("Mejora");
        //        AddSectionAsyn("Calidad");
        //        AddSectionAsyn("Mantenimiento");
        //        AddSectionAsyn("Produccion");
        //        AddSectionAsyn("Compras");
        //        AddSectionAsyn("Gerente");
        //        AddSectionAsyn("Recurso Humano");
        //        AddSectionAsyn("SST");
        //        await _context.SaveChangesAsync();
        //    }
        //}

        //private void AddSectionAsyn(string name)
        //{
        //    string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\images", $"{name}.png");
        //    Guid imageId = await _blobHelper.UploadBlobAsync(path, "section");
        //    _context.Sections.Add(new Section { Name = name/*, ImageId = imageId*/ });
        //}

        
        ////TODO: Pendiente

        //private async Task CheckCountriesAsync()
        //{
        //    if (!_context.Countries.Any())
        //    {
        //        _context.Countries.Add(new Country
        //        {
        //            Name = "Colombia",
        //            Departments = new List<Department>
        //        {
        //            new Department
        //            {
        //                Name = "Antioquia",
        //                Cities = new List<City>
        //                {
        //                    new City { Name = "Medellín" },

        //                }
        //            }
        //        }
        //        });
        //        await _context.SaveChangesAsync();
        //    }

        //}
    }
}


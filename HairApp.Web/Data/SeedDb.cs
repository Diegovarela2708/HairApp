
using HairApp.Common.Entities;
using HairApp.Common.Enums;
using HairApp.Web.Data.Entities;
using HairApp.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HairApp.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IBlobHelper _blobHelper;
        private readonly Random _random;

        public SeedDb(DataContext context, IUserHelper userHelper, IBlobHelper blobHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _blobHelper = blobHelper;
            _random = new Random();
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckDepartamentAsync();            
            await CheckRolesAsync();
            await CheckUserAsync("1119217542", "Diego Fdo", "Alvarez Varela", "varela1044@gmail.com", "3107962912", "Direccion 1", UserType.SuperAdmin, true, "123456");
            await CheckUserAsync("1005860606", "Jhoan Sebastian", "Castillo", "sebati0908@gmail.com", "3107962912", "Direccion 2", UserType.SuperAdmin, true, "123456");
            await CheckUserAsync("1017180031", "Juan David", "Alvarez", "juandavid1990@gmail.com", "3107962912", "Direccion 5", UserType.SuperAdmin, true, "123456");
            await CheckUserAsync("12346789", "Usuario", "Usuario", "usuario@gmail.com", "3107962912", "Direccion 3", UserType.Usuario, true, "123456");
            await CheckUserAsync("123467810", "tendero", "tendero", "tendero@gmail.com", "3107962912", "Direccion 4", UserType.Admin, true, "123456");

        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.SuperAdmin.ToString());
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.Usuario.ToString());

        }

        private async Task<User> CheckUserAsync(String document, string firstName, string lastName, string email, string phone, string address, UserType userType, bool IsActive, string clave)
        {
            User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    Neighborhood = _context.Neighborhoods.FirstOrDefault(),
                    UserType = userType,
                    IsActive = IsActive
                };

                await _userHelper.AddUserAsync(user, clave);
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());

                string token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
            }

            return user;
        }





        //TODO: Pendiente

        private async Task CheckDepartamentAsync()
        {
            if (!_context.Departaments.Any())
            {
                _context.Departaments.Add(new Departament
                {
                    Name = "Antioquia",
                    Cities = new List<City>
                    {
                        new City
                        {
                            Name = "Medellin",
                            Neighborhoods = new List<Neighborhood>
                            {
                                new Neighborhood { Name = "Villa Hermosa" },
                                new Neighborhood { Name = "Marrique" },
                                new Neighborhood { Name = "Prado" },
                                new Neighborhood { Name = "Santa Fe" },
                                new Neighborhood { Name = "Laureles" },

                            }
                        },
                        new City
                        {
                            Name = "Itagui",
                            Neighborhoods = new List<Neighborhood>
                            {
                                new Neighborhood { Name = "Los Naranjos" },
                                new Neighborhood { Name = "El pedregal" },
                                new Neighborhood { Name = "El progreso" },
                                new Neighborhood { Name = "El Rosario" },
                                new Neighborhood { Name = "los Gomez" },
                            }

                        }

                    }
                });
                await _context.SaveChangesAsync();
            }

        }
    }
}


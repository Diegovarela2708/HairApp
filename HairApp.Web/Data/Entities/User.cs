using HairApp.Common.Entities;
using HairApp.Common.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HairApp.Web.Data.Entities
{
    public class User : IdentityUser
    {
        [MaxLength(20)]
        [Required]
        [Display(Name = "Documento")]
        public string Document { get; set; }

        [Display(Name = "Nombre(S)")]
        [MaxLength(50)]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Apellido(S)")]
        [MaxLength(50)]
        [Required]
        public string LastName { get; set; }
        [Display(Name = "Dirección")]
        [MaxLength(100)]
        public string Address { get; set; }

        [Display(Name = "Foto")]
        public Guid ImageId { get; set; }

        [DisplayName("Activo")]
        public bool IsActive { get; set; }
        //TODO: Direccion de Imagen
        [Display(Name = "Image")]
        public string ImageFullPath => ImageId == Guid.Empty
        ? $"https://localhost:44352/images/noimage.png"
            : $"https://blobhairapp.blob.core.windows.net/users/{ImageId}";

        [Display(Name = "Tipo Usuario")]
        public UserType UserType { get; set; }

        [Display(Name = "Barrio")]
        public Neighborhood Neighborhood { get; set; }

        [Display(Name = "Nombre Completo")]
        public string FullName => $"{FirstName} {LastName}";

        [Display(Name = "User")]
        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";

        public ICollection<Shop> Shops { get; set; }
        [Display(Name = "Numero Tiendas")]
        public int ShopsNumber => Shops == null ? 0 : Shops.Count;

    }

}

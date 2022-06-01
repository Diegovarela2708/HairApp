using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using HairApp.Common.Entities;


namespace HairApp.Web.Data.Entities
{
    public class Shop
    {
        public int Id { get; set; }
        [Display(Name = "Tienda")]
        public string Name { get; set; }
        [Display(Name = "Descripción")]
        public string Description { get; set; }
        [Display(Name = "Activa")]
        public bool IsActive { get; set; }
        [Display(Name = "Dirección")]
        public string Addrees { get; set; }
        [Display(Name = "Barrio")]
        public Neighborhood Neighborhood { get; set; }
        [Display(Name = "Saldo actual")]
        public float Balance { get; set; }
        [Display(Name = "Calificación")]
        public float StarCalification { get; set; }
        public ICollection<Service> Services { get; set; }

        [Display(Name = "Cantidad servicios")]
        public int ServicesNumber => Services == null ? 0 : Services.Count;

        public ICollection<ShopImage> ShopImages { get; set; }
        
        [DisplayName("Cantidad de Imagenes")]
        public int ProductImagesNumber => ShopImages == null ? 0 : ShopImages.Count;

        //TODO: Pendiente por cambiar la direccion local
        [Display(Name = "Imagen")]
        public string ImageFullPath => ShopImages == null || ShopImages.Count == 0
            ? $"https://localhost:44352/images/noimage.png"
            : ShopImages.FirstOrDefault().ImageFullPath;

        [JsonIgnore]
        [NotMapped]
        public int IdUser { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HairApp.Web.Data
{
    public class ShopImage
    {
        public int Id { get; set; }

        [Display(Name = "Imagen")]
        public Guid ImageId { get; set; }

        //TODO: pendiente por cambiar la  Ubicacion local de la imagen
        [Display(Name = "Imagen")]
        public string ImageFullPath => ImageId == Guid.Empty
             //? $"https://hairapp.azurewebsites.net/images/noimage.png"
             ? $"https://localhost:44352/images/noimage.png"
            : $"https://hairapp.blob.core.windows.net/shopimages/{ImageId}";
    }
}

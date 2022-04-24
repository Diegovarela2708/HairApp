using HairApp.Common.Entities;
using HairApp.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HairApp.Common.Responses
{
    public class UserResponse
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Document { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public Guid ImageId { get; set; }

        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://algamarsa.azurewebsites.net/images/noimage.png"
            : $"https://AlgamarSA.blob.core.windows.net/users/{ImageId}";

        public UserType UserType { get; set; }

        public Neighborhood Neighborhood { get; set; }        

        public string FullName => $"{FirstName} {LastName}";

        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";
    }

}

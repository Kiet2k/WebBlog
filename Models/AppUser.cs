using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace razorweb.Models
{
    public class AppUser : IdentityUser
    {
        public string HomeAdress {get;set;}
        
        [DataType(DataType.Date)]
        public DateTime? BirthDate {get;set;}
    }
}
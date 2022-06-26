using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ElectronicsShop.Models;

public class AppUser : IdentityUser
{
    [Required,MaxLength(250)]
    public string Name { get; set; }
    [Required, MaxLength(250)]
    public string Address { get; set; }
    [Required]
    public DateTime Birthdate { get; set; }


}
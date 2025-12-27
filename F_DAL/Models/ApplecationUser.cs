using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F_DAL.Models
{
    public class ApplecationUser :IdentityUser
    {
        public string FullName { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? CodeResetPassword { get; set; }
        public DateTime? paaswordResetDateExpiry { get; set; }
    }
}

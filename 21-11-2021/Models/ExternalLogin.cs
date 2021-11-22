﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace _21_11_2021.Models
{
    public class ExternalLogin
    {
        [Required]
        //[EmailAddress]
        public string Email { get; set; }
        public ClaimsPrincipal Principal { get; set; }
    }
}

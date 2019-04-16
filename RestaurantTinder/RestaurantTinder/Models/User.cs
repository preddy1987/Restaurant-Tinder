﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantTinder.Models
{
    public class User
    {  
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int ZipCode { get; set; }
        public string ConfirmPassword { get; set; }
    }
}


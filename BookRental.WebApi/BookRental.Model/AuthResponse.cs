﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BookRental.Model
{
    public class AuthResponse
    {
        public string Token { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool IsAuthenticated { get; set; } = false;
    }

}

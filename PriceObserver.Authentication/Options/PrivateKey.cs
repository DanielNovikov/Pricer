﻿using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace PriceObserver.Authentication.Options
{
    public class PrivateKey
    {
        private const string Key = "fyzEpvNsAA9Ve55S";
        
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
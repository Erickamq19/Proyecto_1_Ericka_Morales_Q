using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_1_Ericka_Morales_Q.Conexion_DB
{
    public class gestion_contrasenas
    {
        private readonly PasswordHasher hasher = new PasswordHasher();
        public string Password(string contrasena) 
        { 
            return hasher.HashPassword(contrasena);

        }

        public bool V_contrasena(string hash, string contrasena)
        {
            if (string.IsNullOrWhiteSpace(hash) || string.IsNullOrEmpty(contrasena))
            {
                return false;
                
            }

            PasswordVerificationResult verificationResult = hasher.VerifyHashedPassword(hash, contrasena);
            return verificationResult != PasswordVerificationResult.Failed;

        }
    }
}
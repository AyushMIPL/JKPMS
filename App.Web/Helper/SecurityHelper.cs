using System;
using System.Security.Cryptography;

namespace App.Web.Helper
{
    public static class SecurityHelper
    {
        public static string GenerateNonce()
        {
            byte[] nonceBytes = new byte[16];  // 128 bits is sufficient
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(nonceBytes);
            }
            return Convert.ToBase64String(nonceBytes);
        }
    }
}

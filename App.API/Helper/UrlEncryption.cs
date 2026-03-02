using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Data;
using System.Data;
using System.Net;
using System.IO;
using System.Threading;
using System.Security.Cryptography;
using System.Text;

namespace App.API.Helper
{
    public static class UrlEncryption
    {
        public static string EncryptURL(string clearText)
        {
            string EncryptionKey = "LUPHLAKMAKJ2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);

            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }

                    // Convert the encrypted data to Base64Url
                    return Convert.ToBase64String(ms.ToArray())
                        .Replace('+', '-')
                        .Replace('/', '_')
                        .TrimEnd('=');
                }
            }
        }

        public static string Decrypt(string cipherText)
        {
            try
            {
                string EncryptionKey = "LUPHLAKMAKJ2SPBNI99212";

                // Convert from Base64Url to Base64
                cipherText = cipherText.Replace('-', '+').Replace('_', '/');
                int padding = 4 - (cipherText.Length % 4);
                if (padding > 0)
                {
                    cipherText += new string('=', padding);
                }

                byte[] cipherBytes = Convert.FromBase64String(cipherText);

                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }

                        return Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                return "Error";
            }
        }


    }

}
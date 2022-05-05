using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace AgriProductTracking.util
{
    public class CustomPasswordHasher
    {
        /* public static string GenerateHash(string SourceText)
         {
             UnicodeEncoding Ue = new UnicodeEncoding();
             byte[] ByteSourceText = Ue.GetBytes(SourceText);
             MD5CryptoServiceProvider Md5 = new MD5CryptoServiceProvider();
            byte[] ByteHash = Md5.ComputeHash(ByteSourceText);
             string tmp = Convert.ToBase64String(ByteHash);
             int x = 0;
             var aStringBuilder = tmp.ToArray();
             for (x = 1; x <= tmp.Length; x++)
             {
                 if (Strings.AscW(Mid(tmp, x, 1)) < 48 | (Strings.AscW(Mid(tmp, x, 1)) > 57 & Strings.AscW(Mid(tmp, x, 1)) < 65) | (Strings.AscW(Mid(tmp, x, 1)) > 90 & Strings.AscW(Mid(tmp, x, 1)) < 97) | Strings.AscW(Mid(tmp, x, 1)) > 122)
                 {
                     //Strings.Mid(tmp, x, 1);
                     aStringBuilder[x - 1] = 'x';
                 }
             }

             return string.Join("", aStringBuilder);
         }*/
         public static string GenerateHash(string SourceText)
         {

             // generate a 128-bit salt using a cryptographically strong random sequence of nonzero values
             byte[] salt = new byte[128 / 8];
 #pragma warning disable SYSLIB0023 // Type or member is obsolete
             using (var rngCsp = new RNGCryptoServiceProvider())
             {
                 rngCsp.GetNonZeroBytes(salt);
             }
 #pragma warning restore SYSLIB0023 // Type or member is obsolete

             // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
             string hashedValue = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                 password: SourceText,
                 salt: salt,
                 prf: KeyDerivationPrf.HMACSHA256,
                 iterationCount: 100000,
                 numBytesRequested: 256 / 8));

             return hashedValue;
         }

        

        public static string Mid(string s, int a, int b)
        {
            string temp = s.Substring(a - 1, b);
            return temp;
        }

    }  
}

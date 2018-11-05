using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Common
{
    public static class Extensions
    {
        public static string ComputeHash(this Stream stream)
        {
            using (var sha = new SHA1CryptoServiceProvider())
            {
                var hash = sha.ComputeHash(stream);

                var sb = new StringBuilder(hash.Length * 2);

                foreach (var b in hash)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}

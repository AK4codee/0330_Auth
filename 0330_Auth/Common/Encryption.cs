using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace _0330_Auth.Common
{
    public static class Encryption
    {
        public static string SHA256Encryption(string strSource)
        {
            byte[] source = Encoding.Default.GetBytes(strSource);
            byte[] crypto = new SHA256CryptoServiceProvider().ComputeHash(source);

            string res = crypto.Aggregate(string.Empty, (current, t) => current + t.ToString("X2"));

            return res.ToUpper();
        }
    }
}

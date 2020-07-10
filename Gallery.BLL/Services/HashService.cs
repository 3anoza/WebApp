using System.Security.Cryptography;
using System.Text;
using Gallery.BLL.Interfaces;

namespace Gallery.BLL.Services
{
    public class HashService : IHashService
    {
        public string CompareSha256Hashes(string data)
        {
            using (SHA256 hashSha256 = SHA256.Create())
            {
                byte[] bytes = hashSha256.ComputeHash(Encoding.UTF8.GetBytes(data));

                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
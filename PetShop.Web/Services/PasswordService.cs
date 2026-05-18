using System.Security.Cryptography;
using System.Text;

namespace PetShop.Web.Services
{
    public static class PasswordService
    {
        public static string GerarHash(string senha)
        {
            using var sha = SHA256.Create();

            var bytes = Encoding.UTF8.GetBytes(senha);

            var hash = sha.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }
    }
}
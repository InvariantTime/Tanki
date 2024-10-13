using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace Tanki.Services.Hashers
{
    public class ShaHasher : IHashService
    {
        public string CreateHash(object @object)
        {
            using var sha = SHA256.Create();
            var encoding = Encoding.UTF8;

            var bytes = sha.ComputeHash(encoding.GetBytes(@object.ToString() ?? string.Empty));
            var builder = new StringBuilder();

            foreach (var @byte in bytes)
                builder.Append(@byte.ToString("x2"));

            return builder.ToString();
        }

        public bool Compare(string hash, object @object)
        {
            var other = CreateHash(@object);
            return hash == other;
        }
    }
}
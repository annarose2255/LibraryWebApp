using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCommon
{
    public class Hasher
    {
        private string ComputeSHA256Hash(string rawData)
        {
            string value;
            using (SHA256 sha = SHA256.Create())

            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                value = builder.ToString();
            }
            return value;
        }

        // value passed in needs to the salt concatenated to the password IN THAT ORDER
        // order matter, SALT + PASSWORD, not the any other way !!!
        public string HashedValue(string saltpassword)
        {
            return this.ComputeSHA256Hash(saltpassword);
        }

        public string CreateSalt()
        {
            return Guid.NewGuid().ToString();
        }
    }
}

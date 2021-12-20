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

        //// OLD        
        //public string SHA256HashWithSalt(string inInputValue, string inSalt)
        //{
        //    // declaring variables - unuseful and unnecessary comment
        //    string _hashedValue = "";
        //    byte[] _plainTextByteArray = new byte[inInputValue.Length];
        //    byte[] _saltByteArray = new byte[inSalt.Length];
        //    byte[] _plainTextWithSaltBytes = new byte[_plainTextByteArray.Length + inSalt.Length];

        //    HashAlgorithm algorithm = new SHA256Managed();

        //    // get the bytes into their byte arrays using ASCII
        //    _plainTextByteArray = Encoding.ASCII.GetBytes(inInputValue);
        //    _saltByteArray = Encoding.ASCII.GetBytes(inSalt);

        //    // get the bytes into their byte arrays using Unicode
        //    //plainTextByteArray = Encoding.Unicode.GetBytes(inInputValue);
        //    //saltByteArray = Encoding.Unicode.GetBytes(inSalt);

        //    for (int i = 0; i < _plainTextByteArray.Length; i++)
        //    {
        //        _plainTextWithSaltBytes[i] = _plainTextByteArray[i];
        //    }

        //    // add to the final array
        //    for (int i = 0; i < inSalt.Length; i++)
        //    {
        //        // notice that indexer using the end for our first array as the starting location
        //        _plainTextWithSaltBytes[_plainTextByteArray.Length + i] = _saltByteArray[i];
        //    }

        //    // hash it SHA256 style !!!! as a byte[] array
        //    var _hashed = algorithm.ComputeHash(_plainTextWithSaltBytes);

        //    // btye[] to string
        //    _hashedValue = System.Text.Encoding.Default.GetString(_hashed);

        //    return _hashedValue;

        //}

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

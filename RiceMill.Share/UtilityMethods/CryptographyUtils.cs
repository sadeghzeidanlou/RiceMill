using System.Security.Cryptography;
using System.Text;

namespace Shared.UtilityMethods
{
    public static class CryptographyUtils
    {
        public static string EncryptStringAes(this string plainText, string sharedSecret)
        {
            ArgumentException.ThrowIfNullOrEmpty(plainText);
            ArgumentException.ThrowIfNullOrEmpty(sharedSecret);
            string result;
            RijndaelManaged rijndaelManaged = null;
            try
            {
                Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(sharedSecret, Salt);
                rijndaelManaged = new RijndaelManaged();
                rijndaelManaged.Key = rfc2898DeriveBytes.GetBytes(rijndaelManaged.KeySize / 8);
                ICryptoTransform transform = rijndaelManaged.CreateEncryptor(rijndaelManaged.Key, rijndaelManaged.IV);
                using MemoryStream memoryStream = new();
                memoryStream.Write(BitConverter.GetBytes(rijndaelManaged.IV.Length), 0, 4);
                memoryStream.Write(rijndaelManaged.IV, 0, rijndaelManaged.IV.Length);
                using (CryptoStream cryptoStream = new(memoryStream, transform, CryptoStreamMode.Write))
                {
                    using StreamWriter streamWriter = new(cryptoStream);
                    streamWriter.Write(plainText);
                }
                result = Convert.ToBase64String(memoryStream.ToArray());
            }
            finally
            {
                rijndaelManaged?.Clear();
            }
            return result;
        }

        public static string DecryptStringAes(this string cipherText, string sharedSecret)
        {
            ArgumentException.ThrowIfNullOrEmpty(cipherText);
            ArgumentException.ThrowIfNullOrEmpty(sharedSecret);
            string result;
            RijndaelManaged rijndaelManaged = null;
            try
            {
                Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(sharedSecret, Salt);
                byte[] buffer = Convert.FromBase64String(cipherText);
                using MemoryStream memoryStream = new(buffer);
                rijndaelManaged = new RijndaelManaged();
                rijndaelManaged.Key = rfc2898DeriveBytes.GetBytes(rijndaelManaged.KeySize / 8);
                rijndaelManaged.IV = ReadByteArray(memoryStream);
                ICryptoTransform transform = rijndaelManaged.CreateDecryptor(rijndaelManaged.Key, rijndaelManaged.IV);
                using CryptoStream cryptoStream = new(memoryStream, transform, CryptoStreamMode.Read);
                using StreamReader streamReader = new(cryptoStream);
                result = streamReader.ReadToEnd();
            }
            finally
            {
                rijndaelManaged?.Clear();
            }
            return result;
        }

        private static byte[] ReadByteArray(Stream s)
        {
            byte[] array = new byte[4];
            bool flag = s.Read(array, 0, array.Length) != array.Length;
            if (flag)
            {
                throw new SystemException("Stream did not contain properly formatted byte array");
            }
            byte[] array2 = new byte[BitConverter.ToInt32(array, 0)];
            bool flag2 = s.Read(array2, 0, array2.Length) != array2.Length;
            if (flag2)
            {
                throw new SystemException("Did not read byte array properly");
            }
            return array2;
        }

        private static readonly byte[] Salt = Encoding.ASCII.GetBytes("o6806642kbM7c5");

        public static string ToSha512(this string inputString)
        {
            ArgumentException.ThrowIfNullOrEmpty(inputString);
            byte[] hashedBytes = SHA512.HashData(Encoding.UTF8.GetBytes(inputString));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Shared.ExtensionMethods
{
    public static class StringMethods
    {
        public static string EncryptStringAes(this string plainText, string sharedSecret)
        {
            if (plainText.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(plainText));

            if (sharedSecret.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(sharedSecret));

            string outStr;
            Aes aesAlg = Aes.Create();
            try
            {
                const int iteration = 1000;
                var Salt = Encoding.ASCII.GetBytes("o6806642kbM7c5");
                var key = new Rfc2898DeriveBytes(sharedSecret, Salt, iteration, HashAlgorithmName.SHA512);
                aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using var msEncrypt = new MemoryStream();
                msEncrypt.Write(BitConverter.GetBytes(aesAlg.IV.Length), 0, sizeof(int));
                msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using var swEncrypt = new StreamWriter(csEncrypt);
                    swEncrypt.Write(plainText);
                }
                outStr = Convert.ToBase64String(msEncrypt.ToArray());
            }
            finally
            {
                aesAlg?.Clear();
            }
            return outStr;
        }

        public static string DecryptStringAes(this string cipherText, string sharedSecret)
        {
            if (cipherText.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(cipherText));

            if (sharedSecret.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(sharedSecret));

            Aes aesAlg = Aes.Create();
            string plaintext;
            try
            {
                const int iteration = 1000;
                var Salt = Encoding.ASCII.GetBytes("o6806642kbM7c5");
                var key = new Rfc2898DeriveBytes(sharedSecret, Salt, iteration, HashAlgorithmName.SHA512);
                var bytes = Convert.FromBase64String(cipherText);
                using var msDecrypt = new MemoryStream(bytes);
                aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                aesAlg.IV = msDecrypt.ToArray();
                var decryption = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using var csDecrypt = new CryptoStream(msDecrypt, decryption, CryptoStreamMode.Read);
                using var srDecrypt = new StreamReader(csDecrypt);
                plaintext = srDecrypt.ReadToEnd();
            }
            finally
            {
                aesAlg?.Clear();
            }
            return plaintext;
        }

        public static string ToSha512(this string inputString)
        {
            var result = new StringBuilder();
            var bytesOfValue = SHA512.HashData(Encoding.UTF8.GetBytes(inputString)).ToList();
            bytesOfValue.ForEach(x => result.Append(x.ToString("x2")));
            return result.ToString();
        }

        public static bool IsAllDigit(this string str) => str.ToCharArray().Any(x => !char.IsDigit(x));

        public static string ToNotNullStandardPersianStringWithPersianNumber(this string inputString)
        {
            if (inputString.IsNullOrEmpty())
                return string.Empty;

            var sb = new StringBuilder();
            foreach (var character in inputString)
            {
                switch (character)
                {
                    case 'ك':
                    case 'ﻙ':
                        sb.Append('ک');
                        break;

                    case 'ي':
                    case 'ئ':
                        sb.Append('ی');
                        break;

                    case '۰':
                        sb.Append('0');
                        break;

                    case '۱':
                        sb.Append('1');
                        break;

                    case '۲':
                        sb.Append('2');
                        break;

                    case '۳':
                        sb.Append('3');
                        break;

                    case '۴':
                        sb.Append('4');
                        break;

                    case '۵':
                        sb.Append('5');
                        break;

                    case '۶':
                        sb.Append('6');
                        break;

                    case '۷':
                        sb.Append('7');
                        break;

                    case '۸':
                        sb.Append('8');
                        break;

                    case '۹':
                        sb.Append('9');
                        break;

                    default:
                        sb.Append(character);
                        break;
                }
            }
            return sb.ToString();
        }

        public static string ToNotNullString(this string inputString) => inputString ?? string.Empty;

        public static bool IsNullOrEmpty(this string inputString) => string.IsNullOrEmpty(inputString);

        public static bool IsNotNullOrEmpty(this string inputString) => !string.IsNullOrEmpty(inputString);

        public static string ToStringInvariantCulture(this string inputParameter, params object[] args) => inputParameter != null
            ? string.Format(CultureInfo.InvariantCulture, inputParameter, args) : string.Empty;

        public static string ToStringCurrentCulture(this string inputParameter, params object[] args) => inputParameter != null
            ? string.Format(CultureInfo.CurrentCulture, inputParameter, args) : string.Empty;

        public static bool CanConvertToBoolean(this string inputParameter) => bool.TryParse(inputParameter, out _);

        public static bool CanConvertToByte(this string inputParameter) => byte.TryParse(inputParameter, out _);

        public static bool CanConvertToShort(this string inputParameter) => short.TryParse(inputParameter, out _);

        public static bool CanConvertToInt(this string inputParameter) => int.TryParse(inputParameter, out _);

        public static bool CanConvertToLong(this string inputParameter) => long.TryParse(inputParameter, out _);

        public static bool ToBoolean(this string inputParameter)
        {
            _ = bool.TryParse(inputParameter, out var res);
            return res;
        }

        public static byte ToByte(this string inputParameter)
        {
            _ = byte.TryParse(inputParameter, out var res);
            return res;
        }

        public static short ToShort(this string inputParameter)
        {
            _ = short.TryParse(inputParameter, out var res);
            return res;
        }

        public static int ToInt(this string inputParameter)
        {
            _ = int.TryParse(inputParameter, out var res);
            return res;
        }

        public static long ToLong(this string inputParameter)
        {
            _ = long.TryParse(inputParameter, out var res);
            return res;
        }

        public static double ToDouble(this string inputParameter)
        {
            _ = double.TryParse(inputParameter, out var res);
            return res;
        }

        public static int ExtractInt32(this string inputParameter)
        {
            _ = int.TryParse(new string(inputParameter.Where(char.IsDigit).ToArray()), out var res);
            return res;
        }

        public static int? ExtractNullableInt32(this string inputParameter)
        {
            if (inputParameter.IsNullOrEmpty())
                return null;

            _ = int.TryParse(new string(inputParameter.Where(char.IsDigit).ToArray()), out var res);
            return res;
        }

        public static long ExtractInt64(this string inputParameter)
        {
            _ = long.TryParse(new string(inputParameter.Where(char.IsDigit).ToArray()), out var res);
            return res;
        }

        public static long? ExtractNullableInt64(this string inputParameter)
        {
            if (inputParameter.IsNullOrEmpty())
                return null;

            return inputParameter.ExtractInt64();
        }

        public static bool IsNotNullOrEmpty(this Guid? guid) => guid.HasValue && guid.Value != Guid.Empty && guid.Value != default;

        public static bool IsNullOrEmpty(this Guid? guid) => !guid.HasValue || guid.Value == Guid.Empty || guid.Value == default;

        public static bool IsNotNullOrEmpty(this Guid guid) => guid != Guid.Empty && guid != default;

        public static bool IsNullOrEmpty(this Guid guid) => guid == Guid.Empty || guid == default;
    }
}
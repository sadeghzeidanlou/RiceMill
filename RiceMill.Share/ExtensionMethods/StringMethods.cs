using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Shared.ExtensionMethods
{
    public static partial class StringMethods
    {
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

        public static string? MakeEmptyStringToNull(this string input) => input.Trim().IsNullOrEmpty() ? null : input;

        public static bool IsNullOrEmpty(this StringBuilder inputString) => inputString.Length < 1;

        public static bool IsNotNullOrEmpty(this StringBuilder inputString) => inputString.Length > 0;

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

        public static bool IsNotNullOrEmpty(this Guid? guid) => guid.HasValue && guid.Value.IsNotNullOrEmpty();

        public static bool IsNullOrEmpty(this Guid? guid) => !guid.HasValue || guid.Value.IsNullOrEmpty();

        public static bool IsNotNullOrEmpty(this Guid guid) => guid != Guid.Empty && guid != default;

        public static bool IsNullOrEmpty(this Guid guid) => guid == Guid.Empty || guid == default;

        public static string JsonPrettify(this string json)
        {
            using var jDoc = JsonDocument.Parse(json);
            return jDoc.SerializeObject();
        }

        [GeneratedRegex("^([0-9]{11})$")]
        private static partial Regex PhoneNumberRegex();

        public static bool IsPhoneNumber(this string number) => PhoneNumberRegex().Match(number).Success;

        [GeneratedRegex(@"^\d{3}-\d{5}$")]
        private static partial Regex MotorcyclePlateRegex();

        public static bool IsMotorcyclePlate(this string plate) => MotorcyclePlateRegex().Match(plate).Success;

        [GeneratedRegex(@"^\d{2}[\u0600-\u06FF]\d{3}-\d{2}$")]
        private static partial Regex GeneralPlateRegex();

        public static bool IsGeneralPlate(this string plate) => GeneralPlateRegex().Match(plate).Success;
    }
}
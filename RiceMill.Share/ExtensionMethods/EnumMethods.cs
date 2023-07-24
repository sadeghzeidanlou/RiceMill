using Shared.Enums;

namespace Shared.ExtensionMethods
{
    public sealed class EnumMethods
    {
        /// <summary>
        /// Get two enum and check first enum has contain second enum
        /// </summary>
        /// <typeparam name="TEnum">Enum type</typeparam>
        /// <param name="valueForVerify">First enum</param>
        /// <param name="flags">Second enum</param>
        /// <returns></returns>
        public static bool HasAllFlags<TEnum>(TEnum valueForVerify, TEnum flags) where TEnum : struct, IConvertible
        {
            var convertedSource = Convert.ToUInt64(valueForVerify);
            var convertedFlags = Convert.ToUInt64(flags);
            return (convertedSource & convertedFlags) == convertedFlags;
        }

        /// <summary>
        /// Get an enum and return all enum value that has contain on that input enum
        /// </summary>
        /// <param name="input">Input enum</param>
        /// <returns></returns>
        public static IEnumerable<Enum> GetTrueFlags(Enum input)
        {
            foreach (Enum value in Enum.GetValues(input.GetType()))
            {
                if (input.HasFlag(value))
                    yield return value;
            }
        }

        /// <summary>
        /// Get an enum and return all enum value that has not contain on that input enum
        /// </summary>
        /// <param name="input">Input enum</param>
        /// <returns></returns>
        public static IEnumerable<Enum> GetFalseFlags(Enum input)
        {
            foreach (Enum value in Enum.GetValues(input.GetType()))
            {
                if (!input.HasFlag(value))
                    yield return value;
            }
        }

        /// <summary>
        /// Get all enum items as a list of that
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static List<T> GetList<T>() => Enum.GetValues(typeof(T)).Cast<T>().ToList();
    }
}
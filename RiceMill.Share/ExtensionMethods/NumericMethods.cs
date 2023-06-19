namespace Shared.ExtensionMethods
{
    public static class NumericMethods
    {
        /// <summary>
        /// Check a byte value is in specefied range
        /// </summary>
        /// <param name="inputParameter">Source value</param>
        /// <param name="minValue">Minimum</param>
        /// <param name="maxValue">Maximum</param>
        /// <returns></returns>
        public static bool IsInRange(this byte inputParameter, byte minValue, byte maxValue) => inputParameter >= minValue && inputParameter <= maxValue;

        /// <summary>
        /// Check a short value is in specefied range
        /// </summary>
        /// <param name="inputParameter">Source value</param>
        /// <param name="minValue">Minimum</param>
        /// <param name="maxValue">Maximum</param>
        /// <returns></returns>
        public static bool IsInRange(this short inputParameter, short minValue, short maxValue) => inputParameter >= minValue && inputParameter <= maxValue;

        /// <summary>
        /// Check a int value is in specefied range
        /// </summary>
        /// <param name="inputParameter">Source value</param>
        /// <param name="minValue">Minimum</param>
        /// <param name="maxValue">Maximum</param>
        /// <returns></returns>
        public static bool IsInRange(this int inputParameter, int minValue, int maxValue) => inputParameter >= minValue && inputParameter <= maxValue;

        /// <summary>
        /// Check a long value is in specefied range
        /// </summary>
        /// <param name="inputParameter">Source value</param>
        /// <param name="minValue">Minimum</param>
        /// <param name="maxValue">Maximum</param>
        /// <returns></returns>
        public static bool IsInRange(this long inputParameter, long minValue, long maxValue) => inputParameter >= minValue && inputParameter <= maxValue;

        /// <summary>
        /// Return Number with 00:00 Format
        /// </summary>
        /// <param name="time">Number</param>
        /// <returns>00:00 format of the number</returns>
        public static string ToTimeFormat(this int time) => time.ToString().PadLeft(4, '0').Insert(4 - 2, ":");
    }
}
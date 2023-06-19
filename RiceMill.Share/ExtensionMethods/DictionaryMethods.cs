namespace Shared.ExtensionMethods
{
    public static class DictionaryMethods
    {
        /// <summary>
        /// Add an item into a dictionary with check exist and null constraint
        /// </summary>
        /// <typeparam name="T1">Dictionary key type</typeparam>
        /// <typeparam name="T2">Dictionary value type</typeparam>
        /// <param name="sourceDictionary">Source dictionary</param>
        /// <param name="key">Key object</param>
        /// <param name="value">Value object</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void SafeAdd<T1, T2>(this Dictionary<T1, T2> sourceDictionary, T1 key, T2 value)
        {
            if (sourceDictionary is null)
                throw new ArgumentNullException(nameof(sourceDictionary));

            if (!sourceDictionary.ContainsKey(key))
            {
                if (value is null && default(T2) is null)
                    throw new ArgumentNullException(nameof(value));

                sourceDictionary.Add(key, value!);
            }
        }
    }
}
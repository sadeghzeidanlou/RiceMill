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
#pragma warning disable CS8714 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'notnull' constraint.
        public static void SafeAdd<T1, T2>(this Dictionary<T1, T2> sourceDictionary, T1 key, T2 value)
#pragma warning restore CS8714 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'notnull' constraint.
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
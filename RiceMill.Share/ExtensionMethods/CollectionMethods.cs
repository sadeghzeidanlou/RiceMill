using System.Data;
using System.Reflection;

namespace Shared.ExtensionMethods
{
    public static class CollectionMethods
    {
        /// <summary>
        /// Check input collection is <see langword="null"/> or empty
        /// </summary>
        /// <typeparam name="T">Input object Type</typeparam>
        /// <param name="inputCollection">Input collection</param>
        /// <returns></returns>
        public static bool IsCollectionNullOrEmpty<T>(this IEnumerable<T> inputCollection) => inputCollection == null || !inputCollection.Any();

        /// <summary>
        /// Check input collection is not null or empty
        /// </summary>
        /// <typeparam name="T">Input object Type</typeparam>
        /// <param name="inputCollection">Input collection</param>
        /// <returns></returns>
        public static bool IsCollectionNotNullOrEmpty<T>(this IEnumerable<T> inputCollection) => inputCollection != null && inputCollection.Any();

        /// <summary>
        /// Get a collection and return represented DataTable
        /// </summary>
        /// <typeparam name="T">Input object Type</typeparam>
        /// <param name="input">Input collection</param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this IEnumerable<T> input)
        {
            var dataTable = new DataTable(typeof(T).Name);
            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in props)
            {
                var type = prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType;
                if (type == null)
                    continue;

                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (var item in input)
            {
                var values = new object?[props.Length];
                for (var i = 0; i < props.Length; i++)
                {
                    var value = props[i].GetValue(item, null);
                    values[i] = value;
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
    }
}
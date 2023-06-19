using Newtonsoft.Json;
using System.Reflection;

namespace Shared.ExtensionMethods
{
    public static class ObjectMethods
    {
        /// <summary>
        /// Serialize an object
        /// </summary>
        /// <param name="source">Input object that serialized</param>
        /// <returns></returns>
        public static string SerializeObject(this object source) => JsonConvert.SerializeObject(source);

        /// <summary>
        /// Deserialize an object from string
        /// </summary>
        /// <param name="source">Input string that will be Deserialized</param>
        /// <returns></returns>
        public static T? DeserializeObject<T>(this string source) where T : class => JsonConvert.DeserializeObject<T>(source);

        /// <summary>
        /// Clones An Object
        /// </summary>
        /// <typeparam name="T">Target object</typeparam>
        /// <param name="source">Input object</param>
        /// <returns></returns>
        public static T? CloneObject<T>(this T source) where T : class => JsonConvert.DeserializeObject<T>(
            JsonConvert.SerializeObject(source, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));

        /// <summary>
        /// Compares 2 Objects
        /// </summary>
        /// <typeparam name="T">Target object</typeparam>
        /// <param name="obj1">First object</param>
        /// <param name="obj2">Second object</param>
        /// <returns>true if values of all properties are equal</returns>
        public static bool IsEqual<T>(this T obj1, T obj2) where T : class =>
            JsonConvert.SerializeObject(obj1, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }) ==
            JsonConvert.SerializeObject(obj2, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

        public static TOut? MapTo<TOut>(this object input) => JsonConvert.DeserializeObject<TOut>(JsonConvert.SerializeObject(input, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));

        /// <summary>
        /// Get changed properties of object1 based on object2
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public static List<PropertyInfo> GetChangedProperties<T>(this T obj1, T obj2) where T : class
        {
            var changed = new List<PropertyInfo>();
            foreach (PropertyInfo prop in obj1.GetType().GetProperties())
            {
                var val1 = prop.GetValue(obj1);
                var val2 = prop.GetValue(obj2);

                if (val1 != null)
                {
                    if (!val1.Equals(val2))
                        changed.Add(prop);
                }
                else
                {
                    if (val2 != null)
                        changed.Add(prop);
                }
            }
            return changed;
        }
    }
}
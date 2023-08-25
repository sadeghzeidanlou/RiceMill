using Shared.ExtensionMethods;
using System.Text.Json;

namespace RiceMill.Api.Policies
{
    public class PascalCaseNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            if (name.IsNullOrEmpty() || char.IsUpper(name[0]))
                return name;

            return char.ToUpper(name[0]) + name[1..];
        }
    }
}
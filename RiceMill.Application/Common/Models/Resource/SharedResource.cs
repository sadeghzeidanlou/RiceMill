namespace RiceMill.Application.Common.Models.Resource
{
    public class SharedResource
    {
        public static readonly string Issuer = "DoordanehRiceMillApi";

        public static readonly string Audience = "DoordanehRiceMillClient";

        public static readonly string TokenKey = "R1c3M1lLDo0rD@N3h";

        public static readonly string EncryptDecryptKey = "h3N@Dr0oDLl1M3c1R";

        public static readonly string SecurityHeaderName = "ApplicationIdentity";

        public static readonly string AuthorizationKeyName = "Authorization";

        public static readonly string JsonContentTypeName = "application/json";

        public static readonly string TokenClaimName = "UserId";
    }
}
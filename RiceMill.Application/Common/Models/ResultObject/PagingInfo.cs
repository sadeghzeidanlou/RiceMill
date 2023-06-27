namespace RiceMill.Application.Common.Models.ResultObject
{
    public class PagingInfo
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public static short DefaultPageSize => 10;

        public static short DefaultPageNumber => 1;

        public static short MaximumPageSize => 50;
    }
}
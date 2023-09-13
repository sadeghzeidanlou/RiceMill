namespace RiceMill.Application.Common.Models.ResultObject
{
    public class PagingInfo
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public static short DefaultPageSize => 1000;

        public static short DefaultPageNumber => 1;

        public static void ApplyPaging<T>(T filter, out int pageNumber, out int pageSize)
        {
            pageNumber = 0;
            pageSize = 0;
            if (filter != null)
            {
                var propertyPageNumber = typeof(T).GetProperty(nameof(PageNumber));
                if (propertyPageNumber != null)
                    pageNumber = (int)propertyPageNumber.GetValue(filter);

                var propertyPageSize = typeof(T).GetProperty(nameof(PageSize));
                if (propertyPageSize != null)
                    pageSize = (int)propertyPageSize.GetValue(filter);
            }
            pageNumber = pageNumber == 0 ? DefaultPageNumber : pageNumber;
            pageSize = pageSize == 0 ? DefaultPageSize : pageSize;
        }
    }
}
using RiceMill.Application.UseCases.ConcernServices.Dto;
using System.Reflection;

namespace RiceMill.Application.Common.Models.ResultObject
{
    public class PagingInfo
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public static short DefaultPageSize => 10;

        public static short DefaultPageNumber => 1;

        public static short MaximumPageSize => 50;

        public static void ApplyPaging<T>(T filter, out int pageNumber, out int pageSize)
        {
            pageNumber = DefaultPageNumber;
            pageSize = DefaultPageSize;
            if (filter != null)
            {
                var propertyPageNumber = typeof(T).GetProperty(nameof(PageNumber));
                if (propertyPageNumber != null)
                    pageNumber = (int)propertyPageNumber.GetValue(filter);

                var propertyPageSize = typeof(T).GetProperty(nameof(PageSize));
                if (propertyPageSize != null)
                    pageSize = (int)propertyPageSize.GetValue(filter);
            }
        }
    }
}
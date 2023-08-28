using Mapster;

namespace RiceMill.Application.Common.Models.ResultObject
{
    public sealed class PaginatedList<TOut>
    {
        public List<TOut> Items { get; set; }

        public int PageNumber { get; set; }

        public int TotalPages { get; set; }

        public int TotalCount { get; set; }

        public bool HasPreviousPage => PageNumber > 1;

        public bool HasNextPage => PageNumber < TotalPages;

        public static PaginatedList<TOut> Create<TIn>(IQueryable<TIn> source, int pageNumber, int pageSize)
        {
            var start = (pageNumber - 1) * pageSize;
            var end = start + pageSize;
            var count = source.Count();
            var items = source.Take(start..end).ToList().Adapt<List<TOut>>();
            return new PaginatedList<TOut> { TotalCount = count, Items = items, PageNumber = pageNumber, TotalPages = (int)Math.Ceiling(count / (double)pageSize) };
        }
    }
}
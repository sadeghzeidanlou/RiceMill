using Mapster;

namespace RiceMill.Application.Common.Models.ResultObject
{
    public sealed class PaginatedList<TOut>
    {
        public List<TOut> Items { get; }
        public int PageNumber { get; }
        public int TotalPages { get; }
        public int TotalCount { get; }

        public PaginatedList(List<TOut> items, int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            Items = items;
        }

        public bool HasPreviousPage => PageNumber > 1;

        public bool HasNextPage => PageNumber < TotalPages;

        public static PaginatedList<TOut> Create<TIn>(IQueryable<TIn> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var start = (pageNumber - 1) * pageSize;
            var end = start + pageSize;
            var items = source.Take(start..end).ToList().Adapt<List<TOut>>();
            return new PaginatedList<TOut>(items, count, pageNumber, pageSize);
        }
    }
}
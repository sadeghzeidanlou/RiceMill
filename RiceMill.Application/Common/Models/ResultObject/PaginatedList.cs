using Mapster;
using Microsoft.EntityFrameworkCore;

namespace RiceMill.Application.Common.Models.ResultObject
{
    public class PaginatedList<TOut>
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

        public static async Task<PaginatedList<TOut>> CreateAsync<TIn>(IQueryable<TIn> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ProjectToType<TOut>().ToListAsync();
            return new PaginatedList<TOut>(items, count, pageNumber, pageSize);
        }
    }
}
using Microsoft.EntityFrameworkCore;

namespace Mataeem.RequestHelpers
{
    public class PagedList<T> where T : class
    {
        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageCount = (int)Math.Ceiling(count / (double)pageSize);
            PageSize = pageSize;
            TotalCount = count;

            Data = items;
        }

        public int PageNumber { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }

        public List<T> Data { get; set; }

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();

            var items = await source.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}

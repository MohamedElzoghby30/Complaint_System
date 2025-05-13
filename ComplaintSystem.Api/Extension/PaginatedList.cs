using Microsoft.EntityFrameworkCore;

namespace ComplaintSystem.Api.Extension
{
    public class PaginatedList<T>(List<T> items, int count, int pageNumber, int pageSize)
    {
        public List<T> Items { get; } = items;
        public int Count { get; } = count;
        public int PageNumber { get; } = pageNumber;
        public int TotalPages => (int)Math.Ceiling(count / (double)pageSize);
        public bool HasPreviousPage => (PageNumber > 1);
        public bool HasNextPage => (PageNumber < TotalPages);


        public static async Task<PaginatedList<T>> CreateAsync(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var count =  source.Count();
            var items =  source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(items, count, pageNumber, pageSize);



        } 
    }
 } 



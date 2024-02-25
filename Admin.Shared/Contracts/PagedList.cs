using System.Text.Json.Serialization;

namespace Admin.Shared.Contracts
{
    public class PagedList<T>
    {
        public IReadOnlyCollection<T> Items { get; }
        public int PageNumber { get; } = 1;

        [JsonIgnore]
        public int TotalPages { get; }
        public int PageSize { get; }
        [JsonIgnore]
        public int Count => Items.Count;
        public int TotalCount { get; }

        [JsonIgnore]
        public bool HasPrevious => PageNumber > 1;
        [JsonIgnore]
        public bool HasNext => PageNumber < TotalPages;

        public PagedList(IReadOnlyCollection<T> items, int totalCount, int pageNumber, int pageSize)
        {
            TotalCount = totalCount;
            PageSize = pageSize;
            PageNumber = pageNumber;
            TotalPages = totalCount == 0 ? 0 : (int)Math.Ceiling(totalCount / (double)pageSize);
            Items = items.ToArray();
        }

        public static PagedList<T> Empty => new PagedList<T>(Array.Empty<T>(), 0, 1, 1);
    }
}

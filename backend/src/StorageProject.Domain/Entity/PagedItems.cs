namespace StorageProject.Domain.Entity
{
    public class PagedItems<T>
    {
        public List<T> Items { get; }
        public int Page { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public int TotalPages { get; }
        public bool HasNextPage => Page < TotalPages;
        public bool HasPreviousPage => Page > 1;

        public PagedItems(IEnumerable<T> items, int page, int pageSize)
        {
            TotalCount = items.Count();

            Page = page;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(TotalCount / (double)pageSize);
            Items = items
                .Skip((Page - 1) * PageSize)
                .Take(PageSize)
                .ToList();
        }
    }

}

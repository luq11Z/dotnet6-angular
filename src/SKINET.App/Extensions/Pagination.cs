namespace SKINET.App.Extensions
{
    public class Pagination<TEntity> where TEntity : class
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public List<TEntity> Data { get; set; }

        public Pagination(int pageIndex, int pageSize, int count, List<TEntity> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }

    }
}

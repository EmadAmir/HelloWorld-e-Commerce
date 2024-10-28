namespace HelloWorldWebAPI.RequestHelpers
{
    public class Pagination<T>
    {
        public Pagination(int pageIndex, int pageSize, int count, IReadOnlyList<T> data )
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.Count = count;
            this.Data = data;
        }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        // Count is the total number of products available after the filtering has been applied
        public int Count { get; set; } 
        public IReadOnlyList<T> Data{ get; set; }
    }
}

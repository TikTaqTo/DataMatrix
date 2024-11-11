namespace Application.Models
{
    public class PaginationResult<T>
    {
        public int AllCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int PagesAmount { get; set; }
        public List<T> List { get; set; }
    }
}

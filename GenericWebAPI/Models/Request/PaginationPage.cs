namespace GenericWebAPI.Models.Request
{
    public class PaginationPage
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 20;
    }
}

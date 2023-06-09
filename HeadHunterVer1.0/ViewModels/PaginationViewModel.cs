namespace HeadHunterVer1._0.ViewModels;

public class PaginationViewModel
{
    public int PageNumber { get; set; }
    public int TotalPages { get; set; }
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;

    public PaginationViewModel(int count, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
    }
}
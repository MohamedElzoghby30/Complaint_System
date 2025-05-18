namespace ComplaintSystem.Core
{
    public class PaginatedListCore<T,F>()
    {
      public int count  { get; set; }
      public int TotalPages { get; set;}
      public bool HasPreviousPage { get; set; }
      public bool HasNextPage { get; set; }
      public int CurrentPage { get; set; }
       public  List<F> items { get; set; }
    }
}

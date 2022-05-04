namespace Inclub.Api.Application.ViewModels
{
	public class PaginatedItemsRequestViewModel<TEntity> where TEntity : class
	{
		public int PageSize { get; set; }
		public int Skip { get; set; }
		public string SortField { get; set; }
		public string SortDir { get; set; }
		public TEntity Filter { get; set; }
	}
}

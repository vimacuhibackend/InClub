using Inclub.Api.Application.ViewModels;
using Inclub.Api.Application.ViewModels.ProductoViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inclub.Api.Application.Queries
{
    public interface IProductoQueries
	{
		Task<List<ProductoItemsDto>> ListarProductoAsync(PaginatedItemsRequestViewModel<ProductoBusquedaRequest> request);
	}
}

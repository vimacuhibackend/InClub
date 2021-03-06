using Inclub.Api.Application.ViewModels;
using Inclub.Api.Application.ViewModels.CompraDetalleViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inclub.Api.Application.Queries
{
    public interface ICompraDetalleQueries
	{
		Task<List<CompraDetalleItemsDto>> ListarCompraDetalleAsync(PaginatedItemsRequestViewModel<CompraDetalleBusquedaRequest> request);
	}
}

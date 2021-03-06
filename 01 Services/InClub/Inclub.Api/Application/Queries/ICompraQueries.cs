using Inclub.Api.Application.ViewModels;
using Inclub.Api.Application.ViewModels.CompraViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inclub.Api.Application.Queries
{
    public interface ICompraQueries
	{
		Task<List<CompraItemsDto>> ListarCompraAsync(PaginatedItemsRequestViewModel<CompraBusquedaRequest> request);
	}
}

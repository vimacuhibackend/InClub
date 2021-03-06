using Inclub.Api.Application.ViewModels;
using Inclub.Api.Application.ViewModels.UsuarioViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inclub.Api.Application.Queries
{
    public interface IUsuarioQueries
	{
		Task<List<UsuarioItemsDto>> ListarUsuarioAsync(PaginatedItemsRequestViewModel<UsuarioBusquedaRequest> request);
	}
}

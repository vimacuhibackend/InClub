using System.Collections.Generic;

namespace Inclub.Domain.AggregatesModel.UsuarioAggregate
{
    public interface IUsuarioRepository : IGenericRepository<Usuario>
	{
		IEnumerable<Usuario> FindByIdAsync(int id);
	}
}

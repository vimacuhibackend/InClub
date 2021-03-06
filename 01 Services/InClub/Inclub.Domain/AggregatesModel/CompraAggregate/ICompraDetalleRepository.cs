using System.Collections.Generic;

namespace Inclub.Domain.AggregatesModel.CompraAggregate
{
    public interface ICompraDetalleRepository : IGenericRepository<CompraDetalle>
	{
		IEnumerable<CompraDetalle> FindByIdAsync(int id);
	}
}

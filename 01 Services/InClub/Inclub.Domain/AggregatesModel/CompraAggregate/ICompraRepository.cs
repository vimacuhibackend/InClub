using System.Collections.Generic;

namespace Inclub.Domain.AggregatesModel.CompraAggregate
{
    public interface ICompraRepository : IGenericRepository<Compra>
	{
		IEnumerable<Compra> FindByIdAsync(int id);
	}
}

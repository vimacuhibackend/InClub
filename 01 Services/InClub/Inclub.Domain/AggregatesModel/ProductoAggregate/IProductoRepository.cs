using System.Collections.Generic;

namespace Inclub.Domain.AggregatesModel.ProductoAggregate
{
    public interface IProductoRepository : IGenericRepository<Producto>
	{
		IEnumerable<Producto> FindByIdAsync(int id);
	}
}

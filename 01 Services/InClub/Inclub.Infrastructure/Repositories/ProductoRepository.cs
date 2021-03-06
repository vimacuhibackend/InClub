using Inclub.Domain.AggregatesModel.ProductoAggregate;
using System.Collections.Generic;
using System.Linq;

namespace Inclub.Infrastructure.Repositories
{
    public class ProductoRepository : GenericRepository<Producto>, IProductoRepository
	{
		public ProductoRepository(UnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		public IEnumerable<Producto> FindByIdAsync(int id)
		{
			return this.Get(x => x.Id == id && x.EsEliminado == false).ToList();
		}
	}
}

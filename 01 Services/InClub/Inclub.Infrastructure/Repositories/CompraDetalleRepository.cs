using Inclub.Domain.AggregatesModel.CompraAggregate;
using System.Collections.Generic;
using System.Linq;

namespace Inclub.Infrastructure.Repositories
{
    public class CompraDetalleRepository : GenericRepository<CompraDetalle>, ICompraDetalleRepository
	{
		public CompraDetalleRepository(UnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		public IEnumerable<CompraDetalle> FindByIdAsync(int id)
		{
			return this.Get(x => x.Id == id && x.EsEliminado == false).ToList();
		}
	}
}

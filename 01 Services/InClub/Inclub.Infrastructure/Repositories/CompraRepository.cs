using Inclub.Domain.AggregatesModel.CompraAggregate;
using System.Collections.Generic;
using System.Linq;

namespace Inclub.Infrastructure.Repositories
{
    public class CompraRepository : GenericRepository<Compra>, ICompraRepository
	{
		public CompraRepository(UnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		public IEnumerable<Compra> FindByIdAsync(int id)
		{
			return this.Get(x => x.Id == id && x.EsEliminado == false).ToList();
		}
	}
}

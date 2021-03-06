using Inclub.Domain.AggregatesModel.UsuarioAggregate;
using System.Collections.Generic;
using System.Linq;

namespace Inclub.Infrastructure.Repositories
{
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
	{
		public UsuarioRepository(UnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		public IEnumerable<Usuario> FindByIdAsync(int id)
		{
			return this.Get(x => x.Id == id && x.EsEliminado == false).ToList();
		}
	}
}

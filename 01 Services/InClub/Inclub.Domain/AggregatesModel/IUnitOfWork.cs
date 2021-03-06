using System.Threading.Tasks;

namespace Inclub.Domain.AggregatesModel
{
    public interface IUnitOfWork
	{
		//T Context();
		void CreateTransaction();
		void Commit();
		void Rollback();
		void Save();
		Task<bool> SaveAsync();
	}
}

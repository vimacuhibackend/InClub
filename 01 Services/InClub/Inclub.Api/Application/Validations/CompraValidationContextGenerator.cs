using Inclub.Domain.AggregatesModel;
using Inclub.Domain.AggregatesModel.CompraAggregate;
using Inclub.Domain.Validations;
using Inclub.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Inclub.Api.Application.Validations
{
	public class CompraValidationContextGenerator
	{
		private IGenericRepository<Compra> _compraRepository;
		private UnitOfWork _unitOfWork;

		public CompraValidationContextGenerator(UnitOfWork unitOfWork, IGenericRepository<Compra> compraRepository)
		{
			_compraRepository = compraRepository;
			_unitOfWork = unitOfWork;
		}

		public CompraValidationContext GenerarModelo(Compra compra, OperacionCompra operacion)
		{
			var context = new CompraValidationContext();
			context.operacion = operacion;
			switch (operacion)
			{
				case OperacionCompra.Registrar:
				break;
				case OperacionCompra.Modificar:
					context.compraEstadoActual = _compraRepository.Get(x => x.Id == compra.Id, asNoTracking: true).FirstOrDefault();
				break;
				case OperacionCompra.Eliminar:
					context.compraEstadoActual = _compraRepository.Get(x => x.Id == compra.Id, asNoTracking: true).FirstOrDefault();
				break;
			}
			return context;
		}
	}
}

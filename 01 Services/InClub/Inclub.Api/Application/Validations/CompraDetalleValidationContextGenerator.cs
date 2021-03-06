using Inclub.Domain.AggregatesModel;
using Inclub.Domain.AggregatesModel.CompraAggregate;
using Inclub.Domain.Validations;
using Inclub.Infrastructure;
using System.Linq;

namespace Inclub.Api.Application.Validations
{
    public class CompraDetalleValidationContextGenerator
	{
		private IGenericRepository<CompraDetalle> _compradetalleRepository;
		private UnitOfWork _unitOfWork;

		public CompraDetalleValidationContextGenerator(UnitOfWork unitOfWork, IGenericRepository<CompraDetalle> compradetalleRepository)
		{
			_compradetalleRepository = compradetalleRepository;
			_unitOfWork = unitOfWork;
		}

		public CompraDetalleValidationContext GenerarModelo(CompraDetalle compradetalle, OperacionCompraDetalle operacion)
		{
			var context = new CompraDetalleValidationContext();
			context.operacion = operacion;
			switch (operacion)
			{
				case OperacionCompraDetalle.Registrar:
				break;
				case OperacionCompraDetalle.Modificar:
					context.compraDetalleEstadoActual = _compradetalleRepository.Get(x => x.Id == compradetalle.Id, asNoTracking: true).FirstOrDefault();
				break;
				case OperacionCompraDetalle.Eliminar:
					context.compraDetalleEstadoActual = _compradetalleRepository.Get(x => x.Id == compradetalle.Id, asNoTracking: true).FirstOrDefault();
				break;
			}
			return context;
		}
	}
}

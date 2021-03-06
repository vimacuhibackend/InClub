using Inclub.Core;
using Inclub.Domain.AggregatesModel.CompraAggregate;

namespace Inclub.Domain.Validations
{
    public class CompraDetalleValidator : BaseValidator, IValidator<CompraDetalle>
	{
		private ValidationResult _result;
		private CompraDetalleValidationContext _context;

		public CompraDetalleValidator(CompraDetalleValidationContext context)
		{
			_context = context;
			_result = new ValidationResult();
		}

		public ValidationResult Validar(CompraDetalle registro)
		{
			_result = new ValidationResult();
			if (_context.operacion == OperacionCompraDetalle.Registrar)
			{
			}
			else if (_context.operacion == OperacionCompraDetalle.Modificar)
			{
			}
			else if(_context.operacion == OperacionCompraDetalle.Eliminar)
			{
			}
			else if(_context.operacion == OperacionCompraDetalle.CargaMasiva)
			{
			}
			return _result;
		}
	}

	public enum OperacionCompraDetalle
	{
		CargaMasiva,
		Registrar,
		Modificar,
		Eliminar
	}

	public class CompraDetalleValidationContext : BaseValidationContext
		{
		public CompraDetalle compraDetalleEstadoActual;
		public OperacionCompraDetalle operacion;
	}
}

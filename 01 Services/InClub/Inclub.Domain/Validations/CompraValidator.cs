using Inclub.Core;
using Inclub.Domain.AggregatesModel.CompraAggregate;

namespace Inclub.Domain.Validations
{
    public class CompraValidator : BaseValidator, IValidator<Compra>
	{
		private ValidationResult _result;
		private CompraValidationContext _context;

		public CompraValidator(CompraValidationContext context)
		{
			_context = context;
			_result = new ValidationResult();
		}

		public ValidationResult Validar(Compra registro)
		{
			_result = new ValidationResult();
			if (_context.operacion == OperacionCompra.Registrar)
			{
			}
			else if (_context.operacion == OperacionCompra.Modificar)
			{
			}
			else if(_context.operacion == OperacionCompra.Eliminar)
			{
			}
			else if(_context.operacion == OperacionCompra.CargaMasiva)
			{
			}
			return _result;
		}
	}

	public enum OperacionCompra
	{
		CargaMasiva,
		Registrar,
		Modificar,
		Eliminar
	}

	public class CompraValidationContext : BaseValidationContext
		{
		public Compra compraEstadoActual;
		public OperacionCompra operacion;
	}
}

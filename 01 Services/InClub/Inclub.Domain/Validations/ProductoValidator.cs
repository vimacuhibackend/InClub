using Inclub.Core;
using Inclub.Domain.AggregatesModel.ProductoAggregate;

namespace Inclub.Domain.Validations
{
    public class ProductoValidator : BaseValidator, IValidator<Producto>
	{
		private ValidationResult _result;
		private ProductoValidationContext _context;

		public ProductoValidator(ProductoValidationContext context)
		{
			_context = context;
			_result = new ValidationResult();
		}

		public ValidationResult Validar(Producto registro)
		{
			_result = new ValidationResult();
			if (_context.operacion == OperacionProducto.Registrar)
			{
			}
			else if (_context.operacion == OperacionProducto.Modificar)
			{
			}
			else if(_context.operacion == OperacionProducto.Eliminar)
			{
			}
			else if(_context.operacion == OperacionProducto.CargaMasiva)
			{
			}
			return _result;
		}
	}

	public enum OperacionProducto
	{
		CargaMasiva,
		Registrar,
		Modificar,
		Eliminar
	}

	public class ProductoValidationContext : BaseValidationContext
		{
		public Producto productoEstadoActual;
		public OperacionProducto operacion;
	}
}

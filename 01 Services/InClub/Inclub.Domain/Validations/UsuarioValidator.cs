using Inclub.Core;
using Inclub.Domain.AggregatesModel.UsuarioAggregate;

namespace Inclub.Domain.Validations
{
    public class UsuarioValidator : BaseValidator, IValidator<Usuario>
	{
		private ValidationResult _result;
		private UsuarioValidationContext _context;

		public UsuarioValidator(UsuarioValidationContext context)
		{
			_context = context;
			_result = new ValidationResult();
		}

		public ValidationResult Validar(Usuario registro)
		{
			_result = new ValidationResult();
			if (_context.operacion == OperacionUsuario.Registrar)
			{
			}
			else if (_context.operacion == OperacionUsuario.Modificar)
			{
			}
			else if(_context.operacion == OperacionUsuario.Eliminar)
			{
			}
			else if(_context.operacion == OperacionUsuario.CargaMasiva)
			{
			}
			return _result;
		}
	}

	public enum OperacionUsuario
	{
		CargaMasiva,
		Registrar,
		Modificar,
		Eliminar
	}

	public class UsuarioValidationContext : BaseValidationContext
		{
		public Usuario usuarioEstadoActual;
		public OperacionUsuario operacion;
	}
}

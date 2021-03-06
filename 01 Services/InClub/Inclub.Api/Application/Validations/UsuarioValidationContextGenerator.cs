using Inclub.Domain.AggregatesModel;
using Inclub.Domain.AggregatesModel.UsuarioAggregate;
using Inclub.Domain.Validations;
using Inclub.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Inclub.Api.Application.Validations
{
	public class UsuarioValidationContextGenerator
	{
		private IGenericRepository<Usuario> _usuarioRepository;
		private UnitOfWork _unitOfWork;

		public UsuarioValidationContextGenerator(UnitOfWork unitOfWork, IGenericRepository<Usuario> usuarioRepository)
		{
			_usuarioRepository = usuarioRepository;
			_unitOfWork = unitOfWork;
		}

		public UsuarioValidationContext GenerarModelo(Usuario usuario, OperacionUsuario operacion)
		{
			var context = new UsuarioValidationContext();
			context.operacion = operacion;
			switch (operacion)
			{
				case OperacionUsuario.Registrar:
				break;
				case OperacionUsuario.Modificar:
					context.usuarioEstadoActual = _usuarioRepository.Get(x => x.Id == usuario.Id, asNoTracking: true).FirstOrDefault();
				break;
				case OperacionUsuario.Eliminar:
					context.usuarioEstadoActual = _usuarioRepository.Get(x => x.Id == usuario.Id, asNoTracking: true).FirstOrDefault();
				break;
			}
			return context;
		}
	}
}

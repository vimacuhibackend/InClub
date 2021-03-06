using Inclub.Api.Application.Validations;
using Inclub.Core;
using Inclub.Domain.AggregatesModel.ProductoAggregate;
using Inclub.Domain.Validations;
using Inclub.Infrastructure;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Inclub.Api.Application.Commands
{

    public class EliminarProductoCommandHandler : IRequestHandler<EliminarProductoCommand, GenericResult>
	{
		private readonly IMediator _mediator;
		private readonly GenericRepository<Producto> _repository;
		private readonly UnitOfWork _unitOfWork;

		public EliminarProductoCommandHandler(IMediator mediator, UnitOfWork unitOfWork)
		{
			_mediator = mediator;
			_unitOfWork = unitOfWork;
			_repository = new GenericRepository<Producto>(unitOfWork);
		}

		public async Task<GenericResult> Handle(EliminarProductoCommand request, CancellationToken cancellationToken)
		{
			GenericResult response = new GenericResult();
			Producto producto = _repository .Get(x => x.GuidValue == request.guidProducto && !x.EsEliminado).FirstOrDefault();

			if (producto == null)
				response.Messages.Add(new GenericMessage(MessageType.Error, "El producto indicado no existe."));

			if (response.HasErrors) { return response; }

			producto.SetProductoDelete(
					request.UsuarioRegistro,
					request.IpRegistro);

				response = await Validar(producto);
				if (response.HasErrors) { return response; }
				_repository.Update(producto);
				await _unitOfWork.SaveAsync();
				return response;
		}
		private async Task<GenericResult> Validar(Producto producto)
		{

		GenericResult result = new GenericResult();
		ValidationResult validationResult = new ValidationResult();

			//1) validar atributos
			validationResult = producto.ValidarAtributos();
			if (validationResult.EsValido == false)
			{
				validationResult.Observaciones.ForEach(x => result.AddError(x));
				return result;
			}

			//2) Invocar validaciones de negocio
			#region Inicializando objetos de validacion
			ProductoValidationContextGenerator contextGenerator = new ProductoValidationContextGenerator(_unitOfWork, _repository);
			ProductoValidationContext validationContext = contextGenerator.GenerarModelo(producto, OperacionProducto.Eliminar);
			ProductoValidator validator = new ProductoValidator(validationContext);
			#endregion

			validationResult = validator.Validar(producto);
			if (validationResult.EsValido == false)
			{
				validationResult.Observaciones.ForEach(x => result.AddError(x));
				return result;
			}
			return result;
		}
	}
}

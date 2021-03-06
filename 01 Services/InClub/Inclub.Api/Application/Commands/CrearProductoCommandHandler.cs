using Inclub.Api.Application.Validations;
using Inclub.Core;
using Inclub.Domain.AggregatesModel;
using Inclub.Domain.AggregatesModel.ProductoAggregate;
using Inclub.Domain.Validations;
using Inclub.Infrastructure;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Inclub.Api.Application.Commands
{

    public class CrearProductoCommandHandler : IRequestHandler<CrearProductoCommand, GenericResult>
	{
		private readonly IMediator _mediator;
		private readonly IGenericRepository<Producto> _repository;
		private readonly UnitOfWork _unitOfWork;

		public CrearProductoCommandHandler(IMediator mediator, UnitOfWork unitOfWork)
		{
			_mediator = mediator;
			_unitOfWork = unitOfWork;
			_repository = new GenericRepository<Producto>(unitOfWork);
		}

		public async Task<GenericResult> Handle(CrearProductoCommand request, CancellationToken cancellationToken)
		{
			GenericResult response = new GenericResult();
				Producto producto = new Producto(
					request.codigoProducto,
					request.nombreProducto,
					request.stockInicial,
					request.stockActual,
					request.precioVenta,
					request.UsuarioRegistro,
					request.IpRegistro);

				response = await Validar(producto);
				if (response.HasErrors) { return response; }
				_repository.Add(producto);
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
			ProductoValidationContext validationContext = contextGenerator.GenerarModelo(producto, OperacionProducto.Registrar);
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

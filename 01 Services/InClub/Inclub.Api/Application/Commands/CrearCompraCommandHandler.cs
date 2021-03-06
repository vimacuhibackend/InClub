using Inclub.Api.Application.Validations;
using Inclub.Core;
using Inclub.Domain.AggregatesModel;
using Inclub.Domain.AggregatesModel.CompraAggregate;
using Inclub.Domain.Validations;
using Inclub.Infrastructure;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Inclub.Api.Application.Commands
{

    public class CrearCompraCommandHandler : IRequestHandler<CrearCompraCommand, GenericResult>
	{
		private readonly IMediator _mediator;
		private readonly IGenericRepository<Compra> _repository;
		private readonly UnitOfWork _unitOfWork;

		public CrearCompraCommandHandler(IMediator mediator, UnitOfWork unitOfWork)
		{
			_mediator = mediator;
			_unitOfWork = unitOfWork;
			_repository = new GenericRepository<Compra>(unitOfWork);
		}

		public async Task<GenericResult> Handle(CrearCompraCommand request, CancellationToken cancellationToken)
		{
			GenericResult response = new GenericResult();
				Compra compra = new Compra(
					request.idUsuario,
					request.subtotal,
					request.total,
					request.UsuarioRegistro,
					request.IpRegistro);

				response = await Validar(compra);
				if (response.HasErrors) { return response; }
				_repository.Add(compra);
				await _unitOfWork.SaveAsync();
				return response;
		}
		private async Task<GenericResult> Validar(Compra compra)
		{

		GenericResult result = new GenericResult();
		ValidationResult validationResult = new ValidationResult();

			//1) validar atributos
			validationResult = compra.ValidarAtributos();
			if (validationResult.EsValido == false)
			{
				validationResult.Observaciones.ForEach(x => result.AddError(x));
				return result;
			}

			//2) Invocar validaciones de negocio
			#region Inicializando objetos de validacion
			CompraValidationContextGenerator contextGenerator = new CompraValidationContextGenerator(_unitOfWork, _repository);
			CompraValidationContext validationContext = contextGenerator.GenerarModelo(compra, OperacionCompra.Registrar);
			CompraValidator validator = new CompraValidator(validationContext);
			#endregion

			validationResult = validator.Validar(compra);
			if (validationResult.EsValido == false)
			{
				validationResult.Observaciones.ForEach(x => result.AddError(x));
				return result;
			}
			return result;
		}
	}
}

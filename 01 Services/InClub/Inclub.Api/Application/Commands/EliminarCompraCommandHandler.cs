using Inclub.Api.Application.Validations;
using Inclub.Core;
using Inclub.Domain.AggregatesModel.CompraAggregate;
using Inclub.Domain.Validations;
using Inclub.Infrastructure;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Inclub.Api.Application.Commands
{

    public class EliminarCompraCommandHandler : IRequestHandler<EliminarCompraCommand, GenericResult>
	{
		private readonly IMediator _mediator;
		private readonly GenericRepository<Compra> _repository;
		private readonly UnitOfWork _unitOfWork;

		public EliminarCompraCommandHandler(IMediator mediator, UnitOfWork unitOfWork)
		{
			_mediator = mediator;
			_unitOfWork = unitOfWork;
			_repository = new GenericRepository<Compra>(unitOfWork);
		}

		public async Task<GenericResult> Handle(EliminarCompraCommand request, CancellationToken cancellationToken)
		{
			GenericResult response = new GenericResult();
			Compra compra = _repository .Get(x => x.GuidValue == request.guidCompra && !x.EsEliminado).FirstOrDefault();

			if (compra == null)
				response.Messages.Add(new GenericMessage(MessageType.Error, "La compra indicada no existe."));

			if (response.HasErrors) { return response; }

			compra.SetCompraDelete(
					request.UsuarioRegistro,
					request.IpRegistro);

				response = await Validar(compra);
				if (response.HasErrors) { return response; }
				_repository.Update(compra);
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
			CompraValidationContext validationContext = contextGenerator.GenerarModelo(compra, OperacionCompra.Eliminar);
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

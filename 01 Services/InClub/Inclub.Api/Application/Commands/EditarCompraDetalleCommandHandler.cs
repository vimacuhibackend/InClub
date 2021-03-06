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

    public class EditarCompraDetalleCommandHandler : IRequestHandler<EditarCompraDetalleCommand, GenericResult>
    {
        private readonly IMediator _mediator;
        private readonly GenericRepository<CompraDetalle> _repository;
        private readonly UnitOfWork _unitOfWork;

        public EditarCompraDetalleCommandHandler(IMediator mediator, UnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _repository = new GenericRepository<CompraDetalle>(unitOfWork);
        }

        public async Task<GenericResult> Handle(EditarCompraDetalleCommand request, CancellationToken cancellationToken)
        {
            GenericResult response = new GenericResult();

            CompraDetalle compradetalle = _repository.Get(x => x.GuidValue == request.guidCompraDetalle && !x.EsEliminado).FirstOrDefault();

            if (compradetalle == null)
                response.Messages.Add(new GenericMessage(MessageType.Error, "El detalle de la compra indicado no existe."));

            if (response.HasErrors) { return response; }

            compradetalle.SetCompraDetalleUpdate(
                request.idCompra,
                request.cantidad,
                request.idProducto,
                request.precioUnitario,
                request.precioTotal,
                request.UsuarioRegistro,
                request.IpRegistro);

            response = await Validar(compradetalle);
            if (response.HasErrors) { return response; }
            _repository.Update(compradetalle);
            await _unitOfWork.SaveAsync();
            return response;
        }
        private async Task<GenericResult> Validar(CompraDetalle compradetalle)
        {

            GenericResult result = new GenericResult();
            ValidationResult validationResult = new ValidationResult();

            //1) validar atributos
            validationResult = compradetalle.ValidarAtributos();
            if (validationResult.EsValido == false)
            {
                validationResult.Observaciones.ForEach(x => result.AddError(x));
                return result;
            }

            //2) Invocar validaciones de negocio
            #region Inicializando objetos de validacion
            CompraDetalleValidationContextGenerator contextGenerator = new CompraDetalleValidationContextGenerator(_unitOfWork, _repository);
            CompraDetalleValidationContext validationContext = contextGenerator.GenerarModelo(compradetalle, OperacionCompraDetalle.Modificar);
            CompraDetalleValidator validator = new CompraDetalleValidator(validationContext);
            #endregion

            validationResult = validator.Validar(compradetalle);
            if (validationResult.EsValido == false)
            {
                validationResult.Observaciones.ForEach(x => result.AddError(x));
                return result;
            }
            return result;
        }
    }
}

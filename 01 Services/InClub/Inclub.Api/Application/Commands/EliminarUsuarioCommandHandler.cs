using Inclub.Api.Application.Validations;
using Inclub.Core;
using Inclub.Domain.AggregatesModel.UsuarioAggregate;
using Inclub.Domain.Validations;
using Inclub.Infrastructure;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Inclub.Api.Application.Commands
{

    public class EliminarUsuarioCommandHandler : IRequestHandler<EliminarUsuarioCommand, GenericResult>
    {
        private readonly IMediator _mediator;
        private readonly GenericRepository<Usuario> _repository;
        private readonly UnitOfWork _unitOfWork;

        public EliminarUsuarioCommandHandler(IMediator mediator, UnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _repository = new GenericRepository<Usuario>(unitOfWork);
        }

        public async Task<GenericResult> Handle(EliminarUsuarioCommand request, CancellationToken cancellationToken)
        {
            GenericResult response = new GenericResult();
            Usuario usuario = _repository.Get(x => x.GuidValue == request.guidUsuario && !x.EsEliminado).FirstOrDefault();

            if (usuario == null)
                response.Messages.Add(new GenericMessage(MessageType.Error, "El usuario indicado no existe."));

            if (response.HasErrors) { return response; }

            usuario.SetUsuarioDelete(
                    request.UsuarioRegistro,
                    request.IpRegistro);

            response = await Validar(usuario);
            if (response.HasErrors) { return response; }
            _repository.Update(usuario);
            await _unitOfWork.SaveAsync();
            return response;
        }
        private async Task<GenericResult> Validar(Usuario usuario)
        {

            GenericResult result = new GenericResult();
            ValidationResult validationResult = new ValidationResult();

            //1) validar atributos
            validationResult = usuario.ValidarAtributos();
            if (validationResult.EsValido == false)
            {
                validationResult.Observaciones.ForEach(x => result.AddError(x));
                return result;
            }

            //2) Invocar validaciones de negocio
            #region Inicializando objetos de validacion
            UsuarioValidationContextGenerator contextGenerator = new UsuarioValidationContextGenerator(_unitOfWork, _repository);
            UsuarioValidationContext validationContext = contextGenerator.GenerarModelo(usuario, OperacionUsuario.Eliminar);
            UsuarioValidator validator = new UsuarioValidator(validationContext);
            #endregion

            validationResult = validator.Validar(usuario);
            if (validationResult.EsValido == false)
            {
                validationResult.Observaciones.ForEach(x => result.AddError(x));
                return result;
            }
            return result;
        }
    }
}

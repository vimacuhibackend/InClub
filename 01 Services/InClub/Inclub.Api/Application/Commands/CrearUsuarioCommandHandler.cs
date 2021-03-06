using Inclub.Api.Application.Validations;
using Inclub.Core;
using Inclub.Domain.AggregatesModel;
using Inclub.Domain.AggregatesModel.UsuarioAggregate;
using Inclub.Domain.Validations;
using Inclub.Infrastructure;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Inclub.Api.Application.Commands
{

    public class CrearUsuarioCommandHandler : IRequestHandler<CrearUsuarioCommand, GenericResult>
    {
        private readonly IMediator _mediator;
        private readonly IGenericRepository<Usuario> _repository;
        private readonly UnitOfWork _unitOfWork;

        public CrearUsuarioCommandHandler(IMediator mediator, UnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _repository = new GenericRepository<Usuario>(unitOfWork);
        }

        public async Task<GenericResult> Handle(CrearUsuarioCommand request, CancellationToken cancellationToken)
        {
            GenericResult response = new GenericResult();
            Usuario usuario = new Usuario(
                request.uidUsuario,
                request.passwordHash,
                request.nombreUsuario,
                request.UsuarioRegistro,
                request.IpRegistro);

            response = await Validar(usuario);
            if (response.HasErrors) { return response; }
            _repository.Add(usuario);
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
            UsuarioValidationContext validationContext = contextGenerator.GenerarModelo(usuario, OperacionUsuario.Registrar);
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

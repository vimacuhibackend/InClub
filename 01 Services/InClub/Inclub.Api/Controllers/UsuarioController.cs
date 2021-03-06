using Inclub.Api.Application.Commands;
using Inclub.Api.Application.Queries;
using Inclub.Api.Application.ViewModels;
using Inclub.Api.Application.ViewModels.UsuarioViewModel;
using Inclub.Api.Infrastructure.Services;
using Inclub.Core;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Inclub.Api.Controllers
{
    [Route("api/v1/usuario")]
	[ApiController]
	public class UsuarioController : BaseController
	{
		private readonly IUsuarioQueries _usuarioQueries;
		private readonly IIdentityService _identityService;
		private readonly IMediator _mediator;
		private readonly IHostingEnvironment _hostingEnvironment;
		private const String XlsxContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
		public readonly IConfiguration _configuration;

		public UsuarioController(IMediator mediator, IUsuarioQueries usuarioQueries, IIdentityService identityService, IHostingEnvironment hostingEnvironment, IConfiguration configuration)
		{
			_usuarioQueries = usuarioQueries ?? throw new ArgumentNullException(nameof(usuarioQueries));
			_identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
			_hostingEnvironment = hostingEnvironment;
			_configuration = configuration;
		}

		[HttpGet]
		[Route("")]
		[ProducesResponseType(typeof(List<UsuarioItemsDto>), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> ListarUsuario([FromQuery] PaginatedItemsRequestViewModel<UsuarioBusquedaRequest> request)
		{
			var oLista = await _usuarioQueries.ListarUsuarioAsync(request);
			return Ok(oLista);
		}

		[HttpPost]
		[Route("")]
		[ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> Registrar([FromBody] CrearUsuarioCommand command)
		{
			return await Validar(command);}

		[HttpPut]
		[Route("")]
		public async Task<IActionResult> Editar([FromBody] EditarUsuarioCommand command)
		{
			return await Validar(command);
		}

		[HttpDelete]
		[Route("{guid}")]
		public async Task<IActionResult> Eliminar([FromRoute] Guid guid)
		{
			EliminarUsuarioCommand command = new EliminarUsuarioCommand(guid);
			return await Validar(command);
		}
		private async Task<IActionResult> Validar(IValidable command)
		{
			GenericResult response = new GenericResult();
			ValidationResult result = command.ValidarAtributos();
			if (!result.EsValido)
			{
				result.Observaciones.ForEach(o =>
				{
					response.AddWarning(o);
				});
				return BadRequest(response);
			}
			response = await _mediator.Send((IRequest<GenericResult>)command, new System.Threading.CancellationToken());
			return response.HasErrors ? (IActionResult)BadRequest(response) : Ok();
		}
	}
}

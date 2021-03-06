using Inclub.Api.Application.Commands;
using Inclub.Api.Application.Queries;
using Inclub.Api.Application.ViewModels;
using Inclub.Api.Application.ViewModels.CompraDetalleViewModel;
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
    [Route("api/v1/compradetalle")]
	[ApiController]
	public class CompraDetalleController : BaseController
	{
		private readonly ICompraDetalleQueries _compradetalleQueries;
		private readonly IIdentityService _identityService;
		private readonly IMediator _mediator;
		private readonly IHostingEnvironment _hostingEnvironment;
		private const String XlsxContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
		public readonly IConfiguration _configuration;

		public CompraDetalleController(IMediator mediator, ICompraDetalleQueries compradetalleQueries, IIdentityService identityService, IHostingEnvironment hostingEnvironment, IConfiguration configuration)
		{
			_compradetalleQueries = compradetalleQueries ?? throw new ArgumentNullException(nameof(compradetalleQueries));
			_identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
			_hostingEnvironment = hostingEnvironment;
			_configuration = configuration;
		}

		[HttpGet]
		[Route("")]
		[ProducesResponseType(typeof(List<CompraDetalleItemsDto>), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> ListarCompraDetalle([FromQuery] PaginatedItemsRequestViewModel<CompraDetalleBusquedaRequest> request)
		{
			var oLista = await _compradetalleQueries.ListarCompraDetalleAsync(request);
			return Ok(oLista);
		}

		[HttpPost]
		[Route("")]
		[ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> Registrar([FromBody] CrearCompraDetalleCommand command)
		{
			return await Validar(command);}

		[HttpPut]
		[Route("")]
		public async Task<IActionResult> Editar([FromBody] EditarCompraDetalleCommand command)
		{
			return await Validar(command);
		}

		[HttpDelete]
		[Route("{guid}")]
		public async Task<IActionResult> Eliminar([FromRoute] Guid guid)
		{
			EliminarCompraDetalleCommand command = new EliminarCompraDetalleCommand(guid);
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

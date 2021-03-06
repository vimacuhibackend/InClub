using Inclub.Api.Application.Commands;
using Inclub.Api.Application.Queries;
using Inclub.Api.Application.ViewModels;
using Inclub.Api.Application.ViewModels.CompraViewModel;
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
    [Route("api/v1/compra")]
	[ApiController]
	public class CompraController : BaseController
	{
		private readonly ICompraQueries _compraQueries;
		private readonly IIdentityService _identityService;
		private readonly IMediator _mediator;
		private readonly IHostingEnvironment _hostingEnvironment;
		private const String XlsxContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
		public readonly IConfiguration _configuration;

		public CompraController(IMediator mediator, ICompraQueries compraQueries, IIdentityService identityService, IHostingEnvironment hostingEnvironment, IConfiguration configuration)
		{
			_compraQueries = compraQueries ?? throw new ArgumentNullException(nameof(compraQueries));
			_identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
			_hostingEnvironment = hostingEnvironment;
			_configuration = configuration;
		}

		[HttpGet]
		[Route("")]
		[ProducesResponseType(typeof(List<CompraItemsDto>), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> ListarCompra([FromQuery] PaginatedItemsRequestViewModel<CompraBusquedaRequest> request)
		{
			var oLista = await _compraQueries.ListarCompraAsync(request);
			return Ok(oLista);
		}

		[HttpPost]
		[Route("")]
		[ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> Registrar([FromBody] CrearCompraCommand command)
		{
			return await Validar(command);}

		[HttpPut]
		[Route("")]
		public async Task<IActionResult> Editar([FromBody] EditarCompraCommand command)
		{
			return await Validar(command);
		}

		[HttpDelete]
		[Route("{guid}")]
		public async Task<IActionResult> Eliminar([FromRoute] Guid guid)
		{
			EliminarCompraCommand command = new EliminarCompraCommand(guid);
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

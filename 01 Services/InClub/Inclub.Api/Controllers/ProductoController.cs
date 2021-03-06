using Inclub.Api.Application.Commands;
using Inclub.Api.Application.Queries;
using Inclub.Api.Application.ViewModels;
using Inclub.Api.Application.ViewModels.ProductoViewModel;
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
    [Route("api/v1/producto")]
	[ApiController]
	public class ProductoController : BaseController
	{
		private readonly IProductoQueries _productoQueries;
		private readonly IIdentityService _identityService;
		private readonly IMediator _mediator;
		private readonly IHostingEnvironment _hostingEnvironment;
		private const String XlsxContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
		public readonly IConfiguration _configuration;

		public ProductoController(IMediator mediator, IProductoQueries productoQueries, IIdentityService identityService, IHostingEnvironment hostingEnvironment, IConfiguration configuration)
		{
			_productoQueries = productoQueries ?? throw new ArgumentNullException(nameof(productoQueries));
			_identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
			_hostingEnvironment = hostingEnvironment;
			_configuration = configuration;
		}

		[HttpGet]
		[Route("")]
		[ProducesResponseType(typeof(List<ProductoItemsDto>), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> ListarProducto([FromQuery] PaginatedItemsRequestViewModel<ProductoBusquedaRequest> request)
		{
			var oLista = await _productoQueries.ListarProductoAsync(request);
			return Ok(oLista);
		}

		[HttpPost]
		[Route("")]
		[ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> Registrar([FromBody] CrearProductoCommand command)
		{
			return await Validar(command);}

		[HttpPut]
		[Route("")]
		public async Task<IActionResult> Editar([FromBody] EditarProductoCommand command)
		{
			return await Validar(command);
		}

		[HttpDelete]
		[Route("{guid}")]
		public async Task<IActionResult> Eliminar([FromRoute] Guid guid)
		{
			EliminarProductoCommand command = new EliminarProductoCommand(guid);
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

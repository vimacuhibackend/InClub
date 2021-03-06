using Inclub.Core;
using MediatR;
using System;
using System.Runtime.Serialization;

namespace Inclub.Api.Application.Commands
{
    [DataContract]
	public class EliminarCompraDetalleCommand :  AuditoriaCommand, IRequest<GenericResult>, IValidable
	{
		[DataMember] 
		[ValorRequerido]
		public Guid guidCompraDetalle { get; private set; }

		public EliminarCompraDetalleCommand(
			Guid idCompraDetalle
		)
		{
			this.guidCompraDetalle = idCompraDetalle;
		}
	}
}

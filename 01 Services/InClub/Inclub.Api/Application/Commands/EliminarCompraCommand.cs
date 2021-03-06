using Inclub.Core;
using MediatR;
using System;
using System.Runtime.Serialization;

namespace Inclub.Api.Application.Commands
{
    [DataContract]
	public class EliminarCompraCommand :  AuditoriaCommand, IRequest<GenericResult>, IValidable
	{
		[DataMember] 
		[ValorRequerido]
		public Guid guidCompra { get; private set; }

		public EliminarCompraCommand(
			Guid idCompra
		)
		{
			this.guidCompra = idCompra;
		}
	}
}

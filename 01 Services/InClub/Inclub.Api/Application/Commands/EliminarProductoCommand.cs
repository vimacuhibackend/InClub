using Inclub.Core;
using MediatR;
using System;
using System.Runtime.Serialization;

namespace Inclub.Api.Application.Commands
{
    [DataContract]
	public class EliminarProductoCommand :  AuditoriaCommand, IRequest<GenericResult>, IValidable
	{
		[DataMember] 
		[ValorRequerido]
		public Guid guidProducto { get; private set; }

		public EliminarProductoCommand(
			Guid idProducto
		)
		{
			this.guidProducto = idProducto;
		}
	}
}

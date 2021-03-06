using Inclub.Core;
using MediatR;
using System;
using System.Runtime.Serialization;

namespace Inclub.Api.Application.Commands
{
    [DataContract]
	public class EditarCompraCommand :  AuditoriaCommand, IRequest<GenericResult>, IValidable
	{
		[DataMember] 
		[ValorRequerido]
		public Guid guidCompra { get; private set; }
		[DataMember] 
		[ValorRequerido]
		public int idUsuario { get; private set; }
		[DataMember] 
		[ValorRequerido]
		public decimal subtotal { get; private set; }
		[DataMember] 
		[ValorRequerido]
		public decimal total { get; private set; }
	}
}

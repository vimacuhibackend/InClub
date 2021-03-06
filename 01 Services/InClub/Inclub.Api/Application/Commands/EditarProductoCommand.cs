using Inclub.Core;
using MediatR;
using System;
using System.Runtime.Serialization;

namespace Inclub.Api.Application.Commands
{
    [DataContract]
	public class EditarProductoCommand :  AuditoriaCommand, IRequest<GenericResult>, IValidable
	{
		[DataMember] 
		[ValorRequerido]
		public Guid guidProducto { get; private set; }
		[DataMember] 
		[ValorRequerido]
		public string codigoProducto { get; private set; }
		[DataMember] 
		[ValorRequerido]
		public string nombreProducto { get; private set; }
		[DataMember] 
		[ValorRequerido]
		public int stockInicial { get; private set; }
		[DataMember] 
		[ValorRequerido]
		public int stockActual { get; private set; }
		[DataMember] 
		[ValorRequerido]
		public decimal precioVenta { get; private set; }
	}
}

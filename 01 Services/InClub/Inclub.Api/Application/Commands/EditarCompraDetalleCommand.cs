using Inclub.Core;
using MediatR;
using System;
using System.Runtime.Serialization;

namespace Inclub.Api.Application.Commands
{
    [DataContract]
	public class EditarCompraDetalleCommand :  AuditoriaCommand, IRequest<GenericResult>, IValidable
	{
		[DataMember] 
		[ValorRequerido]
		public Guid guidCompraDetalle { get; private set; }
		[DataMember] 
		[ValorRequerido]
		public int idCompra { get; private set; }
		[DataMember] 
		[ValorRequerido]
		public int cantidad { get; private set; }
		[DataMember] 
		[ValorRequerido]
		public int idProducto { get; private set; }
		[DataMember] 
		[ValorRequerido]
		public decimal precioUnitario { get; private set; }
		[DataMember] 
		[ValorRequerido]
		public decimal precioTotal { get; private set; }
	}
}

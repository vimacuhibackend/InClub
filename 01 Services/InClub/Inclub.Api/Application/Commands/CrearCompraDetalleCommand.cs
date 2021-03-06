using Inclub.Core;
using MediatR;
using System.Runtime.Serialization;

namespace Inclub.Api.Application.Commands
{
    [DataContract]
	public class CrearCompraDetalleCommand :  AuditoriaCommand, IRequest<GenericResult>, IValidable
	{

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

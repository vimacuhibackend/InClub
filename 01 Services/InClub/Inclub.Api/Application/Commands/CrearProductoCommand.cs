using Inclub.Core;
using MediatR;
using System.Runtime.Serialization;

namespace Inclub.Api.Application.Commands
{
    [DataContract]
	public class CrearProductoCommand :  AuditoriaCommand, IRequest<GenericResult>, IValidable
	{

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

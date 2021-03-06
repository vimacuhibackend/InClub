using Inclub.Core;
using MediatR;
using System.Runtime.Serialization;

namespace Inclub.Api.Application.Commands
{
    [DataContract]
	public class CrearCompraCommand :  AuditoriaCommand, IRequest<GenericResult>, IValidable
	{

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

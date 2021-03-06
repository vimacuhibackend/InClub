using Inclub.Core;
using MediatR;
using System.Runtime.Serialization;

namespace Inclub.Api.Application.Commands
{
    [DataContract]
	public class CrearUsuarioCommand :  AuditoriaCommand, IRequest<GenericResult>, IValidable
	{

		[DataMember] 
		[ValorRequerido]
		public string uidUsuario { get; private set; }
		[DataMember] 
		[ValorRequerido]
		public string passwordHash { get; private set; }
		[DataMember] 
		public string nombreUsuario { get; private set; }
	}
}

using Inclub.Core;
using MediatR;
using System;
using System.Runtime.Serialization;

namespace Inclub.Api.Application.Commands
{
    [DataContract]
	public class EditarUsuarioCommand :  AuditoriaCommand, IRequest<GenericResult>, IValidable
	{
		[DataMember] 
		[ValorRequerido]
		public Guid guidUsuario { get; private set; }
		[DataMember] 
		[ValorRequerido]
		public string passwordHash { get; private set; }
		[DataMember] 
		public string nombreUsuario { get; private set; }
	}
}

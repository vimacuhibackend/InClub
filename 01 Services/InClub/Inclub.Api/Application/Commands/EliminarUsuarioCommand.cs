using Inclub.Core;
using MediatR;
using System;
using System.Runtime.Serialization;

namespace Inclub.Api.Application.Commands
{
    [DataContract]
	public class EliminarUsuarioCommand :  AuditoriaCommand, IRequest<GenericResult>, IValidable
	{
		[DataMember] 
		[ValorRequerido]
		public Guid guidUsuario { get; private set; }

		public EliminarUsuarioCommand(
			Guid idUsuario
		)
		{
			this.guidUsuario = idUsuario;
		}
	}
}

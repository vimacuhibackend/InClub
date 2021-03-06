using Inclub.Core;
using Inclub.Domain.SeedWork;
using System;

namespace Inclub.Domain.AggregatesModel.UsuarioAggregate
{
    public class Usuario : Entity, IAggregateRoot, IValidable
	{

		public string UidUsuario { get; private set; }
		public string PasswordHash { get; private set; }
		public string NombreUsuario { get; private set; }
		public bool EsEliminado { get; private set; }

		protected Usuario() { }

		public Usuario(
			string uidUsuario,
			string passwordHash,
			string nombreUsuario,
			Guid usuarioCreacion,
			String ipCreacion) : this()
		{
			this.UidUsuario = uidUsuario;
			this.PasswordHash = passwordHash;
			this.NombreUsuario = nombreUsuario;
			this.EsEliminado = false;
			this.UsuarioCreacion = usuarioCreacion;
			this.FechaCreacion = DateTime.Now;
			this.IpCreacion = ipCreacion;
		}

		public Usuario SetUsuarioUpdate(
			string passwordHash,
			string nombreUsuario,
			Guid? usuarioModificacion,
			String ipModificacion)
		{
			this.PasswordHash = passwordHash;
			this.NombreUsuario = nombreUsuario;

			this.UsuarioModificacion = usuarioModificacion;
			this.FechaModificacion = DateTime.Now;
			this.IpModificacion = ipModificacion;
			return this;
		}

		public Usuario SetUsuarioDelete(
			Guid? usuarioModificacion,
			String ipModificacion)
		{
			EsEliminado = true;

			this.UsuarioModificacion = usuarioModificacion;
			this.FechaModificacion = DateTime.Now;
			this.IpModificacion = ipModificacion;

			return this;
		}
	}
}

using Inclub.Core;
using Inclub.Domain.SeedWork;
using System;

namespace Inclub.Domain.AggregatesModel.CompraAggregate
{
    public class Compra : Entity, IAggregateRoot, IValidable
	{

		public int IdUsuario { get; private set; }
		public decimal Subtotal { get; private set; }
		public decimal Total { get; private set; }
		public bool EsEliminado { get; private set; }

		protected Compra() { }

		public Compra(
			int idUsuario,
			decimal subtotal,
			decimal total,
			Guid usuarioCreacion,
			String ipCreacion) : this()
		{
			this.IdUsuario = idUsuario;
			this.Subtotal = subtotal;
			this.Total = total;
			this.EsEliminado = false;
			this.UsuarioCreacion = usuarioCreacion;
			this.FechaCreacion = DateTime.Now;
			this.IpCreacion = ipCreacion;
		}

		public Compra SetCompraUpdate(
			int idUsuario,
			decimal subtotal,
			decimal total,
			Guid? usuarioModificacion,
			String ipModificacion)
		{
			this.IdUsuario = idUsuario;
			this.Subtotal = subtotal;
			this.Total = total;

			this.UsuarioModificacion = usuarioModificacion;
			this.FechaModificacion = DateTime.Now;
			this.IpModificacion = ipModificacion;
			return this;
		}

		public Compra SetCompraDelete(
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

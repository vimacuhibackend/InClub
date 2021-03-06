using Inclub.Core;
using Inclub.Domain.SeedWork;
using System;

namespace Inclub.Domain.AggregatesModel.CompraAggregate
{
    public class CompraDetalle : Entity, IAggregateRoot, IValidable
	{

		public int IdCompra { get; private set; }
		public int Cantidad { get; private set; }
		public int IdProducto { get; private set; }
		public decimal PrecioUnitario { get; private set; }
		public decimal PrecioTotal { get; private set; }
		public bool EsEliminado { get; private set; }

		protected CompraDetalle() { }

		public CompraDetalle(
			int idCompra,
			int cantidad,
			int idProducto,
			decimal precioUnitario,
			decimal precioTotal,
			Guid usuarioCreacion,
			String ipCreacion) : this()
		{
			this.IdCompra = idCompra;
			this.Cantidad = cantidad;
			this.IdProducto = idProducto;
			this.PrecioUnitario = precioUnitario;
			this.PrecioTotal = precioTotal;
			this.EsEliminado = false;
			this.UsuarioCreacion = usuarioCreacion;
			this.FechaCreacion = DateTime.Now;
			this.IpCreacion = ipCreacion;
		}

		public CompraDetalle SetCompraDetalleUpdate(
			int idCompra,
			int cantidad,
			int idProducto,
			decimal precioUnitario,
			decimal precioTotal,
			Guid? usuarioModificacion,
			String ipModificacion)
		{
			this.IdCompra = idCompra;
			this.Cantidad = cantidad;
			this.IdProducto = idProducto;
			this.PrecioUnitario = precioUnitario;
			this.PrecioTotal = precioTotal;

			this.UsuarioModificacion = usuarioModificacion;
			this.FechaModificacion = DateTime.Now;
			this.IpModificacion = ipModificacion;
			return this;
		}

		public CompraDetalle SetCompraDetalleDelete(
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

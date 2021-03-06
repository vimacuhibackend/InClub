using Inclub.Core;
using Inclub.Domain.SeedWork;
using System;

namespace Inclub.Domain.AggregatesModel.ProductoAggregate
{
    public class Producto : Entity, IAggregateRoot, IValidable
	{

		public string CodigoProducto { get; private set; }
		public string NombreProducto { get; private set; }
		public int StockInicial { get; private set; }
		public int StockActual { get; private set; }
		public decimal PrecioVenta { get; private set; }
		public bool EsEliminado { get; private set; }

		protected Producto() { }

		public Producto(
			string codigoProducto,
			string nombreProducto,
			int stockInicial,
			int stockActual,
			decimal precioVenta,
			Guid usuarioCreacion,
			String ipCreacion) : this()
		{
			this.CodigoProducto = codigoProducto;
			this.NombreProducto = nombreProducto;
			this.StockInicial = stockInicial;
			this.StockActual = stockActual;
			this.PrecioVenta = precioVenta;
			this.EsEliminado = false;
			this.UsuarioCreacion = usuarioCreacion;
			this.FechaCreacion = DateTime.Now;
			this.IpCreacion = ipCreacion;
		}

		public Producto SetProductoUpdate(
			string codigoProducto,
			string nombreProducto,
			int stockInicial,
			int stockActual,
			decimal precioVenta,
			Guid? usuarioModificacion,
			String ipModificacion)
		{
			this.CodigoProducto = codigoProducto;
			this.NombreProducto = nombreProducto;
			this.StockInicial = stockInicial;
			this.StockActual = stockActual;
			this.PrecioVenta = precioVenta;

			this.UsuarioModificacion = usuarioModificacion;
			this.FechaModificacion = DateTime.Now;
			this.IpModificacion = ipModificacion;
			return this;
		}

		public Producto SetProductoDelete(
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

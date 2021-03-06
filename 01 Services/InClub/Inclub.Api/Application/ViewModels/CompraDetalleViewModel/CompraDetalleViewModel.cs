
using System;

namespace Inclub.Api.Application.ViewModels.CompraDetalleViewModel
{

	public class CompraDetalleItemsDto
	{
		public long? RowNum {get; set;}
		public int IdCompraDetalle{ get; set; }
		public int IdCompra{ get; set; }
		public int Cantidad{ get; set; }
		public int IdProducto{ get; set; }
		public decimal PrecioUnitario{ get; set; }
		public decimal PrecioTotal{ get; set; }
        public Guid Guid { get; set; }
        public int? RowCount { get; set; }
	}

	public class CompraDetalleBusquedaRequest
	{
		public string IdCompra { get; set; }
		public string FechaRegistroDesde { get; set; }
		public string FechaRegistroHasta { get; set; }
	}
}

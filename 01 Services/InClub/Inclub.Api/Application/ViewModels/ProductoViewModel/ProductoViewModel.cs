
using System;

namespace Inclub.Api.Application.ViewModels.ProductoViewModel
{

	public class ProductoItemsDto
	{
		public long? RowNum {get; set;}
		public int IdProducto{ get; set; }
		public string CodigoProducto{ get; set; }
		public string NombreProducto{ get; set; }
		public int StockInicial{ get; set; }
		public int StockActual{ get; set; }
		public decimal PrecioVenta{ get; set; }
		public Guid Guid { get; set; }
		public int? RowCount { get; set; }
	}

	public class ProductoBusquedaRequest
	{
		public string CodigoProducto { get; set; }
		public string NombreProducto { get; set; }
		public string FechaRegistroDesde { get; set; }
		public string FechaRegistroHasta { get; set; }
	}
}

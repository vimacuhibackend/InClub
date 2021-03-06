
using System;

namespace Inclub.Api.Application.ViewModels.CompraViewModel
{

	public class CompraItemsDto
	{
		public long? RowNum {get; set;}
		public int IdCompra{ get; set; }
		public int IdUsuario{ get; set; }
		public decimal Subtotal{ get; set; }
		public decimal Total{ get; set; }
        public Guid Guid { get; set; }
        public int? RowCount { get; set; }
	}

	public class CompraBusquedaRequest
	{
		public string IdUsuario { get; set; }
		public string FechaRegistroDesde { get; set; }
		public string FechaRegistroHasta { get; set; }
	}
}

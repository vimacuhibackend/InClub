
using System;

namespace Inclub.Api.Application.ViewModels.UsuarioViewModel
{

	public class UsuarioItemsDto
	{
		public long? RowNum {get; set;}
		public int IdUsuario{ get; set; }
		public string UidUsuario{ get; set; }
		public string PasswordHash{ get; set; }
		public string NombreUsuario{ get; set; }
		public Guid Guid { get; set; }
		public int? RowCount { get; set; }
	}

	public class UsuarioBusquedaRequest
	{
		public string NombreUsuario { get; set; }
		public string FechaRegistroDesde { get; set; }
		public string FechaRegistroHasta { get; set; }
	}
}

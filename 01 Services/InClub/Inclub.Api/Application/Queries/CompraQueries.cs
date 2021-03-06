using Dapper;
using Inclub.Api.Application.ViewModels;
using Inclub.Api.Application.ViewModels.CompraViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Inclub.Api.Application.Queries
{
    public class CompraQueries : ICompraQueries
	{
		private string _connectionString = string.Empty;

		public CompraQueries(string constr)
		{
			_connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
		}

		public async Task<List<CompraItemsDto>> ListarCompraAsync(PaginatedItemsRequestViewModel<CompraBusquedaRequest> request)
		{
			List<CompraItemsDto> items;
			long count = 0;
			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();

				DynamicParameters parameter = new DynamicParameters();

				//paginacion
				parameter.Add("@REGISTRO_INICIO_DESDE", request.Skip, DbType.Int32, ParameterDirection.Input);
				parameter.Add("@REGISTROS_POR_PAGINA", request.PageSize, DbType.Int32, ParameterDirection.Input);
				parameter.Add("@ORDERBY", request.SortField, DbType.String, ParameterDirection.Input);
				parameter.Add("@ORDERBYASCDESC", request.SortDir, DbType.String, ParameterDirection.Input);

				//tabla

				parameter.Add("@ID_USUARIO", request.Filter is null ? null : request.Filter.IdUsuario, DbType.String, ParameterDirection.Input);
				var result = await connection.QueryAsync<dynamic>("USP_SEL_COMPRA", parameter, null, null, CommandType.StoredProcedure);
				items = MapCompraBusqueda(result);
				count = items.Count;
			}
			return new List<CompraItemsDto>(items);
		}


		private List<CompraItemsDto> MapCompraBusqueda(dynamic result)
		{
			var compraList = new List<CompraItemsDto>();
			foreach (dynamic item in result)
			{
				var newItem = new CompraItemsDto()
				{
					RowNum = (item.ROWNUM is null)? 1 : item.ROWNUM,
					IdCompra = item.ID_COMPRA,
					IdUsuario = item.ID_USUARIO,
					Subtotal = item.SUBTOTAL,
					Total = item.TOTAL,
					Guid = item.GUID,
					RowCount = (item.ROW_COUNT is null) ? 1 : item.ROW_COUNT
				};
				compraList.Add(newItem);
			}
			return compraList;
		}
	}
}

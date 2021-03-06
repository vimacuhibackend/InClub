using Dapper;
using Inclub.Api.Application.ViewModels;
using Inclub.Api.Application.ViewModels.CompraDetalleViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Inclub.Api.Application.Queries
{
    public class CompraDetalleQueries : ICompraDetalleQueries
	{
		private string _connectionString = string.Empty;

		public CompraDetalleQueries(string constr)
		{
			_connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
		}

		public async Task<List<CompraDetalleItemsDto>> ListarCompraDetalleAsync(PaginatedItemsRequestViewModel<CompraDetalleBusquedaRequest> request)
		{
			List<CompraDetalleItemsDto> items;
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

				parameter.Add("@ID_COMPRA", request.Filter is null ? null : request.Filter.IdCompra, DbType.String, ParameterDirection.Input);
				var result = await connection.QueryAsync<dynamic>("USP_SEL_COMPRADETALLE", parameter, null, null, CommandType.StoredProcedure);
				items = MapCompraDetalleBusqueda(result);
				count = items.Count;
			}
			return new List<CompraDetalleItemsDto>(items);
		}


		private List<CompraDetalleItemsDto> MapCompraDetalleBusqueda(dynamic result)
		{
			var compradetalleList = new List<CompraDetalleItemsDto>();
			foreach (dynamic item in result)
			{
				var newItem = new CompraDetalleItemsDto()
				{
					RowNum = (item.ROWNUM is null)? 1 : item.ROWNUM,
					IdCompraDetalle = item.ID_COMPRA_DETALLE,
					IdCompra = item.ID_COMPRA,
					Cantidad = item.CANTIDAD,
					IdProducto = item.ID_PRODUCTO,
					PrecioUnitario = item.PRECIO_UNITARIO,
					PrecioTotal = item.PRECIO_TOTAL,
					Guid = item.GUID,
					RowCount = (item.ROW_COUNT is null) ? 1 : item.ROW_COUNT
				};
				compradetalleList.Add(newItem);
			}
			return compradetalleList;
		}
	}
}

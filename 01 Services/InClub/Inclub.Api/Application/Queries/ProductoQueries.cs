using Dapper;
using Inclub.Api.Application.ViewModels;
using Inclub.Api.Application.ViewModels.ProductoViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Inclub.Api.Application.Queries
{
    public class ProductoQueries : IProductoQueries
	{
		private string _connectionString = string.Empty;

		public ProductoQueries(string constr)
		{
			_connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
		}

		public async Task<List<ProductoItemsDto>> ListarProductoAsync(PaginatedItemsRequestViewModel<ProductoBusquedaRequest> request)
		{
			List<ProductoItemsDto> items;
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

				parameter.Add("@CODIGO_PRODUCTO", request.Filter is null ? null : request.Filter.CodigoProducto, DbType.String, ParameterDirection.Input);
				parameter.Add("@NOMBRE_PRODUCTO", request.Filter is null ? null : request.Filter.NombreProducto, DbType.String, ParameterDirection.Input);
				var result = await connection.QueryAsync<dynamic>("USP_SEL_PRODUCTO", parameter, null, null, CommandType.StoredProcedure);
				items = MapProductoBusqueda(result);
				count = items.Count;
			}
			return new List<ProductoItemsDto>(items);
		}


		private List<ProductoItemsDto> MapProductoBusqueda(dynamic result)
		{
			var productoList = new List<ProductoItemsDto>();
			foreach (dynamic item in result)
			{
				var newItem = new ProductoItemsDto()
				{
					RowNum = (item.ROWNUM is null)? 1 : item.ROWNUM,
					IdProducto = item.ID_PRODUCTO,
					CodigoProducto = item.CODIGO_PRODUCTO,
					NombreProducto = item.NOMBRE_PRODUCTO,
					StockInicial = item.STOCK_INICIAL,
					StockActual = item.STOCK_ACTUAL,
					PrecioVenta = item.PRECIO_VENTA,
					Guid = item.GUID,
					RowCount = (item.ROW_COUNT is null) ? 1 : item.ROW_COUNT
				};
				productoList.Add(newItem);
			}
			return productoList;
		}
	}
}

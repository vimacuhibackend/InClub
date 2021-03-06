using Dapper;
using Inclub.Api.Application.ViewModels;
using Inclub.Api.Application.ViewModels.UsuarioViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Inclub.Api.Application.Queries
{
    public class UsuarioQueries : IUsuarioQueries
	{
		private string _connectionString = string.Empty;

		public UsuarioQueries(string constr)
		{
			_connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
		}

		public async Task<List<UsuarioItemsDto>> ListarUsuarioAsync(PaginatedItemsRequestViewModel<UsuarioBusquedaRequest> request)
		{
			List<UsuarioItemsDto> items;
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

				parameter.Add("@NOMBRE_USUARIO", request.Filter is null ? null : request.Filter.NombreUsuario, DbType.String, ParameterDirection.Input);
				var result = await connection.QueryAsync<dynamic>("USP_SEL_USUARIO", parameter, null, null, CommandType.StoredProcedure);
				items = MapUsuarioBusqueda(result);
				count = items.Count;
			}
			return new List<UsuarioItemsDto>(items);
		}


		private List<UsuarioItemsDto> MapUsuarioBusqueda(dynamic result)
		{
			var usuarioList = new List<UsuarioItemsDto>();
			foreach (dynamic item in result)
			{
				var newItem = new UsuarioItemsDto()
				{
					RowNum = (item.ROWNUM is null)? 1 : item.ROWNUM,
					IdUsuario = item.ID_USUARIO,
					UidUsuario = item.UID_USUARIO,
					PasswordHash = item.PASSWORD_HASH,
					NombreUsuario = item.NOMBRE_USUARIO,
					Guid = item.GUID,
					RowCount = (item.ROW_COUNT is null) ? 1 : item.ROW_COUNT
				};
				usuarioList.Add(newItem);
			}
			return usuarioList;
		}
	}
}

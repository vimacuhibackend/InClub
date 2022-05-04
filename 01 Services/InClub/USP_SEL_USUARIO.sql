USE [db.inclub]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =========================================================================================================
-- Propósito	: Retorna lista de Usuario
-- Creado por	: 
-- Fecha		: 4/05/2022 10:55:15
------------------------------------------------------------------------------------------------------------
-- Modificado por:
-- Fecha		:
-- Detalle		: Devuelve listado de Usuario
------------------------------------------------------------------------------------------------------------
--TEST: EXECUTE [USP_SEL_USUARIO] 0, 10, null, null, null
-- =========================================================================================================
CREATE PROCEDURE [dbo].[USP_SEL_USUARIO]
(
	@REGISTRO_INICIO_DESDE AS INT = 0,
	@REGISTROS_POR_PAGINA AS INT = 10,
	@ORDERBY	VARCHAR(50),
	@ORDERBYASCDESC VARCHAR(4),
	@NOMBRE_USUARIO AS NVARCHAR(250)= null,
	@FECHA_REGISTRO_DESDE nvarchar(50)=null,
	@FECHA_REGISTRO_HASTA nvarchar(50)=null
)
AS
BEGIN
	SET QUOTED_IDENTIFIER OFF
	SET NOCOUNT ON;

	if @FECHA_REGISTRO_DESDE is null set @FECHA_REGISTRO_DESDE = ''
	if @FECHA_REGISTRO_HASTA is null set @FECHA_REGISTRO_HASTA = GETDATE()
	if @NOMBRE_USUARIO is null set @NOMBRE_USUARIO = ''

	SET NOCOUNT ON;

		SELECT
				[tbl].[NUMERO_FILA] as [ROWNUM]
				,[tbl].TOTAL_FILAS as [ROW_COUNT]
				,[tbl].TOTAL_FILAS
				,[tbl].ID_USUARIO
				,[tbl].UID_USUARIO
				,[tbl].PASSWORD_HASH
				,[tbl].NOMBRE_USUARIO
				,[tbl].GUID
				,[tbl].ES_ELIMINADO
				,format([tbl].FECHA_CREACION, 'dd/MM/yyyy', 'en-us') FECHA_REGISTRO
			FROM
			(
				SELECT
					ROW_NUMBER() OVER (ORDER BY
											CASE WHEN @ORDERBY = 'fechaRegistro' AND @ORDERBYASCDESC = 'asc' THEN u.FECHA_CREACION END ASC
											,CASE WHEN @ORDERBY = 'fechaRegistro' AND @ORDERBYASCDESC = 'desc' THEN u.FECHA_CREACION END DESC) AS [NUMERO_FILA],
					COUNT(1) OVER () AS [TOTAL_FILAS],
					u.ID_USUARIO,
					u.UID_USUARIO,
					u.PASSWORD_HASH,
					u.NOMBRE_USUARIO,
					u.GUID,
					u.ES_ELIMINADO,
					u.FECHA_CREACION
				FROM [dbo].[usuario] u
					WHERE
						(@NOMBRE_USUARIO = '' OR u.NOMBRE_USUARIO IN (SELECT VALOR FROM [dbo].[USF_CADENA_A_TABLA_VARCHAR](@NOMBRE_USUARIO)))
						AND ( 							(@FECHA_REGISTRO_DESDE <> '' AND u.FECHA_CREACION  between  CAST(@FECHA_REGISTRO_DESDE  As datetime2 ) AND DATEADD(DAY,1,CAST(@FECHA_REGISTRO_HASTA AS datetime2)))						OR							(@FECHA_REGISTRO_DESDE = '' AND u.FECHA_CREACION <DATEADD(DAY,1,CAST(@FECHA_REGISTRO_HASTA AS datetime2)))						AND u.ES_ELIMINADO = 0
			) [tbl]
			WHERE (
			@REGISTROS_POR_PAGINA = 0 OR([NUMERO_FILA] BETWEEN (@REGISTRO_INICIO_DESDE+1) AND (@REGISTRO_INICIO_DESDE + @REGISTROS_POR_PAGINA))
			);

	SET NOCOUNT OFF;
END

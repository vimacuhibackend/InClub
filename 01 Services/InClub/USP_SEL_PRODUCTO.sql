USE [db.inclub]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =========================================================================================================
-- Propósito	: Retorna lista de Producto
-- Creado por	: 
-- Fecha		: 4/05/2022 10:55:38
------------------------------------------------------------------------------------------------------------
-- Modificado por:
-- Fecha		:
-- Detalle		: Devuelve listado de Producto
------------------------------------------------------------------------------------------------------------
--TEST: EXECUTE [USP_SEL_PRODUCTO] 0, 10, null, null, null, null
-- =========================================================================================================
CREATE PROCEDURE [dbo].[USP_SEL_PRODUCTO]
(
	@REGISTRO_INICIO_DESDE AS INT = 0,
	@REGISTROS_POR_PAGINA AS INT = 10,
	@ORDERBY	VARCHAR(50),
	@ORDERBYASCDESC VARCHAR(4),
	@CODIGO_PRODUCTO AS NVARCHAR(250)= null,
	@NOMBRE_PRODUCTO AS NVARCHAR(250)= null,
	@FECHA_REGISTRO_DESDE nvarchar(50)=null,
	@FECHA_REGISTRO_HASTA nvarchar(50)=null
)
AS
BEGIN
	SET QUOTED_IDENTIFIER OFF
	SET NOCOUNT ON;

	if @FECHA_REGISTRO_DESDE is null set @FECHA_REGISTRO_DESDE = ''
	if @FECHA_REGISTRO_HASTA is null set @FECHA_REGISTRO_HASTA = GETDATE()
	if @CODIGO_PRODUCTO is null set @CODIGO_PRODUCTO = ''
	if @NOMBRE_PRODUCTO is null set @NOMBRE_PRODUCTO = ''

	SET NOCOUNT ON;

		SELECT
				[tbl].[NUMERO_FILA] as [ROWNUM]
				,[tbl].TOTAL_FILAS as [ROW_COUNT]
				,[tbl].TOTAL_FILAS
				,[tbl].ID_PRODUCTO
				,[tbl].CODIGO_PRODUCTO
				,[tbl].NOMBRE_PRODUCTO
				,[tbl].STOCK_INICIAL
				,[tbl].STOCK_ACTUAL
				,[tbl].PRECIO_VENTA
				,[tbl].GUID
				,[tbl].ES_ELIMINADO
				,format([tbl].FECHA_CREACION, 'dd/MM/yyyy', 'en-us') FECHA_REGISTRO
			FROM
			(
				SELECT
					ROW_NUMBER() OVER (ORDER BY
											CASE WHEN @ORDERBY = 'fechaRegistro' AND @ORDERBYASCDESC = 'asc' THEN p.FECHA_CREACION END ASC
											,CASE WHEN @ORDERBY = 'fechaRegistro' AND @ORDERBYASCDESC = 'desc' THEN p.FECHA_CREACION END DESC) AS [NUMERO_FILA],
					COUNT(1) OVER () AS [TOTAL_FILAS],
					p.ID_PRODUCTO,
					p.CODIGO_PRODUCTO,
					p.NOMBRE_PRODUCTO,
					p.STOCK_INICIAL,
					p.STOCK_ACTUAL,
					p.PRECIO_VENTA,
					p.GUID,
					p.ES_ELIMINADO,
					p.FECHA_CREACION
				FROM [dbo].[producto] p
					WHERE
						(@CODIGO_PRODUCTO = '' OR p.CODIGO_PRODUCTO IN (SELECT VALOR FROM [dbo].[USF_CADENA_A_TABLA_VARCHAR](@CODIGO_PRODUCTO)))
						AND (@NOMBRE_PRODUCTO = '' OR p.NOMBRE_PRODUCTO IN (SELECT VALOR FROM [dbo].[USF_CADENA_A_TABLA_VARCHAR](@NOMBRE_PRODUCTO)))
						AND ( 							(@FECHA_REGISTRO_DESDE <> '' AND p.FECHA_CREACION  between  CAST(@FECHA_REGISTRO_DESDE  As datetime2 ) AND DATEADD(DAY,1,CAST(@FECHA_REGISTRO_HASTA AS datetime2)))						OR							(@FECHA_REGISTRO_DESDE = '' AND p.FECHA_CREACION <DATEADD(DAY,1,CAST(@FECHA_REGISTRO_HASTA AS datetime2)))						AND p.ES_ELIMINADO = 0
			) [tbl]
			WHERE (
			@REGISTROS_POR_PAGINA = 0 OR([NUMERO_FILA] BETWEEN (@REGISTRO_INICIO_DESDE+1) AND (@REGISTRO_INICIO_DESDE + @REGISTROS_POR_PAGINA))
			);

	SET NOCOUNT OFF;
END

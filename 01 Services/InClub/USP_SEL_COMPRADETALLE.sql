USE [db.inclub]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =========================================================================================================
-- Propósito	: Retorna lista de CompraDetalle
-- Creado por	: 
-- Fecha		: 4/05/2022 10:56:12
------------------------------------------------------------------------------------------------------------
-- Modificado por:
-- Fecha		:
-- Detalle		: Devuelve listado de CompraDetalle
------------------------------------------------------------------------------------------------------------
--TEST: EXECUTE [USP_SEL_COMPRADETALLE] 0, 10, null, null, null
-- =========================================================================================================
CREATE PROCEDURE [dbo].[USP_SEL_COMPRADETALLE]
(
	@REGISTRO_INICIO_DESDE AS INT = 0,
	@REGISTROS_POR_PAGINA AS INT = 10,
	@ORDERBY	VARCHAR(50),
	@ORDERBYASCDESC VARCHAR(4),
	@ID_COMPRA AS NVARCHAR(250)= null,
	@FECHA_REGISTRO_DESDE nvarchar(50)=null,
	@FECHA_REGISTRO_HASTA nvarchar(50)=null
)
AS
BEGIN
	SET QUOTED_IDENTIFIER OFF
	SET NOCOUNT ON;

	if @FECHA_REGISTRO_DESDE is null set @FECHA_REGISTRO_DESDE = ''
	if @FECHA_REGISTRO_HASTA is null set @FECHA_REGISTRO_HASTA = GETDATE()
	if @ID_COMPRA is null set @ID_COMPRA = ''

	SET NOCOUNT ON;

		SELECT
				[tbl].[NUMERO_FILA] as [ROWNUM]
				,[tbl].TOTAL_FILAS as [ROW_COUNT]
				,[tbl].TOTAL_FILAS
				,[tbl].ID_COMPRA_DETALLE
				,[tbl].ID_COMPRA
				,[tbl].CANTIDAD
				,[tbl].ID_PRODUCTO
				,[tbl].PRECIO_UNITARIO
				,[tbl].PRECIO_TOTAL
				,[tbl].GUID
				,[tbl].ES_ELIMINADO
				,format([tbl].FECHA_CREACION, 'dd/MM/yyyy', 'en-us') FECHA_REGISTRO
			FROM
			(
				SELECT
					ROW_NUMBER() OVER (ORDER BY
											CASE WHEN @ORDERBY = 'fechaRegistro' AND @ORDERBYASCDESC = 'asc' THEN c.FECHA_CREACION END ASC
											,CASE WHEN @ORDERBY = 'fechaRegistro' AND @ORDERBYASCDESC = 'desc' THEN c.FECHA_CREACION END DESC) AS [NUMERO_FILA],
					COUNT(1) OVER () AS [TOTAL_FILAS],
					c.ID_COMPRA_DETALLE,
					c.ID_COMPRA,
					c.CANTIDAD,
					c.ID_PRODUCTO,
					c.PRECIO_UNITARIO,
					c.PRECIO_TOTAL,
					c.GUID,
					c.ES_ELIMINADO,
					c.FECHA_CREACION
				FROM [dbo].[compra_detalle] c
					WHERE
						(@ID_COMPRA = '' OR c.ID_COMPRA IN (SELECT VALOR FROM [dbo].[USF_CADENA_A_TABLA_INT](@ID_COMPRA)))
						AND ( 							(@FECHA_REGISTRO_DESDE <> '' AND c.FECHA_CREACION  between  CAST(@FECHA_REGISTRO_DESDE  As datetime2 ) AND DATEADD(DAY,1,CAST(@FECHA_REGISTRO_HASTA AS datetime2)))						OR							(@FECHA_REGISTRO_DESDE = '' AND c.FECHA_CREACION <DATEADD(DAY,1,CAST(@FECHA_REGISTRO_HASTA AS datetime2)))						AND c.ES_ELIMINADO = 0
			) [tbl]
			WHERE (
			@REGISTROS_POR_PAGINA = 0 OR([NUMERO_FILA] BETWEEN (@REGISTRO_INICIO_DESDE+1) AND (@REGISTRO_INICIO_DESDE + @REGISTROS_POR_PAGINA))
			);

	SET NOCOUNT OFF;
END

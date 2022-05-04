USE [master]
GO
/****** Object:  Database [db.inclub]    Script Date: 4/05/2022 13:51:09 ******/
CREATE DATABASE [db.inclub]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'db.inclub', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\db.inclub.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'db.inclub_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\db.inclub_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [db.inclub] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [db.inclub].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [db.inclub] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [db.inclub] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [db.inclub] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [db.inclub] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [db.inclub] SET ARITHABORT OFF 
GO
ALTER DATABASE [db.inclub] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [db.inclub] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [db.inclub] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [db.inclub] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [db.inclub] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [db.inclub] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [db.inclub] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [db.inclub] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [db.inclub] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [db.inclub] SET  ENABLE_BROKER 
GO
ALTER DATABASE [db.inclub] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [db.inclub] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [db.inclub] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [db.inclub] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [db.inclub] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [db.inclub] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [db.inclub] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [db.inclub] SET RECOVERY FULL 
GO
ALTER DATABASE [db.inclub] SET  MULTI_USER 
GO
ALTER DATABASE [db.inclub] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [db.inclub] SET DB_CHAINING OFF 
GO
ALTER DATABASE [db.inclub] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [db.inclub] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [db.inclub] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [db.inclub] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'db.inclub', N'ON'
GO
ALTER DATABASE [db.inclub] SET QUERY_STORE = OFF
GO
USE [db.inclub]
GO
/****** Object:  UserDefinedFunction [dbo].[USF_CADENA_A_TABLA_INT]    Script Date: 4/05/2022 13:51:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--============================================================================================= 
-- Propósito	: Convierte lista seguida de comas a tabla con campo del tipo numerico
-- Creado por	: Herón Meza
-- Fecha		: 01.11.2018
-----------------------------------------------------------------------------------------------	
-- Prueba		: SELECT * FROM [dbo].[USF_CADENA_A_TABLA_INT]('1,2,3')
--=============================================================================================
CREATE FUNCTION [dbo].[USF_CADENA_A_TABLA_INT](@entrada AS VARCHAR(4000))
RETURNS
      @resultado TABLE(VALOR INT)
AS
BEGIN
      DECLARE @str VARCHAR(50)
      DECLARE @ind INT
      IF(@entrada is not null)
      BEGIN
            SET @ind = CHARINDEX(',', @entrada)
            WHILE @ind > 0
            BEGIN
                  SET @str = SUBSTRING(@entrada, 1, @ind - 1)
                  SET @entrada = SUBSTRING(@entrada, @ind + 1, LEN(@entrada) - @ind)
                  INSERT INTO @resultado values (@str)
                  SET @ind = CHARINDEX(',', @entrada)
            END
            SET @str = @entrada
            INSERT INTO @resultado VALUES (@str)
      END
      RETURN
END
GO
/****** Object:  UserDefinedFunction [dbo].[USF_CADENA_A_TABLA_VARCHAR]    Script Date: 4/05/2022 13:51:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--============================================================================================= 
-- Propósito	: Convierte lista seguida de comas a tabla con campo del tipo varchar
-- Creado por	: Herón Meza
-- Fecha		: 01.11.2018
-----------------------------------------------------------------------------------------------	
-- Prueba		: SELECT * FROM [dbo].[USF_CADENA_A_TABLA_VARCHAR]('001,002,003')
--=============================================================================================
CREATE FUNCTION [dbo].[USF_CADENA_A_TABLA_VARCHAR](@entrada AS VARCHAR(4000))
RETURNS
      @resultado TABLE(VALOR VARCHAR(20))
AS
BEGIN
      DECLARE @str VARCHAR(50)
      DECLARE @ind INT
      IF(@entrada is not null)
      BEGIN
            SET @ind = CHARINDEX(',', @entrada)
            WHILE @ind > 0
            BEGIN
                  SET @str = SUBSTRING(@entrada, 1, @ind - 1)
                  SET @entrada = SUBSTRING(@entrada, @ind + 1, LEN(@entrada) - @ind)
                  INSERT INTO @resultado values (@str)
                  SET @ind = CHARINDEX(',', @entrada)
            END
            SET @str = @entrada
            INSERT INTO @resultado VALUES (@str)
      END
      RETURN
END
GO
/****** Object:  Table [dbo].[compra]    Script Date: 4/05/2022 13:51:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[compra](
	[ID_COMPRA] [int] IDENTITY(1,1) NOT NULL,
	[ID_USUARIO] [int] NOT NULL,
	[SUBTOTAL] [decimal](8, 2) NOT NULL,
	[TOTAL] [decimal](8, 2) NOT NULL,
	[GUID] [uniqueidentifier] NOT NULL,
	[ES_ELIMINADO] [bit] NOT NULL,
	[USUARIO_CREACION] [uniqueidentifier] NOT NULL,
	[FECHA_CREACION] [datetime2](7) NOT NULL,
	[IP_CREACION] [nvarchar](15) NOT NULL,
	[USUARIO_MODIFICACION] [uniqueidentifier] NULL,
	[FECHA_MODIFICACION] [datetime2](7) NULL,
	[IP_MODIFICACION] [nvarchar](15) NULL,
 CONSTRAINT [PK_compra] PRIMARY KEY CLUSTERED 
(
	[ID_COMPRA] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[compra_detalle]    Script Date: 4/05/2022 13:51:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[compra_detalle](
	[ID_COMPRA_DETALLE] [int] IDENTITY(1,1) NOT NULL,
	[ID_COMPRA] [int] NOT NULL,
	[CANTIDAD] [int] NOT NULL,
	[ID_PRODUCTO] [int] NOT NULL,
	[PRECIO_UNITARIO] [decimal](8, 2) NOT NULL,
	[PRECIO_TOTAL] [decimal](8, 2) NOT NULL,
	[GUID] [uniqueidentifier] NOT NULL,
	[ES_ELIMINADO] [bit] NOT NULL,
	[USUARIO_CREACION] [uniqueidentifier] NOT NULL,
	[FECHA_CREACION] [datetime2](7) NOT NULL,
	[IP_CREACION] [nvarchar](15) NOT NULL,
	[USUARIO_MODIFICACION] [uniqueidentifier] NULL,
	[FECHA_MODIFICACION] [datetime2](7) NULL,
	[IP_MODIFICACION] [nvarchar](15) NULL,
 CONSTRAINT [PK_compra_detalle] PRIMARY KEY CLUSTERED 
(
	[ID_COMPRA_DETALLE] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[producto]    Script Date: 4/05/2022 13:51:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[producto](
	[ID_PRODUCTO] [int] IDENTITY(1,1) NOT NULL,
	[CODIGO_PRODUCTO] [nvarchar](10) NOT NULL,
	[NOMBRE_PRODUCTO] [nvarchar](250) NOT NULL,
	[STOCK_INICIAL] [int] NOT NULL,
	[STOCK_ACTUAL] [int] NOT NULL,
	[PRECIO_VENTA] [decimal](8, 2) NOT NULL,
	[GUID] [uniqueidentifier] NOT NULL,
	[ES_ELIMINADO] [bit] NOT NULL,
	[USUARIO_CREACION] [uniqueidentifier] NOT NULL,
	[FECHA_CREACION] [datetime2](7) NOT NULL,
	[IP_CREACION] [nvarchar](15) NOT NULL,
	[USUARIO_MODIFICACION] [uniqueidentifier] NULL,
	[FECHA_MODIFICACION] [datetime2](7) NULL,
	[IP_MODIFICACION] [nvarchar](15) NULL,
 CONSTRAINT [PK_producto] PRIMARY KEY CLUSTERED 
(
	[ID_PRODUCTO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[usuario]    Script Date: 4/05/2022 13:51:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuario](
	[ID_USUARIO] [int] IDENTITY(1,1) NOT NULL,
	[UID_USUARIO] [nvarchar](40) NOT NULL,
	[PASSWORD_HASH] [nvarchar](250) NOT NULL,
	[NOMBRE_USUARIO] [nvarchar](40) NULL,
	[GUID] [uniqueidentifier] NOT NULL,
	[ES_ELIMINADO] [bit] NOT NULL,
	[USUARIO_CREACION] [uniqueidentifier] NOT NULL,
	[FECHA_CREACION] [datetime2](7) NOT NULL,
	[IP_CREACION] [nvarchar](15) NOT NULL,
	[USUARIO_MODIFICACION] [uniqueidentifier] NULL,
	[FECHA_MODIFICACION] [datetime2](7) NULL,
	[IP_MODIFICACION] [nvarchar](15) NULL,
 CONSTRAINT [PK_usuario] PRIMARY KEY CLUSTERED 
(
	[ID_USUARIO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[producto] ON 

INSERT [dbo].[producto] ([ID_PRODUCTO], [CODIGO_PRODUCTO], [NOMBRE_PRODUCTO], [STOCK_INICIAL], [STOCK_ACTUAL], [PRECIO_VENTA], [GUID], [ES_ELIMINADO], [USUARIO_CREACION], [FECHA_CREACION], [IP_CREACION], [USUARIO_MODIFICACION], [FECHA_MODIFICACION], [IP_MODIFICACION]) VALUES (1, N'P002CAMBIO', N'PRODUCTOCAMBIO', 100, 100, CAST(19.20 AS Decimal(8, 2)), N'e1aca62a-d4ce-4491-acb3-5233c428b8c9', 0, N'3c858138-cff8-415e-bb49-d8ba0eb9a7f8', CAST(N'2022-05-04T13:26:40.7816077' AS DateTime2), N'::1', N'd12bc817-6118-4633-8599-77cad28f5624', CAST(N'2022-05-04T13:29:37.4338164' AS DateTime2), N'::1')
INSERT [dbo].[producto] ([ID_PRODUCTO], [CODIGO_PRODUCTO], [NOMBRE_PRODUCTO], [STOCK_INICIAL], [STOCK_ACTUAL], [PRECIO_VENTA], [GUID], [ES_ELIMINADO], [USUARIO_CREACION], [FECHA_CREACION], [IP_CREACION], [USUARIO_MODIFICACION], [FECHA_MODIFICACION], [IP_MODIFICACION]) VALUES (2, N'P0001', N'producto2', 50, 50, CAST(17.50 AS Decimal(8, 2)), N'82886b6f-13c4-446f-a940-fc0078d9d582', 1, N'a2a39481-6301-4216-abe8-ea56ff8cc76c', CAST(N'2022-05-04T13:30:00.5415654' AS DateTime2), N'::1', N'd60db62e-2896-416d-9bd4-fd65431cef27', CAST(N'2022-05-04T13:30:42.9058576' AS DateTime2), N'::1')
SET IDENTITY_INSERT [dbo].[producto] OFF
GO
SET IDENTITY_INSERT [dbo].[usuario] ON 

INSERT [dbo].[usuario] ([ID_USUARIO], [UID_USUARIO], [PASSWORD_HASH], [NOMBRE_USUARIO], [GUID], [ES_ELIMINADO], [USUARIO_CREACION], [FECHA_CREACION], [IP_CREACION], [USUARIO_MODIFICACION], [FECHA_MODIFICACION], [IP_MODIFICACION]) VALUES (1, N'devchsoft', N'䫡뇉陔ᓔ욁螅ᙯ콘熖惩ꄫ㮝䱄罆媇緓ㇳ⋿�ẅⵘ泇ᙟ뙳曪읋◩␧Ṉ社鼞', N'devchsoft15', N'dd573193-9e75-41b4-afa1-d1219ac7637a', 0, N'934a3961-e574-4848-a061-658792cc853a', CAST(N'2022-05-04T12:44:36.2550432' AS DateTime2), N'::1', N'0c6425c5-7752-4310-8882-da85aab69131', CAST(N'2022-05-04T13:23:04.4317978' AS DateTime2), N'::1')
INSERT [dbo].[usuario] ([ID_USUARIO], [UID_USUARIO], [PASSWORD_HASH], [NOMBRE_USUARIO], [GUID], [ES_ELIMINADO], [USUARIO_CREACION], [FECHA_CREACION], [IP_CREACION], [USUARIO_MODIFICACION], [FECHA_MODIFICACION], [IP_MODIFICACION]) VALUES (2, N'devchsoft5', N'쑿ያҼ⭫䩯众➡嗓퇪ꠐ�酁芭詹와嗘༧ﵺ뷩ꫧ鞺긆㳴ఽ颉䁇⮒脺掠✮誌萱', N'devchsoft55', N'92576fa1-be1a-4f4d-b07f-ae42c08ca46c', 1, N'e8121324-fc9d-4bf7-9aae-521886b8bb30', CAST(N'2022-05-04T13:21:50.1965900' AS DateTime2), N'::1', N'5de1723f-37c3-4432-bc20-ba9a2bb1c9ec', CAST(N'2022-05-04T13:22:19.8468451' AS DateTime2), N'::1')
SET IDENTITY_INSERT [dbo].[usuario] OFF
GO
ALTER TABLE [dbo].[compra]  WITH CHECK ADD  CONSTRAINT [FK_compra_usuario] FOREIGN KEY([ID_USUARIO])
REFERENCES [dbo].[usuario] ([ID_USUARIO])
GO
ALTER TABLE [dbo].[compra] CHECK CONSTRAINT [FK_compra_usuario]
GO
ALTER TABLE [dbo].[compra_detalle]  WITH CHECK ADD  CONSTRAINT [FK_compra_detalle_compra] FOREIGN KEY([ID_COMPRA])
REFERENCES [dbo].[compra] ([ID_COMPRA])
GO
ALTER TABLE [dbo].[compra_detalle] CHECK CONSTRAINT [FK_compra_detalle_compra]
GO
/****** Object:  StoredProcedure [dbo].[USP_SEL_COMPRA]    Script Date: 4/05/2022 13:51:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =========================================================================================================
-- Propósito	: Retorna lista de Compra
-- Creado por	: 
-- Fecha		: 4/05/2022 10:56:02
------------------------------------------------------------------------------------------------------------
-- Modificado por:
-- Fecha		:
-- Detalle		: Devuelve listado de Compra
------------------------------------------------------------------------------------------------------------
--TEST: EXECUTE [USP_SEL_COMPRA] 0, 10, null, null, null
-- =========================================================================================================
CREATE PROCEDURE [dbo].[USP_SEL_COMPRA]
(
	@REGISTRO_INICIO_DESDE AS INT = 0,
	@REGISTROS_POR_PAGINA AS INT = 10,
	@ORDERBY	VARCHAR(50),
	@ORDERBYASCDESC VARCHAR(4),
	@ID_USUARIO AS NVARCHAR(250)= null,
	@FECHA_REGISTRO_DESDE nvarchar(50)=null,
	@FECHA_REGISTRO_HASTA nvarchar(50)=null
)
AS
BEGIN
	SET QUOTED_IDENTIFIER OFF
	SET NOCOUNT ON;

	if @FECHA_REGISTRO_DESDE is null set @FECHA_REGISTRO_DESDE = ''
	if @FECHA_REGISTRO_HASTA is null set @FECHA_REGISTRO_HASTA = GETDATE()
	if @ID_USUARIO is null set @ID_USUARIO = ''

	SET NOCOUNT ON;

		SELECT
				[tbl].[NUMERO_FILA] as [ROWNUM]
				,[tbl].TOTAL_FILAS as [ROW_COUNT]
				,[tbl].TOTAL_FILAS
				,[tbl].ID_COMPRA
				,[tbl].ID_USUARIO
				,[tbl].SUBTOTAL
				,[tbl].TOTAL
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
					c.ID_COMPRA,
					c.ID_USUARIO,
					c.SUBTOTAL,
					c.TOTAL,
					c.GUID,
					c.ES_ELIMINADO,
					c.FECHA_CREACION
				FROM [dbo].[compra] c
					WHERE
						(@ID_USUARIO = '' OR c.ID_USUARIO IN (SELECT VALOR FROM [dbo].[USF_CADENA_A_TABLA_INT](@ID_USUARIO)))
						AND ( 
							(@FECHA_REGISTRO_DESDE <> '' AND c.FECHA_CREACION  between  CAST(@FECHA_REGISTRO_DESDE  As datetime2 ) AND DATEADD(DAY,1,CAST(@FECHA_REGISTRO_HASTA AS datetime2)))
						OR
							(@FECHA_REGISTRO_DESDE = '' AND c.FECHA_CREACION <DATEADD(DAY,1,CAST(@FECHA_REGISTRO_HASTA AS datetime2)))
						AND c.ES_ELIMINADO = 0
			)) [tbl]
			WHERE (
			@REGISTROS_POR_PAGINA = 0 OR([NUMERO_FILA] BETWEEN (@REGISTRO_INICIO_DESDE+1) AND (@REGISTRO_INICIO_DESDE + @REGISTROS_POR_PAGINA))
			);

	SET NOCOUNT OFF;
END
GO
/****** Object:  StoredProcedure [dbo].[USP_SEL_COMPRADETALLE]    Script Date: 4/05/2022 13:51:09 ******/
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
						AND ( 
							(@FECHA_REGISTRO_DESDE <> '' AND c.FECHA_CREACION  between  CAST(@FECHA_REGISTRO_DESDE  As datetime2 ) AND DATEADD(DAY,1,CAST(@FECHA_REGISTRO_HASTA AS datetime2)))
						OR
							(@FECHA_REGISTRO_DESDE = '' AND c.FECHA_CREACION <DATEADD(DAY,1,CAST(@FECHA_REGISTRO_HASTA AS datetime2)))
						AND c.ES_ELIMINADO = 0
			)) [tbl]
			WHERE (
			@REGISTROS_POR_PAGINA = 0 OR([NUMERO_FILA] BETWEEN (@REGISTRO_INICIO_DESDE+1) AND (@REGISTRO_INICIO_DESDE + @REGISTROS_POR_PAGINA))
			);

	SET NOCOUNT OFF;
END
GO
/****** Object:  StoredProcedure [dbo].[USP_SEL_PRODUCTO]    Script Date: 4/05/2022 13:51:09 ******/
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
						AND ( 
							(@FECHA_REGISTRO_DESDE <> '' AND p.FECHA_CREACION  between  CAST(@FECHA_REGISTRO_DESDE  As datetime2 ) AND DATEADD(DAY,1,CAST(@FECHA_REGISTRO_HASTA AS datetime2)))
						OR
							(@FECHA_REGISTRO_DESDE = '' AND p.FECHA_CREACION <DATEADD(DAY,1,CAST(@FECHA_REGISTRO_HASTA AS datetime2)))
						AND p.ES_ELIMINADO = 0
			)) [tbl]
			WHERE (
			@REGISTROS_POR_PAGINA = 0 OR([NUMERO_FILA] BETWEEN (@REGISTRO_INICIO_DESDE+1) AND (@REGISTRO_INICIO_DESDE + @REGISTROS_POR_PAGINA))
			);

	SET NOCOUNT OFF;
END
GO
/****** Object:  StoredProcedure [dbo].[USP_SEL_USUARIO]    Script Date: 4/05/2022 13:51:09 ******/
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
						AND ( 
							(@FECHA_REGISTRO_DESDE <> '' AND u.FECHA_CREACION  between  CAST(@FECHA_REGISTRO_DESDE  As datetime2 ) AND DATEADD(DAY,1,CAST(@FECHA_REGISTRO_HASTA AS datetime2)))
						OR
							(@FECHA_REGISTRO_DESDE = '' AND u.FECHA_CREACION <DATEADD(DAY,1,CAST(@FECHA_REGISTRO_HASTA AS datetime2)))
						AND u.ES_ELIMINADO = 0
			)) [tbl]
			WHERE (
			@REGISTROS_POR_PAGINA = 0 OR([NUMERO_FILA] BETWEEN (@REGISTRO_INICIO_DESDE+1) AND (@REGISTRO_INICIO_DESDE + @REGISTROS_POR_PAGINA))
			);

	SET NOCOUNT OFF;
END
GO
USE [master]
GO
ALTER DATABASE [db.inclub] SET  READ_WRITE 
GO

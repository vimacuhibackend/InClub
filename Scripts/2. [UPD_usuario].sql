USE [db.inclub]
GO
IF OBJECT_ID(N'[UPD_usuario]') IS NOT NULL
BEGIN
    DROP TRIGGER [UPD_usuario]
    IF OBJECT_ID(N'[UPD_usuario]') IS NOT NULL
        PRINT N'<<< FAILED DROPPING TRIGGER [UPD_usuario] >>>'
    ELSE
        PRINT N'<<< DROPPED TRIGGER [UPD_usuario] >>>'
END
GO
/****** Object:  StoredProcedure [dbo].[USP_INS_MALLA_CURRICULAR]    Script Date: 22/11/2021 12:35:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--============================================================================================= 
-- Propósito	: 
-- Creador		: 
-- Fecha		: 
-----------------------------------------------------------------------------------------------
-- Prueba		: 
--============================================================================================= 
CREATE TRIGGER [UPD_usuario] ON [usuario]
AFTER UPDATE
AS	
BEGIN

	DECLARE @ID INT = NULL;
	DECLARE @PASSWORD NVARCHAR(250) = NULL;
	
	SELECT @ID = inserted.ID_USUARIO, @PASSWORD = HASHBYTES('SHA2_512', PASSWORD_HASH+CAST([GUID] AS NVARCHAR(36))) FROM inserted

	IF @PASSWORD IS NOT NULL
	BEGIN
		UPDATE [usuario] SET PASSWORD_HASH = @PASSWORD WHERE ID_USUARIO = @ID;
	END
END
GO
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF OBJECT_ID(N'[UPD_usuario]') IS NOT NULL
    PRINT N'<<< CREATED TRIGGER [UPD_usuario] >>>'
ELSE
    PRINT N'<<< FAILED CREATING TRIGGER [UPD_usuario] >>>'
GO
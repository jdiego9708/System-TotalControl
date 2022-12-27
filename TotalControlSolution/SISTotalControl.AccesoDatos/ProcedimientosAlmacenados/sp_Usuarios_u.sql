CREATE OR ALTER PROC sp_Usuarios_u
@Id_usuario int,
@Fecha_ingreso date,
@Alias varchar(50),
@Nombres varchar(50),
@Apellidos varchar(50),
@Identificacion varchar(50),
@Celular varchar(50),
@Email varchar(100),
@Tipo_usuario varchar(50),
@Estado_usuario varchar(50)
AS
BEGIN
	UPDATE Usuarios SET
	Fecha_ingreso = @Fecha_ingreso, 
	Alias = @Alias, 
	Nombres = @Nombres, 
	Apellidos = @Apellidos, 
	Identificacion = @Identificacion, 
	Celular = @Celular, 
	Email = @Email, 
	Tipo_usuario = @Tipo_usuario, 
	Estado_usuario = @Estado_usuario
	WHERE Id_usuario = @Id_usuario
END
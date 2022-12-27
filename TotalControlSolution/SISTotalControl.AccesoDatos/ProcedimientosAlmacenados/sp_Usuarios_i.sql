CREATE OR ALTER PROC sp_Usuarios_i
@Id_usuario int output,
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
	INSERT INTO Usuarios 
	VALUES (@Fecha_ingreso, @Alias, @Nombres, @Apellidos, 
	@Identificacion, @Celular, @Email, @Tipo_usuario, @Estado_usuario)

	SET @Id_usuario = SCOPE_IDENTITY();
END
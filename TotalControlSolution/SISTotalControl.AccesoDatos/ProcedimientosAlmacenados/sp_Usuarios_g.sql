CREATE OR ALTER PROC sp_Usuarios_g
@Tipo_busqueda varchar(50),
@Texto_busqueda varchar(50)
AS
BEGIN
	IF (@Tipo_busqueda = 'COMPLETO')
	BEGIN
		SELECT * 
		FROM Usuarios
	END
	ELSE IF (@Tipo_busqueda = 'ID USUARIO')
	BEGIN
		SELECT *
		FROM Usuarios us 
		INNER JOIN Usuarios_cobros usco ON us.Id_usuario = usco.Id_usuario
		WHERE us.Id_usuario = CONVERT(int, @Texto_busqueda)
		ORDER BY us.Id_usuario DESC
	END
	ELSE IF (@Tipo_busqueda = 'PIN')
	BEGIN
		SELECT *
		FROM Usuarios us 
		INNER JOIN Usuarios_cobros usco ON us.Id_usuario = usco.Id_usuario
		INNER JOIN Credenciales cr ON us.Id_usuario = cr.Id_usuario
		INNER JOIN Cobros co ON usco.Id_cobro = co.Id_cobro
		INNER JOIN Zonas zo ON co.Id_zona = zo.Id_zona
		INNER JOIN Ciudades ci ON zo.Id_ciudad = ci.Id_ciudad
		INNER JOIN Paises pa ON ci.Id_pais = pa.Id_pais
		WHERE cr.Password = @Texto_busqueda
	END
	ELSE IF (@Tipo_busqueda = 'REGLAS ID COBRO')
	BEGIN
		SELECT *
		FROM Reglas re
		INNER JOIN Cobros co ON re.Id_cobro = co.Id_cobro
		WHERE re.Id_cobro = CONVERT(int,@Texto_busqueda)
	END
	ELSE IF (@Tipo_busqueda = 'REGLAS ID USUARIO')
	BEGIN
		SELECT *
		FROM Usuarios_reglas usre 
		INNER JOIN Usuarios us ON us.Id_usuario = usre.Id_usuario
		INNER JOIN Reglas re ON usre.Id_regla = re.Id_regla
		INNER JOIN Cobros co ON re.Id_cobro = co.Id_cobro
		WHERE usre.Id_usuario = CONVERT(int, @Texto_busqueda)
	END
END
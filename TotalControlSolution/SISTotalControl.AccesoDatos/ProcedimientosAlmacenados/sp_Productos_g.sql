CREATE OR ALTER PROC sp_Productos_g
@Tipo_busqueda varchar(50),
@Texto_busqueda varchar(50)
AS
BEGIN TRY

IF (@Tipo_busqueda = 'ID PRODUCTO')
BEGIN
	SELECT * 
	FROM Productos pr 
	INNER JOIN Catalogo ca ON pr.Id_tipo_producto = ca.Id_tipo
	WHERE pr.Id_producto = CONVERT(int, @Texto_busqueda)
END
ELSE IF (@Tipo_busqueda = 'COMPLETO')
BEGIN
	SELECT TOP 50 * 
	FROM Productos pr 
	INNER JOIN Catalogo ca ON pr.Id_tipo_producto = ca.Id_tipo
	ORDER BY Id_producto DESC
END
ELSE IF (@Tipo_busqueda = 'NOMBRE')
BEGIN
	SELECT * 
	FROM Productos pr 
	INNER JOIN Catalogo ca ON pr.Id_tipo_producto = ca.Id_tipo
	WHERE pr.Nombre_producto like @Texto_busqueda + '%'
END
ELSE IF (@Tipo_busqueda = 'ID TIPO PRODUCTO')
BEGIN
	SELECT * 
	FROM Productos pr 
	INNER JOIN Catalogo ca ON pr.Id_tipo_producto = ca.Id_tipo
	WHERE pr.Id_tipo_producto = CONVERT(int, @Texto_busqueda)
END
ELSE IF (@Tipo_busqueda = 'ID COBRO')
BEGIN
	SELECT * 
	FROM Productos pr 
	INNER JOIN Catalogo ca ON pr.Id_tipo_producto = ca.Id_tipo
	WHERE pr.Id_cobro = CONVERT(int, @Texto_busqueda)
END
ELSE IF (@Tipo_busqueda = 'ID COBRO STOCK')
BEGIN	
	DECLARE @dtStockProductos table (Id_stock int, Id_producto int)

	INSERT INTO @dtStockProductos
	SELECT MAX(Id_stock) as Id_stock, Id_producto
	FROM Stock_productos
	GROUP BY Id_producto

	SELECT pr.*, ca.*, sto.*
	FROM Productos pr 
	INNER JOIN Catalogo ca ON pr.Id_tipo_producto = ca.Id_tipo
	INNER JOIN @dtStockProductos st ON pr.Id_producto = st.Id_producto
	INNER JOIN Stock_productos sto ON st.Id_stock = sto.Id_stock
	WHERE pr.Id_cobro = CONVERT(int, '1')
END

END TRY
BEGIN CATCH
DECLARE @Mensaje_error NVARCHAR(4000) = ERROR_MESSAGE();
DECLARE @Mensaje_severidad INT = ERROR_SEVERITY();
DECLARE @Estado_error INT = ERROR_STATE();
DECLARE @Numero_error INT = ERROR_NUMBER();
DECLARE @Error_procedure varchar(500) = ERROR_PROCEDURE();
DECLARE @Error_line INT = Error_line();
RAISERROR (@Mensaje_error,
           @Mensaje_severidad,
           @Estado_error,
		   @Numero_error,
		   @Error_procedure,
		   @Error_line
           ); 
END CATCH
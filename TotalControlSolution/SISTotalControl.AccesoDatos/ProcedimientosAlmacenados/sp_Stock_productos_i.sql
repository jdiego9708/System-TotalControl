CREATE OR ALTER PROC sp_Stock_productos_i
@Id_stock int output,
@Id_producto int,
@Fecha_stock date,
@Hora_stock time(2),
@Cantidad_stock decimal(19,2),
@Observaciones_producto varchar(MAX)
AS
BEGIN
	INSERT INTO Stock_productos
	VALUES (@Id_producto, @Fecha_stock, @Hora_stock, 
	@Cantidad_stock, @Observaciones_producto)

	SET @Id_stock = SCOPE_IDENTITY();
END

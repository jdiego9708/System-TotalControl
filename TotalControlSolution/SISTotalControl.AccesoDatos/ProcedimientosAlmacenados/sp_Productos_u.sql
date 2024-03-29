CREATE OR ALTER PROC sp_Productos_u
@Id_producto int, 
@Id_tipo_producto int,
@Id_cobro int,
@Nombre_producto varchar(50),
@Precio_producto decimal(19, 2),
@Descripcion_producto varchar(200),
@Estado_producto varchar(50)
AS
BEGIN TRY

UPDATE Productos SET
Id_tipo_producto = @Id_tipo_producto, 
Id_cobro = @Id_cobro,
Nombre_producto = @Nombre_producto, 
Precio_producto = @Precio_producto,
Descripcion_producto = @Descripcion_producto, 
Estado_producto = @Estado_producto
WHERE Id_producto  = @Id_producto;

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
DELETE from cobros
DELETE from zonas
DELETE from ciudades
DELETE from paises

DELETE from usuarios


INSERT INTO Paises 
VALUES ('COLOMBIA', '', 'ACTIVO')

DECLARE @Id_pais int = SCOPE_IDENTITY();

INSERT INTO Ciudades 
VALUES (@Id_pais, 'MANIZALES', '', 'ACTIVO')

DECLARE @Id_ciudad int = SCOPE_IDENTITY();

INSERT INTO Zonas 
VALUES (@Id_ciudad, 'LA ESTRELLA', '', 'ACTIVO')

DECLARE @Id_zona int = SCOPE_IDENTITY();

INSERT INTO Usuarios
VALUES (GETDATE(), 'CHICO', 'JUAN DIEGO', 'DUQUE', '1053859229', 
'3229098696', 'jdiego9708@gmail.com', 'ADMINISTRADOR', 'ACTIVO')

DECLARE @Id_usuario int = SCOPE_IDENTITY();

INSERT INTO Cobros 
VALUES (@Id_usuario, @Id_zona, CONVERT(date,GETDATE()), CONVERT(time(2),GETDATE()),
0, 'ACTIVO', 'COBRO PRINCIPAL')
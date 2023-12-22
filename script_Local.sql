-- Si existe, elimina la base de datos existente
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'BD_CaldosAlex')
BEGIN
    ALTER DATABASE BD_CaldosAlex SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE BD_CaldosAlex;
END

-- Crea la base de datos
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'BD_CaldosAlex')
BEGIN
    CREATE DATABASE BD_CaldosAlex;
END


ALTER AUTHORIZATION ON DATABASE::BD_CaldosAlex TO sa
GO

USE BD_CaldosAlex;

	CREATE TABLE Usuarios
	(
		ID INT PRIMARY KEY,
		NombreUsuario NVARCHAR(255) NOT NULL,
		Contraseña NVARCHAR(255) NOT NULL,
		Logeado BIT NOT NULL DEFAULT 0
	);
	-- Crear la tabla Caja con columnas para sumatorias de ventas y estado
	CREATE TABLE Caja (
		idCaja INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
		FechaApertura DATETIME,
		FechaCierre DATETIME NULL, -- Puede ser NULL si la caja está abierta
		EstadoCaja BIT, -- 0 para "Cerrada", 1 para "Abierta"
		MontoInicial DECIMAL(10, 2),
		MontoFinal DECIMAL(10, 2) NULL,
		TotalVentasBoleta DECIMAL(10, 2) NULL,
		TotalVentasFactura DECIMAL(10, 2) NULL,
		TotalVentasNotaVenta DECIMAL(10, 2) NULL,
		TotalVentasYape DECIMAL(10, 2) NULL,
		TotalVentasPlin DECIMAL(10, 2) NULL,
		TotalVentasEfectivo DECIMAL(10, 2) NULL,
		TotalVentasTarjeta DECIMAL(10, 2) NULL,
		Gastos DECIMAL(10, 2) NULL,
		idUsuario INT NULL, -- Nueva columna para la clave foránea
		FOREIGN KEY (idUsuario) REFERENCES Usuarios(ID) -- Clave foránea que referencia la tabla Usuarios
	);

	SELECT * FROM Caja
	-- Tabla: TipoComponentes
	CREATE TABLE TipoComponentes (
		idTipoComponente int IDENTITY(1,1) NOT NULL PRIMARY KEY,
		descripcionTipoComponente nvarchar(max) NOT NULL
	);

	-- Tabla: CategoriaPlato
	CREATE TABLE CategoriaPlato (
		idCategoriaPlato int IDENTITY(1,1) NOT NULL PRIMARY KEY,
		nombreCategoria nvarchar(max) NOT NULL
	);

	-- Tabla: Componentes
	CREATE TABLE Componentes (
		idComponente int IDENTITY(1,1) NOT NULL PRIMARY KEY,
		descripcionComponente nvarchar(max) NOT NULL,
		precioComponente decimal(10, 2) NOT NULL,
		idTipoComponente int NOT NULL,
		idCategoriaPlato int NULL,
		CONSTRAINT FK_Componentes_TipoComponentes FOREIGN KEY (idTipoComponente) REFERENCES TipoComponentes(idTipoComponente),
		CONSTRAINT FK_Componentes_CategoriaPlato FOREIGN KEY (idCategoriaPlato) REFERENCES CategoriaPlato(idCategoriaPlato)
	);

	-- Tabla: Mesa
	CREATE TABLE Mesa (
		idMesa int IDENTITY(1,1) NOT NULL PRIMARY KEY,
		numeroMesa int NOT NULL,
		estadoOcupado bit NOT NULL
	);

	-- Tabla: MetodoPago
	CREATE TABLE MetodoPago (
		idMetodoPago int IDENTITY(1,1) NOT NULL PRIMARY KEY,
		nombreMetPag nvarchar(max) NOT NULL
	);

	-- Crear la tabla Pedido
	CREATE TABLE Pedido (
		idPedido int IDENTITY(1,1) NOT NULL PRIMARY KEY,
		FechaPedido datetime NOT NULL,
		idMesa int NOT NULL,
		pagado bit NOT NULL,
		FOREIGN KEY (idMesa) REFERENCES Mesa(idMesa)  -- Agregar clave foránea
	);

	-- Crear la tabla PedidoComponente
	CREATE TABLE PedidoComponente (
		idPedidoComponentes int IDENTITY(1,1) NOT NULL PRIMARY KEY,
		idPedido int NOT NULL,
		idTipoComponente int NOT NULL,
		idComponente int NOT NULL,
		Cantidad int NOT NULL,
		CONSTRAINT idTipoComponente_fk FOREIGN KEY (idTipoComponente) REFERENCES TipoComponentes(idTipoComponente),
		CONSTRAINT idComponente_fk FOREIGN KEY (idComponente) REFERENCES Componentes(idComponente),
		CONSTRAINT idPedido_fk FOREIGN KEY (idPedido) REFERENCES Pedido (idPedido)
	);

	--Modificacion
	ALTER TABLE PedidoComponente
	ADD importeComponente decimal(10, 2) NULL;

	CREATE TABLE CategoriaVenta(
		idCategoriaVenta int IDENTITY(1,1) NOT NULL PRIMARY KEY,
		nombreCategoria nvarchar(30) NOT NULL
	)
	-- Crear la tabla Venta
	CREATE TABLE Venta (
		idVenta int IDENTITY(1,1) NOT NULL PRIMARY KEY,
		FechaVenta datetime NOT NULL,
		idCategoríaVenta int NOT NULL,
		idPedido int NOT NULL,
		idMetodoPago int NOT NULL,
		NombreCliente nvarchar(max) NULL,
		ApellidoCliente nvarchar(max) NULL,
		DNICliente nvarchar(20) NULL,
		idUsuario int NULL,  -- Nueva columna para la clave foránea
		ImporteVenta decimal(10, 2) NULL,  -- Nueva columna: ImporteVenta
		CONSTRAINT FK_Venta_Pedido FOREIGN KEY (idPedido) REFERENCES Pedido(idPedido),
		CONSTRAINT FK_Venta_MetodoPago FOREIGN KEY (idMetodoPago) REFERENCES MetodoPago(idMetodoPago),
		CONSTRAINT FK_Venta_CategoriaVenta FOREIGN KEY (idCategoríaVenta) REFERENCES CategoriaVenta(idCategoriaVenta),
		CONSTRAINT FK_Venta_Usuarios FOREIGN KEY (idUsuario) REFERENCES Usuarios(ID)  -- Nueva clave foránea
	);
	
INSERT INTO Usuarios (ID ,NombreUsuario, Contraseña, Logeado)
VALUES (1, 'admin', '12345678', 0);

-- Insertar categorías de venta
INSERT INTO CategoriaVenta (nombreCategoria) VALUES
('Boleta'),
('Factura'),
('Nota de Venta');

-- Insertar tipos de componentes
INSERT INTO TipoComponentes (descripcionTipoComponente)
VALUES
('Plato'),
('Bebida'),
('Adicional');

-- Inserción de categorías de platos
INSERT INTO CategoriaPlato (nombreCategoria)
VALUES
('CALDOS'),
('POLLO A LA BRASA'),
('PLATOS A LA CARTA');

INSERT INTO Componentes (descripcionComponente, precioComponente, idTipoComponente, idCategoriaPlato)
VALUES
('CALDO DE POLLO', 11.00, 1, 1),
('CALDO DE GALLINA', 13.00, 1, 1),
('PRESA DE GALLINA', 8.00, 1, 1),
('PRESA DE POLLO', 6.00, 1, 1),
('CALDO ESPECIAL', 5.00, 1, 1),
('POLLO ENTERO', 65.00, 1, 2),
('UN OCTAVO DE POLLO', 12.00, 1, 2),
('PRESA UN OCTAVO', 6.00, 1, 2),
('UN CUARTO DE POLLO', 18.00, 1, 2),
('COMBO 1', 13.00, 1, 2),
('COMBO 2', 14.00, 1, 2),
('COMBO 3', 19.00, 1, 2),
('COMBO 4', 20.00, 1, 2),
('LOMO SALTADO', 19.00, 1, 3),
('LOMO MONTADO', 20.00, 1, 3),
('LOMO POBRE', 21.00, 1, 3),
('TALLARIN DE CARNE', 18.00, 1, 3),
('CHAUFA DE CARNE', 18.00, 1, 3),
('SALTADO DE POLLO', 17.00, 1, 3),
('SALTADO MONTADO', 18.00, 1, 3),
('SALTADO POBRE', 19.00, 1, 3),
('TALLARIN DE POLLO', 17.00, 1, 3),
('CHAUFA DE POLLO', 17.00, 1, 3),
('POLLO FRITO', 16.00, 1, 3),
('FRITO MONTADO', 17.00, 1, 3),
('FRITO POBRE', 18.00, 1, 3),
('POLLO A LA PLANCHA', 16.00, 1, 3),
('POLLO BROSTER', 16.00, 1, 3),
('BROSTER CON CHAUFA', 17.00, 1, 3),
('ARROZ A LA CUBANA', 14.00, 1, 3),
('CHICHARRON DE POLLO', 16.00, 1, 3);

-- Inserción de bebidas en la tabla Componentes
INSERT INTO Componentes (descripcionComponente, precioComponente, idTipoComponente)
VALUES
('AGUA', 3.00, 2),
('SPORADE', 4.00, 2),
('G.PERSONAL', 2.50, 2),
('G. PEQUEÑA', 2.00, 2),
('G.MEDIO LITRO', 4.50, 2),
('PEPSI MEDIO LITRO', 3.00, 2),
('GORDITA', 5.00, 2),
('G.LITRO', 7.00, 2),
('G. LITRO Y MEDIO', 9.00, 2),
('G. PEPSI UNO Y MEDIO', 7.00, 2),
('GASEOSA 2.25 LT', 11.00, 2),
('G. PEPSI 2 LTS', 8.00, 2);

-- Inserción de adicionales en la tabla Componentes
INSERT INTO Componentes (descripcionComponente, precioComponente, idTipoComponente)
VALUES
('PAPA EN ENSALADA CHAUFA', 5.00, 3),
('MEDIO DE CHICHA', 6.00, 3),
('JARRA DE CHICHA', 12.00, 3),
('TAPER', 1.00, 3),
('PAN', 1.00, 3),
('HUEVO', 1.50, 3);
select * from Componentes


-- Insertar métodos de pago en la tabla MetodoPago
INSERT INTO MetodoPago (nombreMetPag)
VALUES
('YAPE'),
('PLIN'),
('EFECTIVO'),
('TARJETA');

-- Insertar mesas en la tabla Mesa
INSERT INTO Mesa (numeroMesa, estadoOcupado)
VALUES
(1, 0),  -- Mesa 1, no ocupada
(2, 0),  -- Mesa 2, no ocupada
(3, 0),  -- Mesa 3, no ocupada
(4, 0),  -- Mesa 4, no ocupada
(5, 0),  -- Mesa 5, no ocupada
(6, 0),  -- Mesa 6, no ocupada
(7, 0),  -- Mesa 7, no ocupada
(8, 0),  -- Mesa 8, no ocupada
(9, 0),  -- Mesa 9, no ocupada
(10, 0),  -- Mesa 10, no ocupada
(11, 0),  -- Mesa 11, no ocupada
(12, 0),  -- Mesa 12, no ocupada
(13, 0),  -- Mesa 13, no ocupada
(14, 0),  -- Mesa 14, no ocupada
(15, 0),  -- Mesa 14, no ocupada
(16, 0),  -- Mesa 14, no ocupada
(17, 0),  -- Mesa 14, no ocupada
(18, 0),  -- Mesa 14, no ocupada
(19, 0),  
(20, 0),
(21, 0); 


--Eliminamos el trigger si ya existe
IF OBJECT_ID('ActualizarPedidoMesa', 'TR') IS NOT NULL
DROP TRIGGER ActualizarPedidoMesa;
GO

-- Creamos el trigger en la tabla Venta
CREATE OR ALTER TRIGGER ActualizarPedidoMesa
ON Venta
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @idPedido INT;

    -- Obtener el idPedido de la última venta insertada
    SELECT @idPedido = idPedido
    FROM inserted;

    -- Actualizar el campo pagado de la tabla Pedido a 1
    UPDATE Pedido
    SET pagado = 1
    WHERE idPedido = @idPedido;

    -- Asignar la mesa número 20 al pedido pagado
    UPDATE Pedido
    SET idMesa = 20
    WHERE idPedido = @idPedido;
END;


-- Crear el trigger
CREATE OR ALTER TRIGGER ActualizarFechaCierre
ON Caja
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Verificar si el estado de la caja cambió a "Cerrada"
    IF (UPDATE(EstadoCaja) AND EXISTS (SELECT 1 FROM inserted WHERE EstadoCaja = 0))
    BEGIN
        -- Actualizar la columna FechaCierre con la fecha actual
        UPDATE Caja
        SET FechaCierre = GETDATE(),
            MontoFinal = CASE WHEN inserted.MontoFinal = 0 THEN inserted.MontoInicial ELSE inserted.MontoFinal END
        FROM Caja
        INNER JOIN inserted ON Caja.idCaja = inserted.idCaja
        WHERE inserted.EstadoCaja = 0;
    END
END;




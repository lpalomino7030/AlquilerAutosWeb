

CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Usuario VARCHAR(50) NOT NULL,
    Password VARCHAR(255) NOT NULL,
    Rol VARCHAR(20) NOT NULL,
    Estado BIT NOT NULL
);
GO
INSERT INTO Usuarios(Usuario,Password,Rol,Estado ) VALUES ('LUISP','LOREM123','ADM',1);

---------------
CREATE TABLE Clientes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    DNI VARCHAR(15) NOT NULL,
    Nombres VARCHAR(100) NOT NULL,
    Apellidos VARCHAR(100) NOT NULL,
    Telefono VARCHAR(20),
    Email VARCHAR(100),
    Estado BIT NOT NULL
);
GO

----

CREATE TABLE Autos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Placa VARCHAR(20) NOT NULL,
    Marca VARCHAR(50) NOT NULL,
    Modelo VARCHAR(50) NOT NULL,
    Anio INT NOT NULL,
    PrecioDia DECIMAL(10,2) NOT NULL,
    EstadoAuto VARCHAR(20) NOT NULL, 
    Estado BIT NOT NULL

);
GO

----

CREATE TABLE Alquileres (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ClienteId INT NOT NULL,
    AutoId INT NOT NULL,
    FechaInicio DATE NOT NULL,
    FechaFin DATE NOT NULL,
    Total DECIMAL(10,2) NOT NULL,
    EstadoAlquiler VARCHAR(20) NOT NULL, -- Activo / Finalizado
    Estado BIT NOT NULL,

    CONSTRAINT FK_Alquiler_Cliente FOREIGN KEY (ClienteId)
        REFERENCES Clientes(Id),

    CONSTRAINT FK_Alquiler_Auto FOREIGN KEY (AutoId)
        REFERENCES Autos(Id)
);
GO


--Insert de carros

INSERT INTO Autos (Placa, Marca, Modelo, Anio, PrecioDia, EstadoAuto, Estado)
VALUES
('BR-101', 'Toyota', 'Corolla Hybrid', 2024, 160, 'Disponible', 1),
('BR-102', 'Toyota', 'RAV4 Hybrid', 2024, 240, 'Disponible', 1),
('BR-103', 'Hyundai', 'Elantra', 2023, 155, 'Disponible', 1),
('BR-104', 'Kia', 'Sportage', 2024, 220, 'Disponible', 1),
('BR-105', 'Mazda', 'CX-30', 2024, 210, 'Disponible', 1),
('BR-106', 'Nissan', 'X-Trail', 2023, 225, 'Disponible', 1),
('BR-107', 'Chevrolet', 'Tracker Turbo', 2023, 195, 'Disponible', 1),
('BR-108', 'Volkswagen', 'Taos', 2024, 230, 'Disponible', 1),
('BR-109', 'BMW', 'X1', 2024, 360, 'Disponible', 1),
('BR-110', 'Audi', 'Q3', 2024, 370, 'Disponible', 1);




-- PROCEDIMIENTO ALMACENADOS


CREATE PROCEDURE sp_listar_alquileres_activos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        A.Id,
        A.FechaInicio,
        A.FechaFin,
        A.Total,
        A.Estado,
        A.EstadoAlquiler,
        C.Nombres + ' ' + C.Apellidos AS ClienteNombre,
        AU.Placa + ' - ' + AU.Marca + ' ' + AU.Modelo AS AutoNombre
    FROM Alquileres A
    INNER JOIN Clientes C ON A.ClienteId = C.Id
    INNER JOIN Autos AU ON A.AutoId = AU.Id
    WHERE A.Estado = 1;
END
GO

----------------------------------------
CREATE PROCEDURE sp_insertar_alquiler
    @ClienteId INT,
    @AutoId INT,
    @FechaInicio DATE,
    @FechaFin DATE,
    @Total DECIMAL(10,2)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Alquileres
    (
        ClienteId,
        AutoId,
        FechaInicio,
        FechaFin,
        Total,
        EstadoAlquiler,
        Estado
    )
    VALUES
    (
        @ClienteId,
        @AutoId,
        @FechaInicio,
        @FechaFin,
        @Total,
        'Activo',
        1
    );
END;
GO

--------------------------------------------

CREATE PROCEDURE sp_obtener_alquiler_por_id
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        ClienteId,
        AutoId,
        FechaInicio,
        FechaFin,
        Total,
        EstadoAlquiler,
        Estado
    FROM Alquileres
    WHERE Id = @Id
      AND Estado = 1;
END;
GO

CREATE PROCEDURE sp_obtener_alquiler_id
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        AutoId,
        EstadoAlquiler
    FROM Alquileres
    WHERE Id = @Id
      AND Estado = 1;
END;
GO

------------------------------------

CREATE PROCEDURE sp_finalizar_alquiler
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Alquileres
    SET EstadoAlquiler = 'Finalizado'
    WHERE Id = @Id
      AND Estado = 1;
END;
GO

-----------------------------------
CREATE PROCEDURE sp_alquileres_activos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT COUNT(*) 
    FROM Alquileres
    WHERE Estado = 1
      AND EstadoAlquiler = 'Activo';
END
GO

-----------------------------------
CREATE PROCEDURE sp_listar_autos_activos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        Placa,
        Marca,
        Modelo,
        Anio,
        PrecioDia,
        EstadoAuto,
        Estado
    FROM Autos
    WHERE Estado = 1;
END
GO

----------------------------

CREATE PROCEDURE sp_insertar_auto
    @placa VARCHAR(20),
    @marca VARCHAR(50),
    @modelo VARCHAR(50),
    @anio INT,
    @precio DECIMAL(10,2)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Autos
    (
        Placa,
        Marca,
        Modelo,
        Anio,
        PrecioDia,
        EstadoAuto,
        Estado
    )
    VALUES
    (
        @placa,
        @marca,
        @modelo,
        @anio,
        @precio,
        'Disponible',
        1
    );
END;
GO

-----------------------------

CREATE PROCEDURE sp_obtener_auto_por_id
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        Placa,
        Marca,
        Modelo,
        Anio,
        PrecioDia,
        EstadoAuto,
        Estado
    FROM Autos
    WHERE Id = @Id;
END
GO

----------------------------------

CREATE PROCEDURE sp_actualizar_auto
    @id INT,
    @placa VARCHAR(20),
    @marca VARCHAR(50),
    @modelo VARCHAR(50),
    @anio INT,
    @precio DECIMAL(10,2)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Autos
    SET
        Placa = @placa,
        Marca = @marca,
        Modelo = @modelo,
        Anio = @anio,
        PrecioDia = @precio
    WHERE Id = @id;
END;
GO

------------------------------------

CREATE PROCEDURE sp_autos_listar
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        Placa,
        Marca,
        Modelo,
        Anio,
        PrecioDia,
        EstadoAuto,
        Estado
    FROM Autos
    WHERE Estado = 1;
END
GO

--------------------------------

CREATE PROCEDURE sp_autos_eliminar
(
    @Id INT
)
AS
BEGIN
    UPDATE Autos
    SET Estado = 0
    WHERE Id = @Id;
END
GO
---------------------------------------
CREATE PROCEDURE sp_listar_autos_disponibles
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        Placa,
        Marca,
        Modelo,
        PrecioDia
    FROM Autos
    WHERE Estado = 1
      AND EstadoAuto = 'Disponible';
END;
GO
--------------------------------

CREATE PROCEDURE sp_obtener_precio_auto
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        PrecioDia
    FROM Autos
    WHERE Id = @Id
      AND Estado = 1;
END;
GO

-------------------------------

CREATE PROCEDURE sp_autos_marcar_alquilado
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Autos
    SET EstadoAuto = 'Alquilado'
    WHERE Id = @Id
      AND Estado = 1;
END
GO

--------------------------------

CREATE PROCEDURE sp_reporte_alquileres
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        C.Nombres + ' ' + C.Apellidos AS Cliente,
        AU.Placa AS Placa,
        AU.Marca + ' ' + AU.Modelo AS Auto,
        A.FechaInicio,
        A.FechaFin,
        A.Total,
        A.EstadoAlquiler
    FROM Alquileres A
    INNER JOIN Clientes C ON A.ClienteId = C.Id
    INNER JOIN Autos AU ON A.AutoId = AU.Id
    WHERE A.Estado = 1
    ORDER BY A.FechaInicio DESC;
END
GO

------------------------------------

CREATE PROCEDURE sp_alquileres_por_mes
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        MONTH(FechaInicio) AS Mes,
        COUNT(*) AS Total
    FROM Alquileres
    WHERE Estado = 1
    GROUP BY MONTH(FechaInicio)
    ORDER BY Mes;
END









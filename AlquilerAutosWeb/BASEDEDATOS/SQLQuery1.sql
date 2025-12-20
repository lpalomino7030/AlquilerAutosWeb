

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
GO



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
GO
----------------------------
CREATE PROCEDURE sp_marcar_auto_disponible
    @id INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Autos
    SET EstadoAuto = 'Disponible'
    WHERE Id = @id;
END
GO


----------------------------
CREATE PROCEDURE sp_total_autos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT COUNT(*) 
    FROM Autos
    WHERE Estado = 1;
END
GO


------------------------------

CREATE PROCEDURE sp_autos_disponibles
AS
BEGIN
    SET NOCOUNT ON;

    SELECT COUNT(*)
    FROM Autos
    WHERE Estado = 1
      AND EstadoAuto = 'Disponible';
END
GO

------------------------------------------

CREATE PROCEDURE sp_obtener_estado_autos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        EstadoAuto,
        COUNT(*) AS Total
    FROM Autos
    WHERE Estado = 1
    GROUP BY EstadoAuto;
END
GO
------------------------------------------

CREATE PROCEDURE sp_buscar_autos
    @Texto VARCHAR(100)
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
    WHERE Estado = 1
      AND (
            Placa  LIKE '%' + @Texto + '%' OR
            Marca  LIKE '%' + @Texto + '%' OR
            Modelo LIKE '%' + @Texto + '%'
          );
END
GO

-----------------------------------------

CREATE PROCEDURE sp_listar_clientes
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        DNI,
        Nombres,
        Apellidos,
        Telefono,
        Email,
        Estado
    FROM Clientes
    
END
GO


-----------------------------------------

CREATE PROCEDURE sp_insertar_cliente
    @DNI        VARCHAR(20),
    @Nombres    VARCHAR(100),
    @Apellidos  VARCHAR(100),
    @Telefono   VARCHAR(20),
    @Email      VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Clientes
        (DNI, Nombres, Apellidos, Telefono, Email, Estado)
    VALUES
        (@DNI, @Nombres, @Apellidos, @Telefono, @Email, 1);
END
GO

-----------------------------------------

CREATE PROCEDURE sp_obtener_cliente_por_id
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        DNI,
        Nombres,
        Apellidos,
        Telefono,
        Email,
        Estado
    FROM Clientes
    WHERE Id = @Id;
END
GO

-----------------------------------------

CREATE PROCEDURE sp_actualizar_cliente
    @Id         INT,
    @DNI        VARCHAR(20),
    @Nombres    VARCHAR(100),
    @Apellidos  VARCHAR(100),
    @Telefono   VARCHAR(20),
    @Email      VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Clientes
    SET
        DNI = @DNI,
        Nombres = @Nombres,
        Apellidos = @Apellidos,
        Telefono = @Telefono,
        Email = @Email
    WHERE Id = @Id;
END
GO

-----------------------------------------

CREATE PROCEDURE sp_eliminar_cliente
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Clientes
    SET Estado = 0
    WHERE Id = @Id;
END
GO


-----------------------------------------

CREATE PROCEDURE sp_total_clientes_activos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT COUNT(*) 
    FROM Clientes
    WHERE Estado = 1;
END
GO

-----------------------------------------

CREATE PROCEDURE sp_buscar_clientes
    @Texto VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        DNI,
        Nombres,
        Apellidos,
        Estado
    FROM Clientes
    WHERE Estado = 1
      AND (
            Nombres   LIKE '%' + @Texto + '%'
         OR Apellidos LIKE '%' + @Texto + '%'
         OR DNI       LIKE '%' + @Texto + '%'
      );
END
GO

-----------------------------------------

CREATE PROCEDURE sp_listar_clientes_activos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        Nombres,
        Apellidos
    FROM Clientes
    WHERE Estado = 1;
END
GO

-----------------------------------------

CREATE PROCEDURE sp_listar_usuarios
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        Id,
        Usuario,
        Rol,
        Estado
    FROM Usuarios;
END
GO


-----------------------------------------

CREATE PROCEDURE sp_updateUsuario
    @Id INT,
    @Usuario VARCHAR(50),
    @Password VARCHAR(255),
    @Rol VARCHAR(20),
    @Estado BIT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Usuarios
    SET
        Usuario = @Usuario,
        Password = @Password,
        Rol = @Rol,
        Estado = @Estado
    WHERE Id = @Id;
END
GO
    
-----------------------------------------

CREATE PROCEDURE sp_InsertarUsuario
    @UsuarioNombre VARCHAR(50),
    @Password VARCHAR(255),
    @Rol VARCHAR(20),
    @Estado BIT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Usuarios (Usuario, Password, Rol, Estado)
    VALUES (@UsuarioNombre, @Password, @Rol, @Estado);
END
GO




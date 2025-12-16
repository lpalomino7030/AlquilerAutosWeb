

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




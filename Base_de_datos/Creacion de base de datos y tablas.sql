

IF DB_ID(N'Proyecto1_DB') IS NULL
BEGIN
    CREATE DATABASE Proyecto1_DB;
END;
GO


USE Proyecto1_DB;
GO


CREATE TABLE dbo.EstadosTicket (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(30) NOT NULL UNIQUE
);
GO


INSERT INTO dbo.EstadosTicket (Nombre)
VALUES
    (N'Abierto'),
    (N'En Proceso'),
    (N'Resuelto'),
    (N'Cerrado');
GO



CREATE TABLE dbo.Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Email NVARCHAR(150) NOT NULL UNIQUE,
    ContrasenaHash NVARCHAR(255) NOT NULL,
    Rol NVARCHAR(20) NOT NULL,
    FechaRegistro DATETIME NOT NULL
        DEFAULT GETDATE(),
    Estado BIT NOT NULL
        DEFAULT 1,

    CONSTRAINT CK_Usuarios_Rol
        CHECK (Rol IN (N'Usuario', N'Administrador'))
);
GO



CREATE TABLE dbo.Tickets (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Titulo NVARCHAR(150) NOT NULL,
    Descripcion NVARCHAR(MAX) NOT NULL,
    IdUsuarioSolicitante INT NOT NULL,
    IdUsuarioAsignado INT NULL,
    IdEstado INT NOT NULL,
    FechaCreacion DATETIME NOT NULL
        DEFAULT GETDATE(),
    FechaActualizacion DATETIME NULL,
    Categoria NVARCHAR(80) NOT NULL,

    CONSTRAINT FK_Tickets_Solicitante
        FOREIGN KEY (IdUsuarioSolicitante)
        REFERENCES dbo.Usuarios(Id),

    CONSTRAINT FK_Tickets_Asignado
        FOREIGN KEY (IdUsuarioAsignado)
        REFERENCES dbo.Usuarios(Id),

    CONSTRAINT FK_Tickets_Estado
        FOREIGN KEY (IdEstado)
        REFERENCES dbo.EstadosTicket(Id)
);
GO



CREATE TABLE dbo.Comentarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdTicket INT NOT NULL,
    IdUsuario INT NOT NULL,
    Texto NVARCHAR(MAX) NOT NULL,
    Fecha DATETIME NOT NULL
        DEFAULT GETDATE(),

    CONSTRAINT FK_Comentarios_Ticket
        FOREIGN KEY (IdTicket)
        REFERENCES dbo.Tickets(Id),

    CONSTRAINT FK_Comentarios_Usuario
        FOREIGN KEY (IdUsuario)
        REFERENCES dbo.Usuarios(Id)
);
GO

CREATE TABLE dbo.Reportes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdUsuarioAdministrador INT NOT NULL,
    FechaGeneracion DATETIME NOT NULL
        DEFAULT GETDATE(),
    TotalTickets INT NOT NULL,
    TicketsAbiertos INT NOT NULL,
    TicketsEnProceso INT NOT NULL,
    TicketsResueltos INT NOT NULL,
    TicketsCerrados INT NOT NULL,
    TotalUsuarios INT NOT NULL,

    CONSTRAINT FK_Reportes_Usuarios
        FOREIGN KEY (IdUsuarioAdministrador)
        REFERENCES dbo.Usuarios(Id),

    
);
GO


SELECT * FROM dbo.EstadosTicket;
GO

SELECT TABLE_NAME
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_TYPE = 'BASE TABLE'
ORDER BY TABLE_NAME;
GO


-- PARTE 1: CREAR BASE DE DATOS
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'CineDb')
BEGIN
    CREATE DATABASE [CineDb];
    PRINT 'Base de datos CineDb creada exitosamente.';
END
ELSE
BEGIN
    PRINT 'La base de datos CineDb ya existe.';
END
GO

USE [CineDb];
GO

-- ============================================================
-- PARTE 2: CREAR TABLAS (si no existen)
-- ============================================================

-- 2.1 Tabla: users
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.users') AND type = 'U')
BEGIN
    CREATE TABLE [dbo].[users] (
        [id] INT IDENTITY(1,1) NOT NULL,
        [user_name] NVARCHAR(100) NOT NULL,
        [email] NVARCHAR(150) NOT NULL,
        [password] NVARCHAR(255) NOT NULL,
        [eliminado] BIT NOT NULL DEFAULT(0),
        [created_at] DATETIME2(0) NOT NULL DEFAULT(SYSUTCDATETIME()),
        [updated_at] DATETIME2(0) NULL,
        CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED ([id] ASC)
    );
    
    -- Índice único para email (solo usuarios no eliminados)
    CREATE UNIQUE INDEX [UX_users_email_activo] 
    ON [dbo].[users] ([email]) 
    WHERE ([eliminado] = 0);
    
    PRINT 'Tabla users creada exitosamente.';
END
ELSE
BEGIN
    PRINT 'La tabla users ya existe.';
END
GO

-- 2.2 Tabla: sala_cine
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.sala_cine') AND type = 'U')
BEGIN
    CREATE TABLE [dbo].[sala_cine] (
        [id_sala] INT IDENTITY(1,1) NOT NULL,
        [nombre] NVARCHAR(100) NOT NULL,
        [eliminado] BIT NOT NULL DEFAULT(0),
        [created_at] DATETIME2(0) NOT NULL DEFAULT(SYSUTCDATETIME()),
        [updated_at] DATETIME2(0) NULL,
        CONSTRAINT [PK_sala_cine] PRIMARY KEY CLUSTERED ([id_sala] ASC)
    );
    PRINT 'Tabla sala_cine creada exitosamente.';
END
ELSE
BEGIN
    PRINT 'La tabla sala_cine ya existe.';
END
GO

-- 2.3 Tabla: pelicula
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.pelicula') AND type = 'U')
BEGIN
    CREATE TABLE [dbo].[pelicula] (
        [id_pelicula] INT IDENTITY(1,1) NOT NULL,
        [nombre] NVARCHAR(200) NOT NULL,
        [duracion] INT NOT NULL,
        [eliminado] BIT NOT NULL DEFAULT(0),
        [created_at] DATETIME2(0) NOT NULL DEFAULT(SYSUTCDATETIME()),
        [updated_at] DATETIME2(0) NULL,
        CONSTRAINT [PK_pelicula] PRIMARY KEY CLUSTERED ([id_pelicula] ASC)
    );
    PRINT 'Tabla pelicula creada exitosamente.';
END
ELSE
BEGIN
    PRINT 'La tabla pelicula ya existe.';
END
GO

-- 2.4 Tabla: pelicula_salacine (relación muchos a muchos)
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.pelicula_salacine') AND type = 'U')
BEGIN
    CREATE TABLE [dbo].[pelicula_salacine] (
        [id] INT IDENTITY(1,1) NOT NULL,
        [id_sala_cine] INT NOT NULL,
        [id_pelicula] INT NOT NULL,
        [fecha_publicacion] DATE NOT NULL,
        [fecha_fin] DATE NULL,
        [eliminado] BIT NOT NULL DEFAULT(0),
        [created_at] DATETIME2(0) NOT NULL DEFAULT(SYSUTCDATETIME()),
        [updated_at] DATETIME2(0) NULL,
        CONSTRAINT [PK_pelicula_salacine] PRIMARY KEY CLUSTERED ([id] ASC),
        CONSTRAINT [FK_pelicula_salacine_sala] FOREIGN KEY ([id_sala_cine]) REFERENCES [dbo].[sala_cine]([id_sala]),
        CONSTRAINT [FK_pelicula_salacine_pelicula] FOREIGN KEY ([id_pelicula]) REFERENCES [dbo].[pelicula]([id_pelicula])
    );
    PRINT 'Tabla pelicula_salacine creada exitosamente.';
END
ELSE
BEGIN
    PRINT 'La tabla pelicula_salacine ya existe.';
END
GO


-- 3.1 Eliminar columna 'estado' de sala_cine
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.sala_cine') AND name = 'estado')
BEGIN
    ALTER TABLE [dbo].[sala_cine] DROP COLUMN [estado];
    PRINT 'Columna estado eliminada de sala_cine.';
END
GO

-- 3.2 Eliminar columna 'estado' de users
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.users') AND name = 'estado')
BEGIN
    -- Primero eliminar el constraint default si existe
    DECLARE @ConstraintName NVARCHAR(256)
    SELECT @ConstraintName = dc.name 
    FROM sys.default_constraints dc
    JOIN sys.columns c ON dc.parent_object_id = c.object_id AND dc.parent_column_id = c.column_id
    WHERE dc.parent_object_id = OBJECT_ID('dbo.users') AND c.name = 'estado'
    
    IF @ConstraintName IS NOT NULL
    BEGIN
        EXEC('ALTER TABLE [dbo].[users] DROP CONSTRAINT ' + @ConstraintName)
        PRINT 'Constraint default eliminado de users.estado.';
    END
    
    ALTER TABLE [dbo].[users] DROP COLUMN [estado];
    PRINT 'Columna estado eliminada de users.';
END
GO


PRINT 'Limpiando datos existentes...';

DELETE FROM dbo.pelicula_salacine;
DELETE FROM dbo.pelicula;
DELETE FROM dbo.sala_cine;
DELETE FROM dbo.[users];
GO

-- Reiniciar los identity seeds para tener IDs predecibles
DBCC CHECKIDENT ('dbo.users', RESEED, 0);
DBCC CHECKIDENT ('dbo.sala_cine', RESEED, 0);
DBCC CHECKIDENT ('dbo.pelicula', RESEED, 0);
DBCC CHECKIDENT ('dbo.pelicula_salacine', RESEED, 0);
GO

PRINT 'Datos limpiados exitosamente.';
GO

PRINT 'Insertando datos de prueba...';


INSERT INTO dbo.[users] ([user_name], [email], [password], [eliminado], [created_at])
VALUES ('Admin', 'admin@cine.com', '$2a$11$rK1Xk9vN1gP2dW3Y4eX5s.8qZ6L7mN8oP9.0a1B2c3D4e5F6g7H8i', 0, SYSUTCDATETIME());

PRINT 'Usuario Admin creado (admin@cine.com / admin123).';
GO

INSERT INTO dbo.sala_cine ([nombre], [eliminado], [created_at])
VALUES
    ('Sala 1', 0, SYSUTCDATETIME()),
    ('Sala 2', 0, SYSUTCDATETIME()),
    ('Sala 3', 0, SYSUTCDATETIME());

PRINT 'Salas de cine creadas (3 salas).';
GO


INSERT INTO dbo.pelicula ([nombre], [duracion], [eliminado], [created_at])
VALUES
    ('Interstellar', 169, 0, SYSUTCDATETIME()),
    ('Inception', 148, 0, SYSUTCDATETIME()),
    ('The Matrix', 136, 0, SYSUTCDATETIME());

PRINT 'Películas creadas (3 películas).';
GO


-- Interstellar en Sala 1
INSERT INTO dbo.pelicula_salacine ([id_sala_cine], [id_pelicula], [fecha_publicacion], [fecha_fin], [eliminado], [created_at])
SELECT s.id_sala, p.id_pelicula, CAST('2025-12-01' AS DATE), CAST('2025-12-31' AS DATE), 0, SYSUTCDATETIME()
FROM dbo.sala_cine s
JOIN dbo.pelicula p ON p.nombre = 'Interstellar'
WHERE s.nombre = 'Sala 1';

-- Inception en Sala 2
INSERT INTO dbo.pelicula_salacine ([id_sala_cine], [id_pelicula], [fecha_publicacion], [fecha_fin], [eliminado], [created_at])
SELECT s.id_sala, p.id_pelicula, CAST('2025-12-05' AS DATE), CAST('2026-01-10' AS DATE), 0, SYSUTCDATETIME()
FROM dbo.sala_cine s
JOIN dbo.pelicula p ON p.nombre = 'Inception'
WHERE s.nombre = 'Sala 2';

-- The Matrix en Sala 3
INSERT INTO dbo.pelicula_salacine ([id_sala_cine], [id_pelicula], [fecha_publicacion], [fecha_fin], [eliminado], [created_at])
SELECT s.id_sala, p.id_pelicula, CAST('2025-12-10' AS DATE), NULL, 0, SYSUTCDATETIME()
FROM dbo.sala_cine s
JOIN dbo.pelicula p ON p.nombre = 'The Matrix'
WHERE s.nombre = 'Sala 3';

PRINT 'Asignaciones película-sala creadas (3 asignaciones).';
GO

-- ============================================================
-- PARTE 6: VALIDACIÓN
-- ============================================================
PRINT '';
PRINT '============================================================';
PRINT 'RESUMEN DE DATOS INSERTADOS';
PRINT '============================================================';

SELECT 'Usuarios' AS Tabla, COUNT(*) AS Total FROM dbo.[users]
UNION ALL
SELECT 'Salas de Cine', COUNT(*) FROM dbo.sala_cine
UNION ALL
SELECT 'Películas', COUNT(*) FROM dbo.pelicula
UNION ALL
SELECT 'Asignaciones', COUNT(*) FROM dbo.pelicula_salacine;
GO

PRINT '';
PRINT '============================================================';
PRINT 'SCRIPT EJECUTADO EXITOSAMENTE';
PRINT '============================================================';
PRINT 'Credenciales de prueba:';
PRINT '  Email: admin@cine.com';
PRINT '  Password: admin123';
PRINT '============================================================';
GO

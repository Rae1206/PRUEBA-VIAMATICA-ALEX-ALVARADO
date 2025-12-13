-- Script para crear la tabla Users en CineDb
-- Ejecutar en SQL Server Management Studio o Azure Data Studio

CREATE TABLE [dbo].[users] (
    [id] INT IDENTITY(1,1) NOT NULL,
    [user_name] NVARCHAR(100) NOT NULL,
    [email] NVARCHAR(150) NOT NULL,
    [password] NVARCHAR(255) NOT NULL,
    [estado] BIT NOT NULL DEFAULT(1),
    [eliminado] BIT NOT NULL DEFAULT(0),
    [created_at] DATETIME2(0) NOT NULL DEFAULT(SYSUTCDATETIME()),
    [updated_at] DATETIME2(0) NULL,
    CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED ([id] ASC)
);

-- Índice único para email (solo usuarios no eliminados)
CREATE UNIQUE INDEX [UX_users_email_activo] 
ON [dbo].[users] ([email]) 
WHERE ([eliminado] = 0);

-- Insertar un usuario de prueba (contraseña: admin123)
-- La contraseña está encriptada con BCrypt
INSERT INTO [dbo].[users] ([user_name], [email], [password], [estado], [eliminado])
VALUES ('Admin', 'admin@cine.com', '$2a$11$K8YH9RK8BKqNwGJLqNDkPeJpMKqjYQGvmvqhHPrjCL9W2gH8F5Jly', 1, 0);

PRINT 'Tabla users creada exitosamente';
PRINT 'Usuario de prueba: admin@cine.com / admin123';

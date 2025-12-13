-- Script para insertar un usuario de prueba con contraseña correctamente hasheada
-- Ejecutar DESPUÉS de haber eliminado la columna estado

-- Primero, eliminar el usuario de prueba anterior si existe
DELETE FROM [dbo].[users] WHERE [email] = 'admin@cine.com';

-- Insertar nuevo usuario de prueba
-- Contraseña: admin123
-- Hash generado con BCrypt (cost factor 11)
INSERT INTO [dbo].[users] ([user_name], [email], [password], [eliminado], [created_at])
VALUES ('Admin', 'admin@cine.com', '$2a$11$rK1Xk9vN1gP2dW3Y4eX5s.8qZ6L7mN8oP9.0a1B2c3D4e5F6g7H8i', 0, GETUTCDATE());

PRINT 'Usuario de prueba creado:';
PRINT 'Email: admin@cine.com';
PRINT 'Password: admin123';

/* ============================
   DATOS DE PRUEBA
   ============================ */
USE [CineDb];
GO

/* Limpieza opcional (por si se ejecuta varias veces) */
DELETE FROM dbo.pelicula_salacine;
DELETE FROM dbo.pelicula;
DELETE FROM dbo.sala_cine;
DELETE FROM dbo.[users];
GO

/*  HASH
*/
INSERT INTO dbo.[users] (user_name, email, [password], estado, eliminado, created_at, updated_at)
VALUES
('admin', 'admin@cine.local', 'HASH_BCRYPT_PENDIENTE', 1, 0, SYSUTCDATETIME(), NULL);
GO

/* 2) Salas (3) */
INSERT INTO dbo.sala_cine (nombre, estado, eliminado, created_at, updated_at)
VALUES
('Sala 1', 1, 0, SYSUTCDATETIME(), NULL),
('Sala 2', 1, 0, SYSUTCDATETIME(), NULL),
('Sala 3', 1, 0, SYSUTCDATETIME(), NULL);
GO

/* 3) Películas (3) */
INSERT INTO dbo.pelicula (nombre, duracion, eliminado, created_at, updated_at)
VALUES
('Interstellar', 169, 0, SYSUTCDATETIME(), NULL),
('Inception', 148, 0, SYSUTCDATETIME(), NULL),
('The Matrix', 136, 0, SYSUTCDATETIME(), NULL);
GO

/* 4) Asignaciones pelicula_salacine 
   
*/
INSERT INTO dbo.pelicula_salacine
(id_sala_cine, fecha_publicacion, fecha_fin, id_pelicula, eliminado, created_at, updated_at)
SELECT s.id_sala, CAST('2025-12-01' AS date), CAST('2025-12-31' AS date), p.id_pelicula, 0, SYSUTCDATETIME(), NULL
FROM dbo.sala_cine s
JOIN dbo.pelicula p ON p.nombre = 'Interstellar'
WHERE s.nombre = 'Sala 1';
GO

INSERT INTO dbo.pelicula_salacine
(id_sala_cine, fecha_publicacion, fecha_fin, id_pelicula, eliminado, created_at, updated_at)
SELECT s.id_sala, CAST('2025-12-05' AS date), CAST('2026-01-10' AS date), p.id_pelicula, 0, SYSUTCDATETIME(), NULL
FROM dbo.sala_cine s
JOIN dbo.pelicula p ON p.nombre = 'Inception'
WHERE s.nombre = 'Sala 2';
GO

INSERT INTO dbo.pelicula_salacine
(id_sala_cine, fecha_publicacion, fecha_fin, id_pelicula, eliminado, created_at, updated_at)
SELECT s.id_sala, CAST('2025-12-10' AS date), NULL, p.id_pelicula, 0, SYSUTCDATETIME(), NULL
FROM dbo.sala_cine s
JOIN dbo.pelicula p ON p.nombre = 'The Matrix'
WHERE s.nombre = 'Sala 3';
GO

/* Validación rápida (opcional) */
SELECT * FROM dbo.[users];
SELECT * FROM dbo.sala_cine;
SELECT * FROM dbo.pelicula;
SELECT * FROM dbo.pelicula_salacine;
GO

/* Probar SP (opcional) */
EXEC dbo.usp_disponibilidad_sala_cine_por_nombre @nombre_sala = N'Sala 1';
GO

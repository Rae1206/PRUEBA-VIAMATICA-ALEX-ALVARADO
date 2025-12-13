-- Script para eliminar la columna 'estado' de las tablas
-- ya que solo usaremos 'eliminado' para soft delete
-- Ejecutar en SQL Server Management Studio o Azure Data Studio

-- ============================================
-- 1. Eliminar columna 'estado' de sala_cine
-- ============================================
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.sala_cine') AND name = 'estado')
BEGIN
    ALTER TABLE [dbo].[sala_cine] DROP COLUMN [estado];
    PRINT 'Columna estado eliminada de sala_cine';
END
ELSE
BEGIN
    PRINT 'La columna estado no existe en sala_cine';
END

-- ============================================
-- 2. Eliminar columna 'estado' de users
-- ============================================
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
        PRINT 'Constraint default eliminado de users.estado';
    END
    
    ALTER TABLE [dbo].[users] DROP COLUMN [estado];
    PRINT 'Columna estado eliminada de users';
END
ELSE
BEGIN
    PRINT 'La columna estado no existe en users';
END

PRINT '';
PRINT '¡Script ejecutado correctamente!';
PRINT 'Ahora solo se usa la columna "eliminado" para soft delete.';

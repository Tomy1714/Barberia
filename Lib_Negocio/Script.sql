-- ============================================================
-- SCRIPT SQL - SISTEMA DE GESTION DE BARBERIA
-- Motor: SQL Server (SSMS)
-- Todas las PKs con IDENTITY(1,1)
-- ============================================================

CREATE DATABASE Barberia;
GO

USE Barberia;
GO

-- ============================================================
-- TABLA: Personas (base de Clientes y Empleados)
-- ============================================================
CREATE TABLE Personas (
    IdPersona      INT           IDENTITY(1,1) PRIMARY KEY,
    Identificacion VARCHAR(20)   NOT NULL UNIQUE,
    Nombres        VARCHAR(100)  NOT NULL,
    Apellidos      VARCHAR(100)  NOT NULL,
    Telefono       VARCHAR(20)   NOT NULL,
    Correo         VARCHAR(150)  NOT NULL UNIQUE,
    FechaNacimiento DATE         NULL,
    Direccion      VARCHAR(200)  NULL,
    FechaRegistro  DATE          NOT NULL DEFAULT GETDATE()
);
GO

-- ============================================================
-- TABLA: Clientes (hereda de Personas)
-- ============================================================
CREATE TABLE Clientes (
    IdCliente    INT      IDENTITY(1,1) PRIMARY KEY,
    IdPersona    INT      NOT NULL,
    TotalVisitas INT      NOT NULL DEFAULT 0,
    Activo       BIT      NOT NULL DEFAULT 1,
    CONSTRAINT FK_Clientes_Personas FOREIGN KEY (IdPersona)
        REFERENCES Personas(IdPersona)
);
GO

-- ============================================================
-- TABLA: Empleados (hereda de Personas)
-- ============================================================
CREATE TABLE Empleados (
    IdEmpleado  INT            IDENTITY(1,1) PRIMARY KEY,
    IdPersona   INT            NOT NULL,
    Cargo       VARCHAR(100)   NOT NULL,
    SalarioBase DECIMAL(12,2)  NOT NULL,
    Activo      BIT            NOT NULL DEFAULT 1,
    FechaIngreso DATE          NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Empleados_Personas FOREIGN KEY (IdPersona)
        REFERENCES Personas(IdPersona)
);
GO

-- ============================================================
-- TABLA: Sedes
-- ============================================================
CREATE TABLE Sedes (
    IdSede          INT           IDENTITY(1,1) PRIMARY KEY,
    Nombre          VARCHAR(150)  NOT NULL,
    Direccion       VARCHAR(200)  NOT NULL,
    Ciudad          VARCHAR(100)  NOT NULL,
    Telefono        VARCHAR(20)   NOT NULL,
    Correo          VARCHAR(150)  NULL,
    CapacidadMaxima INT           NOT NULL,
    Activa          BIT           NOT NULL DEFAULT 1,
    FechaApertura   DATE          NULL
);
GO

-- ============================================================
-- TABLA: Barberos (hereda de Empleados)
-- ============================================================
CREATE TABLE Barberos (
    IdBarbero          INT            IDENTITY(1,1) PRIMARY KEY,
    IdEmpleado         INT            NOT NULL,
    Especialidad       VARCHAR(150)   NOT NULL,
    PorcentajeComision DECIMAL(5,2)   NOT NULL DEFAULT 15,
    ServiciosMes       INT            NOT NULL DEFAULT 0,
    ValorPromServicio  DECIMAL(12,2)  NOT NULL DEFAULT 30000,
    Activo             BIT            NOT NULL DEFAULT 1,
    CONSTRAINT FK_Barberos_Empleados FOREIGN KEY (IdEmpleado)
        REFERENCES Empleados(IdEmpleado)
);
GO

-- ============================================================
-- TABLA: Recepcionistas (hereda de Empleados)
-- ============================================================
CREATE TABLE Recepcionistas (
    IdRecepcionista     INT           IDENTITY(1,1) PRIMARY KEY,
    IdEmpleado          INT           NOT NULL,
    IdSede              INT           NOT NULL,
    TurnoAsignado       VARCHAR(50)   NOT NULL,
    CitasGestionadasMes INT           NOT NULL DEFAULT 0,
    BonoPorMeta         DECIMAL(12,2) NOT NULL DEFAULT 150000,
    CONSTRAINT FK_Recepcionistas_Empleados FOREIGN KEY (IdEmpleado)
        REFERENCES Empleados(IdEmpleado),
    CONSTRAINT FK_Recepcionistas_Sedes FOREIGN KEY (IdSede)
        REFERENCES Sedes(IdSede)
);
GO

-- ============================================================
-- TABLA: Administradores (hereda de Empleados)
-- ============================================================
CREATE TABLE Administradores (
    IdAdministrador      INT            IDENTITY(1,1) PRIMARY KEY,
    IdEmpleado           INT            NOT NULL,
    NombreNegocio        VARCHAR(150)   NOT NULL,
    PorcentajeUtilidades DECIMAL(5,2)   NOT NULL DEFAULT 30,
    FechaFundacion       DATE           NULL,
    UtilidadesUltimoMes  DECIMAL(12,2)  NOT NULL DEFAULT 0,
    CONSTRAINT FK_Administradores_Empleados FOREIGN KEY (IdEmpleado)
        REFERENCES Empleados(IdEmpleado)
);
GO

-- ============================================================
-- TABLA: Servicios (base de ServiciosCorte, Tratamiento, Combo)
-- ============================================================
CREATE TABLE Servicios (
    IdServicio      INT            IDENTITY(1,1) PRIMARY KEY,
    Nombre          VARCHAR(150)   NOT NULL,
    Descripcion     VARCHAR(300)   NULL,
    PrecioBase      DECIMAL(12,2)  NOT NULL,
    DuracionMinutos INT            NOT NULL,
    Activo          BIT            NOT NULL DEFAULT 1
);
GO

-- ============================================================
-- TABLA: ServiciosCorte (hereda de Servicios)
-- ============================================================
CREATE TABLE ServiciosCorte (
    IdServicioCorte    INT           IDENTITY(1,1) PRIMARY KEY,
    IdServicio         INT           NOT NULL,
    TipoCorte          VARCHAR(100)  NOT NULL,
    IncluyeBarba       BIT           NOT NULL DEFAULT 0,
    NivelComplejidad   INT           NOT NULL DEFAULT 1,
    RecargoComplejidad DECIMAL(12,2) NOT NULL DEFAULT 0,
    CONSTRAINT FK_ServiciosCorte_Servicios FOREIGN KEY (IdServicio)
        REFERENCES Servicios(IdServicio)
);
GO

-- ============================================================
-- TABLA: ServiciosTratamiento (hereda de Servicios)
-- ============================================================
CREATE TABLE ServiciosTratamiento (
    IdServicioTratamiento INT           IDENTITY(1,1) PRIMARY KEY,
    IdServicio            INT           NOT NULL,
    TipoTratamiento       VARCHAR(150)  NOT NULL,
    CostoProducto         DECIMAL(12,2) NOT NULL DEFAULT 0,
    SesionesRequeridas    INT           NOT NULL DEFAULT 1,
    CONSTRAINT FK_ServiciosTratamiento_Servicios FOREIGN KEY (IdServicio)
        REFERENCES Servicios(IdServicio)
);
GO

-- ============================================================
-- TABLA: Combos (hereda de Servicios)
-- ============================================================
CREATE TABLE Combos (
    IdCombo        INT           IDENTITY(1,1) PRIMARY KEY,
    IdServicio     INT           NOT NULL,
    DescuentoCombo DECIMAL(5,2)  NOT NULL DEFAULT 10,
    Descripcion    VARCHAR(300)  NULL,
    CONSTRAINT FK_Combos_Servicios FOREIGN KEY (IdServicio)
        REFERENCES Servicios(IdServicio)
);
GO

-- ============================================================
-- TABLA: Productos
-- ============================================================
CREATE TABLE Productos (
    IdProducto   INT            IDENTITY(1,1) PRIMARY KEY,
    Nombre       VARCHAR(150)   NOT NULL,
    Marca        VARCHAR(100)   NOT NULL,
    Categoria    VARCHAR(100)   NULL,
    PrecioCompra DECIMAL(12,2)  NOT NULL,
    PrecioVenta  DECIMAL(12,2)  NOT NULL,
    StockActual  INT            NOT NULL DEFAULT 0,
    Activo       BIT            NOT NULL DEFAULT 1
);
GO

-- ============================================================
-- TABLA: Inventarios
-- ============================================================
CREATE TABLE Inventarios (
    IdInventario INT  IDENTITY(1,1) PRIMARY KEY,
    IdSede       INT  NOT NULL,
    StockMinimo  INT  NOT NULL DEFAULT 5,
    FechaActualizacion DATE NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Inventarios_Sedes FOREIGN KEY (IdSede)
        REFERENCES Sedes(IdSede)
);
GO

-- ============================================================
-- TABLA: InventarioProductos (tabla intermedia)
-- ============================================================
CREATE TABLE InventarioProductos (
    IdInventarioProducto INT IDENTITY(1,1) PRIMARY KEY,
    IdInventario         INT NOT NULL,
    IdProducto           INT NOT NULL,
    Cantidad             INT NOT NULL DEFAULT 0,
    CONSTRAINT FK_InventarioProductos_Inventarios FOREIGN KEY (IdInventario)
        REFERENCES Inventarios(IdInventario),
    CONSTRAINT FK_InventarioProductos_Productos FOREIGN KEY (IdProducto)
        REFERENCES Productos(IdProducto)
);
GO

-- ============================================================
-- TABLA: Horarios
-- ============================================================
CREATE TABLE Horarios (
    IdHorario   INT  IDENTITY(1,1) PRIMARY KEY,
    IdEmpleado  INT  NOT NULL,
    HoraEntrada INT  NOT NULL,
    HoraSalida  INT  NOT NULL,
    Activo      BIT  NOT NULL DEFAULT 1,
    CONSTRAINT FK_Horarios_Empleados FOREIGN KEY (IdEmpleado)
        REFERENCES Empleados(IdEmpleado)
);
GO

-- ============================================================
-- TABLA: HorarioDias (dias laborales del horario)
-- ============================================================
CREATE TABLE HorarioDias (
    IdHorarioDia INT          IDENTITY(1,1) PRIMARY KEY,
    IdHorario    INT          NOT NULL,
    Dia          VARCHAR(20)  NOT NULL,
    CONSTRAINT FK_HorarioDias_Horarios FOREIGN KEY (IdHorario)
        REFERENCES Horarios(IdHorario)
);
GO

-- ============================================================
-- TABLA: Turnos
-- ============================================================
CREATE TABLE Turnos (
    IdTurno    INT          IDENTITY(1,1) PRIMARY KEY,
    IdBarbero  INT          NOT NULL,
    IdSede     INT          NOT NULL,
    FechaTurno DATE         NOT NULL,
    HoraInicio INT          NOT NULL,
    HoraFin    INT          NOT NULL,
    Estado     VARCHAR(50)  NOT NULL DEFAULT 'Programado',
    CONSTRAINT FK_Turnos_Barberos FOREIGN KEY (IdBarbero)
        REFERENCES Barberos(IdBarbero),
    CONSTRAINT FK_Turnos_Sedes FOREIGN KEY (IdSede)
        REFERENCES Sedes(IdSede)
);
GO

-- ============================================================
-- TABLA: Citas
-- ============================================================
CREATE TABLE Citas (
    IdCita          INT           IDENTITY(1,1) PRIMARY KEY,
    IdCliente       INT           NOT NULL,
    IdBarbero       INT           NOT NULL,
    IdServicio      INT           NOT NULL,
    FechaHoraInicio DATETIME      NOT NULL,
    Estado          VARCHAR(50)   NOT NULL DEFAULT 'Pendiente',
    Observaciones   VARCHAR(300)  NULL,
    FechaCreacion   DATETIME      NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Citas_Clientes  FOREIGN KEY (IdCliente)
        REFERENCES Clientes(IdCliente),
    CONSTRAINT FK_Citas_Barberos  FOREIGN KEY (IdBarbero)
        REFERENCES Barberos(IdBarbero),
    CONSTRAINT FK_Citas_Servicios FOREIGN KEY (IdServicio)
        REFERENCES Servicios(IdServicio)
);
GO

-- ============================================================
-- TABLA: Pagos (implementa logica de IMetodoPago)
-- ============================================================
CREATE TABLE Pagos (
    IdPago       INT           IDENTITY(1,1) PRIMARY KEY,
    IdCita       INT           NOT NULL,
    Monto        DECIMAL(12,2) NOT NULL,
    Descuento    DECIMAL(12,2) NOT NULL DEFAULT 0,
    Total        DECIMAL(12,2) NOT NULL,
    NombreMetodo VARCHAR(100)  NOT NULL,
    EstadoPago   VARCHAR(50)   NOT NULL DEFAULT 'Pendiente',
    FechaPago    DATETIME      NOT NULL DEFAULT GETDATE(),
    Observaciones VARCHAR(300) NULL,
    CONSTRAINT FK_Pagos_Citas FOREIGN KEY (IdCita)
        REFERENCES Citas(IdCita)
);
GO

-- ============================================================
-- TABLA: Facturas
-- ============================================================
CREATE TABLE Facturas (
    IdFactura    INT           IDENTITY(1,1) PRIMARY KEY,
    IdPago       INT           NOT NULL,
    IdCliente    INT           NOT NULL,
    CodigoFactura VARCHAR(50)  NOT NULL UNIQUE,
    FechaEmision DATETIME      NOT NULL DEFAULT GETDATE(),
    Subtotal     DECIMAL(12,2) NOT NULL,
    Impuestos    DECIMAL(12,2) NOT NULL,
    Total        DECIMAL(12,2) NOT NULL,
    CONSTRAINT FK_Facturas_Pagos    FOREIGN KEY (IdPago)
        REFERENCES Pagos(IdPago),
    CONSTRAINT FK_Facturas_Clientes FOREIGN KEY (IdCliente)
        REFERENCES Clientes(IdCliente)
);
GO

-- ============================================================
-- TABLA: Calificaciones
-- ============================================================
CREATE TABLE Calificaciones (
    IdCalificacion INT           IDENTITY(1,1) PRIMARY KEY,
    IdCita         INT           NOT NULL,
    IdBarbero      INT           NOT NULL,
    Puntaje        INT           NOT NULL,
    Comentario     VARCHAR(500)  NULL,
    FechaCalificacion DATETIME   NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Calificaciones_Citas    FOREIGN KEY (IdCita)
        REFERENCES Citas(IdCita),
    CONSTRAINT FK_Calificaciones_Barberos FOREIGN KEY (IdBarbero)
        REFERENCES Barberos(IdBarbero),
    CONSTRAINT CHK_Puntaje CHECK (Puntaje >= 1 AND Puntaje <= 5)
);
GO

-- ============================================================
-- TABLA: Promociones
-- ============================================================
CREATE TABLE Promociones (
    IdPromocion         INT           IDENTITY(1,1) PRIMARY KEY,
    IdServicio          INT           NOT NULL,
    Nombre              VARCHAR(150)  NOT NULL,
    Descripcion         VARCHAR(300)  NULL,
    PorcentajeDescuento DECIMAL(5,2)  NOT NULL,
    FechaInicio         DATE          NOT NULL,
    FechaFin            DATE          NOT NULL,
    Activa              BIT           NOT NULL DEFAULT 1,
    CONSTRAINT FK_Promociones_Servicios FOREIGN KEY (IdServicio)
        REFERENCES Servicios(IdServicio)
);
GO

-- ============================================================
-- TABLA: Notificaciones
-- ============================================================
CREATE TABLE Notificaciones (
    IdNotificacion INT          IDENTITY(1,1) PRIMARY KEY,
    IdCliente      INT          NOT NULL,
    IdCita         INT          NOT NULL,
    Tipo           VARCHAR(50)  NOT NULL,
    Canal          VARCHAR(50)  NOT NULL,
    Mensaje        VARCHAR(500) NULL,
    Estado         VARCHAR(50)  NOT NULL DEFAULT 'Pendiente',
    FechaEnvio     DATETIME     NULL,
    FechaCreacion  DATETIME     NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Notificaciones_Clientes FOREIGN KEY (IdCliente)
        REFERENCES Clientes(IdCliente),
    CONSTRAINT FK_Notificaciones_Citas    FOREIGN KEY (IdCita)
        REFERENCES Citas(IdCita)
);
GO

-- ============================================================
-- TABLA: PuntosFidelidad
-- ============================================================
CREATE TABLE PuntosFidelidad (
    IdPuntos         INT           IDENTITY(1,1) PRIMARY KEY,
    IdCliente        INT           NOT NULL UNIQUE,
    PuntosAcumulados INT           NOT NULL DEFAULT 0,
    FactorConversion DECIMAL(10,2) NOT NULL DEFAULT 100,
    FechaActualizacion DATETIME    NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_PuntosFidelidad_Clientes FOREIGN KEY (IdCliente)
        REFERENCES Clientes(IdCliente)
);
GO

-- ============================================================
-- TABLA: EmpleadoSede (relacion empleados con sedes)
-- ============================================================
CREATE TABLE EmpleadoSede (
    IdEmpleadoSede INT  IDENTITY(1,1) PRIMARY KEY,
    IdEmpleado     INT  NOT NULL,
    IdSede         INT  NOT NULL,
    FechaAsignacion DATE NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_EmpleadoSede_Empleados FOREIGN KEY (IdEmpleado)
        REFERENCES Empleados(IdEmpleado),
    CONSTRAINT FK_EmpleadoSede_Sedes FOREIGN KEY (IdSede)
        REFERENCES Sedes(IdSede)
);
GO

-- ============================================================
-- TABLA: ComboServicios (servicios incluidos en un combo)
-- ============================================================
CREATE TABLE ComboServicios (
    IdComboServicio INT IDENTITY(1,1) PRIMARY KEY,
    IdCombo         INT NOT NULL,
    IdServicio      INT NOT NULL,
    CONSTRAINT FK_ComboServicios_Combos    FOREIGN KEY (IdCombo)
        REFERENCES Combos(IdCombo),
    CONSTRAINT FK_ComboServicios_Servicios FOREIGN KEY (IdServicio)
        REFERENCES Servicios(IdServicio)
);
GO

CREATE TABLE Usuarios (
    IdUsuario  INT           IDENTITY(1,1) PRIMARY KEY,
    IdPersona  INT           NOT NULL,
    Email      VARCHAR(150)  NOT NULL UNIQUE,
    Contrasena VARCHAR(255)  NOT NULL,
    Rol        VARCHAR(50)   NOT NULL DEFAULT 'Cliente',
    Activo     BIT           NOT NULL DEFAULT 1,
    FechaCreacion DATETIME   NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Usuarios_Personas FOREIGN KEY (IdPersona)
        REFERENCES Personas(IdPersona),
    CONSTRAINT CHK_Rol CHECK (Rol IN ('Administrador', 'Barbero', 'Cliente'))
);
GO


-- ============================================================
-- DATOS DE PRUEBA
-- ============================================================

-- Personas
INSERT INTO Personas (Identificacion, Nombres, Apellidos, Telefono, Correo, FechaNacimiento, Direccion)
VALUES
('1234567890', 'Tomas',  'Vargas', '3001234567', 'tomasvargas@email.com',  '1985-03-15', 'Calle 10 # 20-30'),
('9876543210', 'Pepito', 'Perez',  '3109876543', 'pepito@barberia.com',    '1990-07-22', 'Carrera 5 # 10-15'),
('1112223330', 'Ana',    'Gomez',  '3201112233', 'ana@barberia.com',       '1995-01-10', 'Avenida 80 # 5-20'),
('1112223331', 'Lolo',   'Lopez',  '3005551234', 'lolo@mail.com',          '1998-11-05', 'Calle 50 # 30-40');
GO

-- Sede
INSERT INTO Sedes (Nombre, Direccion, Ciudad, Telefono, Correo, CapacidadMaxima, FechaApertura)
VALUES ('BarberPro Robledo', 'Calle Falsa 10-20', 'Medellin', '6012345678', 'robledo@barberpro.com', 5, '2018-05-10');
GO

-- Empleados
INSERT INTO Empleados (IdPersona, Cargo, SalarioBase, FechaIngreso)
VALUES
(1, 'Administrador', 3000000, '2018-05-10'),
(2, 'Barbero',       1200000, '2019-03-01'),
(3, 'Recepcionista', 1000000, '2020-06-15');
GO

-- Administrador
INSERT INTO Administradores (IdEmpleado, NombreNegocio, PorcentajeUtilidades, FechaFundacion, UtilidadesUltimoMes)
VALUES (1, 'BarberPro', 30, '2018-05-10', 5000000);
GO

-- Barbero
INSERT INTO Barberos (IdEmpleado, Especialidad, PorcentajeComision, ServiciosMes, ValorPromServicio)
VALUES (2, 'Degradados', 15, 45, 30000);
GO

-- Recepcionista
INSERT INTO Recepcionistas (IdEmpleado, IdSede, TurnoAsignado, CitasGestionadasMes, BonoPorMeta)
VALUES (3, 1, 'Manana', 120, 150000);
GO

-- Cliente
INSERT INTO Clientes (IdPersona, TotalVisitas)
VALUES (4, 1);
GO

-- Servicios
INSERT INTO Servicios (Nombre, Descripcion, PrecioBase, DuracionMinutos)
VALUES
('Corte Clasico',      'Corte con tijera tradicional',  25000, 40),
('Hidratacion Capilar','Tratamiento con keratina',       80000, 90),
('Combo Barbero',      'Corte mas barba mas tratamiento',95000, 120);
GO

-- ServicioCorte
INSERT INTO ServiciosCorte (IdServicio, TipoCorte, IncluyeBarba, NivelComplejidad, RecargoComplejidad)
VALUES (1, 'Clasico', 0, 1, 5000);
GO

-- ServicioTratamiento
INSERT INTO ServiciosTratamiento (IdServicio, TipoTratamiento, CostoProducto, SesionesRequeridas)
VALUES (2, 'Hidratacion', 15000, 3);
GO

-- Combo
INSERT INTO Combos (IdServicio, DescuentoCombo, Descripcion)
VALUES (3, 10, 'Paquete completo con descuento');
GO

-- Producto
INSERT INTO Productos (Nombre, Marca, Categoria, PrecioCompra, PrecioVenta, StockActual)
VALUES
('Gel Fijador', 'Taft',  'Fijacion', 8000,  15000, 20),
('Cera Capilar','Gelo',  'Fijacion', 10000, 18000, 3),
('Keratina',    'Loreal','Tratamiento',25000,45000, 8);
GO

-- Inventario
INSERT INTO Inventarios (IdSede, StockMinimo)
VALUES (1, 5);
GO

INSERT INTO InventarioProductos (IdInventario, IdProducto, Cantidad)
VALUES (1, 1, 20), (1, 2, 3), (1, 3, 8);
GO

-- Turno
INSERT INTO Turnos (IdBarbero, IdSede, FechaTurno, HoraInicio, HoraFin, Estado)
VALUES (1, 1, '2026-04-01', 8, 17, 'Programado');
GO

-- Horario
INSERT INTO Horarios (IdEmpleado, HoraEntrada, HoraSalida)
VALUES (2, 8, 17);
GO

INSERT INTO HorarioDias (IdHorario, Dia)
VALUES (1,'Lunes'), (1,'Martes'), (1,'Miercoles'), (1,'Jueves'), (1,'Viernes');
GO

-- Cita
INSERT INTO Citas (IdCliente, IdBarbero, IdServicio, FechaHoraInicio, Estado)
VALUES (1, 1, 1, '2026-04-01 10:00:00', 'Confirmada');
GO

-- Pago
INSERT INTO Pagos (IdCita, Monto, Descuento, Total, NombreMetodo, EstadoPago)
VALUES (1, 30000, 500, 29500, 'Tarjeta', 'Aprobado');
GO

-- Factura
INSERT INTO Facturas (IdPago, IdCliente, CodigoFactura, Subtotal, Impuestos, Total)
VALUES (1, 1, 'FAC-0001', 29500, 5605, 35105);
GO

-- PuntosFidelidad
INSERT INTO PuntosFidelidad (IdCliente, PuntosAcumulados, FactorConversion)
VALUES (1, 30, 100);
GO

-- Calificacion
INSERT INTO Calificaciones (IdCita, IdBarbero, Puntaje, Comentario)
VALUES (1, 1, 5, 'Excelente servicio, muy profesional');
GO

-- Promocion
INSERT INTO Promociones (IdServicio, Nombre, Descripcion, PorcentajeDescuento, FechaInicio, FechaFin)
VALUES (1, 'Descuento Lunes', 'Promo especial dias lunes', 10, '2026-04-01', '2026-04-30');
GO

-- Notificacion
INSERT INTO Notificaciones (IdCliente, IdCita, Tipo, Canal, Mensaje, Estado, FechaEnvio)
VALUES (1, 1, 'Confirmacion', 'Correo', 'Tu cita #1 fue confirmada.', 'Enviada', GETDATE());
GO

-- EmpleadoSede
INSERT INTO EmpleadoSede (IdEmpleado, IdSede)
VALUES (1,1),(2,1),(3,1);
GO

-- ComboServicios
INSERT INTO ComboServicios (IdCombo, IdServicio)
VALUES (1,1),(1,2);
GO

INSERT INTO Usuarios (IdPersona, Email, Contrasena, Rol)
VALUES
(1, 'tomasvargas@email.com',  '1234', 'Administrador'),
(2, 'pepito@barberia.com',    '1234', 'Barbero'),
(4, 'lolo@mail.com',          '1234', 'Cliente');
GO





CREATE TABLE PersonasAuditoria (
    IdAuditoria  INT          IDENTITY(1,1) PRIMARY KEY,
    IdPersona                      INT          NOT NULL,
    Accion       VARCHAR(20)  NOT NULL,
    Fecha        DATETIME     NOT NULL DEFAULT GETDATE()
);
GO

CREATE TABLE ClientesAuditoria (
    IdAuditoria  INT          IDENTITY(1,1) PRIMARY KEY,
    IdCliente                      INT          NOT NULL,
    Accion       VARCHAR(20)  NOT NULL,
    Fecha        DATETIME     NOT NULL DEFAULT GETDATE()
);
GO

CREATE TABLE EmpleadosAuditoria (
    IdAuditoria  INT          IDENTITY(1,1) PRIMARY KEY,
    IdEmpleado                     INT          NOT NULL,
    Accion       VARCHAR(20)  NOT NULL,
    Fecha        DATETIME     NOT NULL DEFAULT GETDATE()
);
GO

CREATE TABLE BarberosAuditoria (
    IdAuditoria  INT          IDENTITY(1,1) PRIMARY KEY,
    IdBarbero                      INT          NOT NULL,
    Accion       VARCHAR(20)  NOT NULL,
    Fecha        DATETIME     NOT NULL DEFAULT GETDATE()
);
GO

CREATE TABLE RecepcionistasAuditoria (
    IdAuditoria  INT          IDENTITY(1,1) PRIMARY KEY,
    IdRecepcionista                INT          NOT NULL,
    Accion       VARCHAR(20)  NOT NULL,
    Fecha        DATETIME     NOT NULL DEFAULT GETDATE()
);
GO

CREATE TABLE AdministradoresAuditoria (
    IdAuditoria  INT          IDENTITY(1,1) PRIMARY KEY,
    IdAdministrador                INT          NOT NULL,
    Accion       VARCHAR(20)  NOT NULL,
    Fecha        DATETIME     NOT NULL DEFAULT GETDATE()
);
GO

CREATE TABLE SedesAuditoria (
    IdAuditoria  INT          IDENTITY(1,1) PRIMARY KEY,
    IdSede                         INT          NOT NULL,
    Accion       VARCHAR(20)  NOT NULL,
    Fecha        DATETIME     NOT NULL DEFAULT GETDATE()
);
GO

CREATE TABLE ServiciosAuditoria (
    IdAuditoria  INT          IDENTITY(1,1) PRIMARY KEY,
    IdServicio                     INT          NOT NULL,
    Accion       VARCHAR(20)  NOT NULL,
    Fecha        DATETIME     NOT NULL DEFAULT GETDATE()
);
GO

CREATE TABLE ServiciosCorteAuditoria (
    IdAuditoria  INT          IDENTITY(1,1) PRIMARY KEY,
    IdServicioCorte                INT          NOT NULL,
    Accion       VARCHAR(20)  NOT NULL,
    Fecha        DATETIME     NOT NULL DEFAULT GETDATE()
);
GO

CREATE TABLE ServiciosTratamientoAuditoria (
    IdAuditoria  INT          IDENTITY(1,1) PRIMARY KEY,
    IdServicioTratamiento          INT          NOT NULL,
    Accion       VARCHAR(20)  NOT NULL,
    Fecha        DATETIME     NOT NULL DEFAULT GETDATE()
);
GO

CREATE TABLE CombosAuditoria (
    IdAuditoria  INT          IDENTITY(1,1) PRIMARY KEY,
    IdCombo                        INT          NOT NULL,
    Accion       VARCHAR(20)  NOT NULL,
    Fecha        DATETIME     NOT NULL DEFAULT GETDATE()
);
GO

CREATE TABLE ProductosAuditoria (
    IdAuditoria  INT          IDENTITY(1,1) PRIMARY KEY,
    IdProducto                     INT          NOT NULL,
    Accion       VARCHAR(20)  NOT NULL,
    Fecha        DATETIME     NOT NULL DEFAULT GETDATE()
);
GO

CREATE TABLE InventariosAuditoria (
    IdAuditoria  INT          IDENTITY(1,1) PRIMARY KEY,
    IdInventario                   INT          NOT NULL,
    Accion       VARCHAR(20)  NOT NULL,
    Fecha        DATETIME     NOT NULL DEFAULT GETDATE()
);
GO

CREATE TABLE InventarioProductosAuditoria (
    IdAuditoria  INT          IDENTITY(1,1) PRIMARY KEY,
    IdInventarioProducto           INT          NOT NULL,
    Accion       VARCHAR(20)  NOT NULL,
    Fecha        DATETIME     NOT NULL DEFAULT GETDATE()
);
GO

CREATE TABLE TurnosAuditoria (
    IdAuditoria  INT          IDENTITY(1,1) PRIMARY KEY,
    IdTurno                        INT          NOT NULL,
    Accion       VARCHAR(20)  NOT NULL,
    Fecha        DATETIME     NOT NULL DEFAULT GETDATE()
);
GO

CREATE TABLE HorariosAuditoria (
    IdAuditoria  INT          IDENTITY(1,1) PRIMARY KEY,
    IdHorario                      INT          NOT NULL,
    Accion       VARCHAR(20)  NOT NULL,
    Fecha        DATETIME     NOT NULL DEFAULT GETDATE()
);
GO

CREATE TABLE HorariosDiasAuditoria (
    IdAuditoria  INT          IDENTITY(1,1) PRIMARY KEY,
    IdHorarioDia                   INT          NOT NULL,
    Accion       VARCHAR(20)  NOT NULL,
    Fecha        DATETIME     NOT NULL DEFAULT GETDATE()
);
GO

CREATE TABLE CitasAuditoria (
    IdAuditoria  INT          IDENTITY(1,1) PRIMARY KEY,
    IdCita                         INT          NOT NULL,
    Accion       VARCHAR(20)  NOT NULL,
    Fecha        DATETIME     NOT NULL DEFAULT GETDATE()
);
GO

CREATE TABLE PagosAuditoria (
    IdAuditoria  INT          IDENTITY(1,1) PRIMARY KEY,
    IdPago                         INT          NOT NULL,
    Accion       VARCHAR(20)  NOT NULL,
    Fecha        DATETIME     NOT NULL DEFAULT GETDATE()
);
GO

CREATE TABLE FacturasAuditoria (
    IdAuditoria  INT          IDENTITY(1,1) PRIMARY KEY,
    IdFactura                      INT          NOT NULL,
    Accion       VARCHAR(20)  NOT NULL,
    Fecha        DATETIME     NOT NULL DEFAULT GETDATE()
);
GO

CREATE TABLE CalificacionesAuditoria (
    IdAuditoria  INT          IDENTITY(1,1) PRIMARY KEY,
    IdCalificacion                 INT          NOT NULL,
    Accion       VARCHAR(20)  NOT NULL,
    Fecha        DATETIME     NOT NULL DEFAULT GETDATE()
);
GO

CREATE TABLE PromocionesAuditoria (
    IdAuditoria  INT          IDENTITY(1,1) PRIMARY KEY,
    IdPromocion                    INT          NOT NULL,
    Accion       VARCHAR(20)  NOT NULL,
    Fecha        DATETIME     NOT NULL DEFAULT GETDATE()
);
GO

CREATE TABLE NotificacionesAuditoria (
    IdAuditoria  INT          IDENTITY(1,1) PRIMARY KEY,
    IdNotificacion                 INT          NOT NULL,
    Accion       VARCHAR(20)  NOT NULL,
    Fecha        DATETIME     NOT NULL DEFAULT GETDATE()
);
GO

CREATE TABLE PuntosFidelidadAuditoria (
    IdAuditoria  INT          IDENTITY(1,1) PRIMARY KEY,
    IdPuntos                       INT          NOT NULL,
    Accion       VARCHAR(20)  NOT NULL,
    Fecha        DATETIME     NOT NULL DEFAULT GETDATE()
);
GO

CREATE TABLE EmpleadosSedesAuditoria (
    IdAuditoria  INT          IDENTITY(1,1) PRIMARY KEY,
    IdEmpleadoSede                 INT          NOT NULL,
    Accion       VARCHAR(20)  NOT NULL,
    Fecha        DATETIME     NOT NULL DEFAULT GETDATE()
);
GO

CREATE TABLE ComboServiciosAuditoria (
    IdAuditoria  INT          IDENTITY(1,1) PRIMARY KEY,
    IdComboServicio                INT          NOT NULL,
    Accion       VARCHAR(20)  NOT NULL,
    Fecha        DATETIME     NOT NULL DEFAULT GETDATE()
);
GO

CREATE TABLE UsuariosAuditoria (
    IdAuditoria  INT          IDENTITY(1,1) PRIMARY KEY,
    IdUsuario                      INT          NOT NULL,
    Accion       VARCHAR(20)  NOT NULL,
    Fecha        DATETIME     NOT NULL DEFAULT GETDATE()
);
GO

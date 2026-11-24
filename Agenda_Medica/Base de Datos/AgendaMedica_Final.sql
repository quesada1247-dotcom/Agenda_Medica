
CREATE DATABASE IF NOT EXISTS AgendaMedica_Final
CHARACTER SET utf8mb4
COLLATE utf8mb4_unicode_ci;

USE AgendaMedica_Final;

-- ====================================================
--  TABLA DE ROLES
-- =====================================================
CREATE TABLE IF NOT EXISTS Roles (
    id_rol INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL UNIQUE,
    descripcion VARCHAR(150)
);

-- ====================================================
--  TABLA DE USUARIOS
-- =====================================================
CREATE TABLE IF NOT EXISTS Usuarios (
    id_usuario INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    usuario VARCHAR(50) NOT NULL UNIQUE,
    correo VARCHAR(100) UNIQUE,
    contrasena VARCHAR(255) NOT NULL,
    id_rol INT NOT NULL,
    estado TINYINT(1) NOT NULL DEFAULT 1,

    CONSTRAINT fk_usuario_rol
        FOREIGN KEY (id_rol)
        REFERENCES Roles(id_rol)
        ON DELETE RESTRICT
        ON UPDATE CASCADE
);

-- =====================================================
--  TABLA DE PACIENTES
-- =====================================================
CREATE TABLE IF NOT EXISTS Pacientes (
    id_paciente INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(60) NOT NULL,
    apellidos VARCHAR(100) NOT NULL,
    identificacion VARCHAR(30) NOT NULL UNIQUE,
    telefono VARCHAR(25),
    correo VARCHAR(100),
    fecha_nacimiento DATE,
    direccion VARCHAR(200),
    estado TINYINT(1) NOT NULL DEFAULT 1
);
-- ====================================================
--  TABLA DE ESPECIALIDADES
-- =====================================================
CREATE TABLE IF NOT EXISTS Especialidades (
    id_especialidad INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(80) NOT NULL UNIQUE,
    descripcion VARCHAR(250),
    estado TINYINT(1) NOT NULL DEFAULT 1
);

-- =====================================================
--  TABLA DE DOCTORES
-- =====================================================
CREATE TABLE IF NOT EXISTS Doctores (
    id_doctor INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(60) NOT NULL,
    apellidos VARCHAR(100) NOT NULL,
    identificacion VARCHAR(30) UNIQUE,
    telefono VARCHAR(25),
    correo VARCHAR(100),
    id_especialidad INT NOT NULL,
    estado TINYINT(1) NOT NULL DEFAULT 1,

    CONSTRAINT fk_doctor_especialidad
        FOREIGN KEY (id_especialidad)
        REFERENCES Especialidades(id_especialidad)
        ON DELETE RESTRICT
        ON UPDATE CASCADE
);

-- ====================================================
--  TABLA DE HORARIOS MEDICOS
-- =====================================================
CREATE TABLE IF NOT EXISTS HorariosMedicos (
    id_horario INT AUTO_INCREMENT PRIMARY KEY,
    id_doctor INT NOT NULL,
    dia_semana ENUM(
        'Lunes',
        'Martes',
        'Miercoles',
        'Jueves',
        'Viernes',
        'Sabado',
        'Domingo'
    ) NOT NULL,
    hora_inicio TIME NOT NULL,
    hora_fin TIME NOT NULL,
    estado TINYINT(1) NOT NULL DEFAULT 1,

    CONSTRAINT fk_horario_doctor
        FOREIGN KEY (id_doctor)
        REFERENCES Doctores(id_doctor)
        ON DELETE RESTRICT
        ON UPDATE CASCADE,

    CONSTRAINT chk_horario
        CHECK (hora_inicio < hora_fin),

    UNIQUE KEY uq_doctor_dia_inicio
        (id_doctor, dia_semana, hora_inicio)
);

-- =====================================================
--  TABLA DE CITAS
-- =====================================================
CREATE TABLE IF NOT EXISTS Citas (
    id_cita INT AUTO_INCREMENT PRIMARY KEY,
    id_paciente INT NOT NULL,
    id_doctor INT NOT NULL,
    id_usuario INT NULL,
    fecha DATE NOT NULL,
    hora TIME NOT NULL,

    duracion_minutos INT NOT NULL DEFAULT 30,

    hora_fin TIME GENERATED ALWAYS AS (
        ADDTIME(hora, SEC_TO_TIME(duracion_minutos * 60))
    ) STORED,

    tipo ENUM(
        'Presencial',
        'Virtual',
        'Urgencia'
    ) NOT NULL DEFAULT 'Presencial',

    motivo VARCHAR(250) NOT NULL,

    estado ENUM(
        'Programada',
        'Confirmada',
        'Atendida',
        'Cancelada',
        'No asistio',
        'Reprogramada'
    ) NOT NULL DEFAULT 'Programada',

    observaciones VARCHAR(500),

    CONSTRAINT fk_cita_paciente
        FOREIGN KEY (id_paciente)
        REFERENCES Pacientes(id_paciente)
        ON DELETE RESTRICT
        ON UPDATE CASCADE,

    CONSTRAINT fk_cita_doctor
        FOREIGN KEY (id_doctor)
        REFERENCES Doctores(id_doctor)
        ON DELETE RESTRICT
        ON UPDATE CASCADE,

    CONSTRAINT fk_cita_usuario
        FOREIGN KEY (id_usuario)
        REFERENCES Usuarios(id_usuario)
        ON DELETE SET NULL
        ON UPDATE CASCADE,

    CONSTRAINT chk_duracion
        CHECK (duracion_minutos IN (30, 45, 60)),

    INDEX idx_citas_doctor_fecha (id_doctor, fecha),
    INDEX idx_citas_paciente_fecha (id_paciente, fecha),
    INDEX idx_citas_estado (estado)
);

-- =====================================================
--  TABLA DE HISTORIAL MEDICO
-- =====================================================
CREATE TABLE IF NOT EXISTS HistorialMedico (
    id_historial INT AUTO_INCREMENT PRIMARY KEY,
    id_paciente INT NOT NULL,
    id_doctor INT NOT NULL,
    id_cita INT NULL,
    fecha_registro DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    diagnostico TEXT NOT NULL,
    tratamiento TEXT,
    observaciones TEXT,

    CONSTRAINT fk_historial_paciente
        FOREIGN KEY (id_paciente)
        REFERENCES Pacientes(id_paciente)
        ON DELETE RESTRICT
        ON UPDATE CASCADE,

    CONSTRAINT fk_historial_doctor
        FOREIGN KEY (id_doctor)
        REFERENCES Doctores(id_doctor)
        ON DELETE RESTRICT
        ON UPDATE CASCADE,

    CONSTRAINT fk_historial_cita
        FOREIGN KEY (id_cita)
        REFERENCES Citas(id_cita)
        ON DELETE SET NULL
        ON UPDATE CASCADE,

    INDEX idx_historial_paciente (id_paciente)
);

-- =====================================================
-- DATOS INICIALES
-- =====================================================
INSERT IGNORE INTO Roles (nombre, descripcion)
VALUES
('Administrador', 'Acceso administrativo al sistema'),
('Recepcion', 'Gestion de pacientes y citas');


INSERT IGNORE INTO Especialidades (nombre, descripcion)
VALUES
('Medicina General', 'Atencion medica general'),
('Pediatria', 'Atencion medica infantil'),
('Cardiologia', 'Especialidad del corazon'),
('Dermatologia', 'Especialidad de la piel'),
('Odontologia', 'Atencion de salud bucodental');
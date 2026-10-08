CREATE TABLE planes (
    id INT PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    velocidad_mbps INT NOT NULL,
    precio_mensual DECIMAL(10,2) NOT NULL,
    activo BOOLEAN DEFAULT TRUE
);

CREATE TABLE clientes (
    id INT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    cedula VARCHAR(20) NOT NULL UNIQUE,
    telefono VARCHAR(20),
    direccion VARCHAR(150),
    correo VARCHAR(100),
    plan_id INT,
    activo BOOLEAN DEFAULT TRUE,
    FOREIGN KEY (plan_id) REFERENCES planes(id)
);

CREATE TABLE facturas (
    id INT PRIMARY KEY,
    cliente_id INT NOT NULL,
    monto DECIMAL(10,2) NOT NULL,
    fecha_emision DATE NOT NULL,
    fecha_vencimiento DATE NOT NULL,
    estado VARCHAR(10) NOT NULL DEFAULT 'Pendiente',
    FOREIGN KEY (cliente_id) REFERENCES clientes(id)
);

CREATE TABLE pagos (
    id INT PRIMARY KEY,
    factura_id INT NOT NULL,
    monto DECIMAL(10,2) NOT NULL,
    fecha DATE NOT NULL,
    metodo VARCHAR(20) NOT NULL,
    FOREIGN KEY (factura_id) REFERENCES facturas(id)
);

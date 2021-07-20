create database PuntoDeVenta

use PuntoDeVenta

create table Usuarios
(
	Id int identity(1,1) NOT NULL primary key,
	Email varchar(250) NOT NULL unique,
	NombreUsuario varchar(100) unique,
	Nombre varchar(100),
	Apellido varchar(100),	
	Pass varchar(max),
	Roll varchar(100),
	FechaCreado	datetime,
	Estatus bit,
)

create table Categorias
(
	Id bigint identity(1,1) NOT NULL primary key,
	Nombre varchar(100) not null,
	FechaCreado	datetime,
	Estatus bit,
)

create table Productos
(
	Id bigint identity(1,1) NOT NULL primary key,
	Codigo varchar(100) not null,
	NombreProducto varchar(100),
	Descripcion varchar(max),
	Imagen varchar(max),
	Costo decimal(18,2),
	Precio decimal(18,2),
	Stock int,
	IdCategoria bigint FOREIGN KEY REFERENCES Categorias (Id),
	FechaCreado	datetime,
	Estatus bit
)


create table Direccion
(
	Id int identity(1,1) NOT NULL primary key,
	Provincia varchar(100),
	Ciudad varchar(100),
	Calle varchar(100),
	NumeroCasa varchar(100),
)

create table Clientes
(
	Id bigint identity(1,1) NOT NULL primary key,
	Nombre varchar(100),
	Apellido varchar(100),
	Email varchar(100),
	Cedula varchar(100) unique,
	Telefono varchar(100),
	IdDireccion int FOREIGN KEY REFERENCES Direccion (Id),
	FechaCreado	datetime,
	Estatus bit,
)



create table Proveedores
(
	Id int identity(1,1) NOT NULL primary key,
	Rnc varchar(100) unique,
	RazonSocial varchar(100),
	Telefono varchar(100),
	IdDireccion int FOREIGN KEY REFERENCES Direccion (Id),
	FechaCreado	datetime,
	Estatus bit,
)

create table Compras
(
	Id bigint identity(1,1) NOT NULL primary key,
	Fecha datetime,
	IdUsuario int FOREIGN KEY REFERENCES Usuarios (Id),
	IdProveedor int FOREIGN KEY REFERENCES Proveedores (Id),	
)

create table DetalleCompras
(
	Id int identity(1,1) NOT NULL primary key,	
	Costo decimal(18,2),
	Cantidad int,
	IdCompra bigint FOREIGN KEY REFERENCES Compras (Id),
	IdProducto bigint FOREIGN KEY REFERENCES Productos (Id)
)

create table Ventas
(
	Id bigint identity(1,1) NOT NULL primary key,
	Fecha datetime,
	IdUsuario int FOREIGN KEY REFERENCES Usuarios (Id),
	IdCliente bigint FOREIGN KEY REFERENCES Clientes (Id),
)

create table DetalleVentas
(
	Id int identity(1,1) NOT NULL primary key,	
	Precio decimal(18,2),
	Cantidad int,
	IdVenta bigint FOREIGN KEY  REFERENCES Ventas (Id),
	IdProducto bigint FOREIGN KEY REFERENCES Productos (Id)
)

create table Empresa(
	id int not null IDENTITY(1,1) primary key,
	rnc varchar(50) NOT NULL unique,
	ncf varchar(250) unique null,
	razonSocial	varchar(250) NOT NULL,
	nombre varchar(250) NOT NULL,
	direccion varchar(250) not null,
	telefono varchar(100),
	fechaCreado	datetime,
	Estatus bit,	
)

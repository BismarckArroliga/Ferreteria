
/* Crear base de Datos */
create database BD_Ferreteria
go

/* Usar base de Datos */
use  BD_Ferreteria
go

create table Cargos
(
	Id		int primary key identity(1,1),
	Cargo		varchar(50),
)
go

create table Empleados
(
	Id		int primary key identity(1,1),
	Fecha		date,
	Nombre		varchar(50),
	Apellido	varchar(50),
	Correo		varchar(50),
	Telefono	varchar(15),
	Direccion	varchar(50),
	Cargo		int,

	foreign key(Cargo) references Cargos(Id)	
)
go

create table Usuarios 
(
	Id		int primary key identity(1,1),
	Usuario		varchar(50),
	Contrasena	varchar(50),
	Empleado	int

	foreign key(Empleado) references Empleados(Id)
)
go

create table Clientes
(
	Id		int primary key identity(1,1),
	Fecha		date,
	Nombre		varchar(50) not null,
	Apellido	varchar(50) not null,
	Cedula		varchar(50) not null,
	Telefono	varchar(15) not null,
	Direccion	varchar(50) not null
)	
go


create table Estados
(
	Id int 		primary key identity(1,1),
	Estado		varchar(50)
)
go

create table Productos
(
	Id int 		identity(1,1),
	Codigo		varchar(10) primary key,
	Nombre		varchar(50),
	Descripcion 	text,
	Stock		int,
	Precio		float,
	Estado		int

	foreign key(Estado) references Estados(Id)
)
go


create table Factura
(
	Id		int primary key identity(1,1),
	Fecha		date,
	Cliente		int,
	Empleado	int,
	MontoPago	float,
	MontoCambio	float,
	MontoTotal	float

	foreign key(Cliente) references Clientes(Id),
	foreign key(Empleado) references Empleados(Id)
)
go

create table DetalleFactura
(
	Id 		int primary key identity (1,1),
	Factura	 	int,
	Producto 	varchar(10),
	Precio 		float,
	Unidades 	int,
	Total 		float,

	foreign key(Factura) references Factura(Id),
	foreign key(Producto) references Productos(codigo) on delete cascade
)
go

/********************* INSERCIONES A LAS  TABLAS ************************/
/*----------------------------------------------------------------------*/
insert into Estados values ('Activo')
insert into Estados values ('No Activo')
go

insert into Cargos values ('Administrador')
insert into Cargos values ('Empleado')
go

insert into Empleados values 
(convert(date, getdate(),103), 'Bismarck','Arroliga', 'bismarckarroliga@gmail.com','+50589307934', 'Leon', 1),
(convert(date, getdate(),103), 'Walter','Garcia', 'waltergarcia99@gmail.com', '+50581307900', 'Managua', 2)
go
 
insert into Usuarios values ('Bismarck', 'pajaro123', 1)
insert into Usuarios values ('Walter', 'walter001', 2)
go

insert into Clientes values (convert(date, getdate(), 103), 'Franklin','Peralta', '28123059010002B','+50586307700', 'Granada')
insert into Clientes values (convert(date, getdate(), 103), 'Milton','Hernandez', '28123019810002B', '+50581307769', 'Managua')
insert into Clientes values (convert(date, getdate(), 103), 'Brandon','Garcia','28123052010002B', '+50588307708', 'Managua')
insert into Clientes values (convert(date, getdate(), 103), 'Pedro','Silva','28123052010002B', '+50581307708', 'Managua')
insert into Clientes values (convert(date, getdate(), 103), 'Marcos','Munguia','28123052010002B', '+50587307708', 'Managua')
go

insert into Productos values ('PIN001', 'Pintura', 'Autos', 20, 340.00, 1)
insert into Productos values ('BAT002', 'Baterias', 'Motos', 16, 650.00, 1)
insert into Productos values ('ALI003', 'Alicates', 'Aluminio', 10, 150.00, 1)
insert into Productos values ('MAR004', 'Martillos', 'Aluminio', 10, 250.00, 1)
insert into Productos values ('GUA005', 'Guantes', 'Plastico', 30, 100.00, 1)
insert into Productos values ('CAR006', 'Carretillas', 'Color Verde', 5, 4000.00, 1)
go



/**************** CREACION DE PROCEDIMIENTOS ALMACENADOS *******************/
/*-------------------------------------------------------------------------*/


/* ---------- PROCEDIMIENTOS PARA EMPLEADOS -----------------*/
create procedure sp_empleados
@op char(1) = null,
@id	int = null,
@fecha date = null,
@nombre	varchar(50) = null,
@apellido varchar(50) = null,
@correo	varchar(50) = null,
@telefono varchar(15) = null,
@direccion varchar(50) = null,
@cargo int = null
 as
	begin 
	if(@op = 'L')---- Listar
		begin
			select E.Id, E.Fecha, E.Nombre, E.Apellido, E.Correo, E.Telefono, E.Direccion, C.Cargo
			from Empleados as E
			inner join Cargos as C on E.Cargo = C.Id 
		end
	if(@op = 'I')---- Insertar
		begin
			insert into Empleados values (convert(date, getdate(),103),@nombre,@apellido,@correo,@telefono,@direccion,@cargo)
		end
	if(@op = 'U')---- Actaulizar
		begin
			update Empleados set Nombre=@nombre,Apellido=@apellido,Correo=@correo,Telefono=@telefono,Direccion=@direccion,Cargo=@cargo
			where Id =@id
		end		
	if(@op = 'D')---- Eliminar
		begin
			delete from Usuarios where Empleado = @id
			delete from Empleados where Id = @id
		end
	if(@op = 'C')---- Listar Cargos
		begin
			select * from Cargos
		end
	if(@op = 'B')---- Buscar
		begin
			select * from Empleados where Id = @id
		end
	end
go


/* ---------- PROCEDIMIENTOS PARA USUARIOS -----------------*/
create procedure sp_usuarios
@op char(1) = null,
@usuario varchar(50) = null,
@contrasena varchar(50)= null,
@empleado int = null
as
	begin 
	if(@op = 'L')---- Login
		begin
			select E.Nombre,E.Apellido, C.Cargo, E.Correo from Usuarios as U 
			inner join Empleados as E on U.Empleado=E.Id
			inner join Cargos as C on E.Cargo = C.Id 
			where U.Usuario collate Latin1_General_CS_AS = @usuario and U.Contrasena = @contrasena
		end

	if(@op = 'I')---- Insertar
		begin
			insert into Usuarios values (@usuario,@contrasena,@empleado)
		end

	if(@op = 'U')---- Actualizar
		begin
			update Usuarios set Usuario=@usuario,Contrasena=@contrasena
			where Empleado = @empleado
		end

	if(@op = 'B')---- Buscar
		begin
			 select * from Usuarios where Id = @empleado
		end
	end
go


/* ---------- PROCEDIMIENTOS PARA CLIENTES -----------------*/
create procedure sp_clientes
@op	char(1) = null,
@id	int = null,
@nombre	varchar(50) = null,
@apellido varchar(50) = null,
@cedula	varchar(50) = null,
@telefono varchar(15) = null,
@direccion varchar(50) = null
as
	begin
	if(@op = 'L')---- Listar
		begin
			select * from Clientes
		end

	if(@op = 'I')---- Insertar
		begin
			insert into Clientes values (convert(date, getdate(), 103),@nombre,@apellido,@cedula,@telefono,@direccion)
		end

	if(@op = 'U')---- Actualizar
		begin
			update Clientes set Nombre=@nombre,Apellido=@apellido,Cedula=@cedula,Telefono=@telefono,Direccion=@direccion
			where Id = @id
		end

	if(@op = 'B')---- Buscar
		begin
			select * from Clientes where Id=@id 
		end
	end
go


/* ---------- PROCEDIMIENTOS PARA PRODUCTOS -----------------*/
create procedure sp_productos
@op char(1) = null,
@codigo	varchar(10) = null,
@nombre	varchar(50) = null,
@descripcion text = null,
@stock	int = null,
@precio	float = null,
@estado	int = null
as
	begin
	if(@op = 'L')---- Listar
		begin
			select p.Id, p.Codigo, p.Nombre as[Producto], p.Descripcion, p.Stock, p.Precio, e.Estado
			from Productos as p 
			inner join Estados as e on e.Id = p.Estado
			order by Id asc
		end

	if(@op = 'I')---- Insertar
		begin
			insert into Productos values (@codigo,@nombre,@descripcion,@stock,@precio,@estado)
		end

	if(@op = 'U')---- Actualizar
		begin
			update Productos set Nombre=@nombre,Descripcion=@descripcion,Stock=@stock,Precio=@precio,Estado=@estado
			where Codigo = @codigo
		end

	if(@op = 'B')---- Buscar
		begin
			select * from Productos where Codigo=@codigo or Nombre = @nombre
		end
 
	if(@op = 'E')---- Listar Estados
		begin
			select * from Estados
		end
	
	if(@op = 'A')---- Seleccionar productos con Estado Actuivo y Stock > 0 
		begin 
			select * from Productos where Estado = 1 and Stock > 0 order by Id
		end

	---------------------- Restar existencias a ala hora de vender
	if(@op = 'R') 
		begin 
			update Productos set Stock = Stock - @stock where Codigo = @codigo
		end
	
	---------------------- Sumar existencias a ala hora de vender
	if(@op = 'S') 
		begin 
			update Productos set Stock = Stock + @stock where Codigo = @codigo
		end
	end
go


/*****  PROCESOS PARA FACTURAR UNA VENTA  ******/
/*---------------------------------------------*/

--- ESTRUCTURA PARA EL DETALLLE DE VENTA
create type [dbo].[DetalleFactura] as table(
	[Producto] varchar(10) null,
	[Precio] float null,
	[Unidades] int null,
	[Total] float null
)
go

/* ---------- PROCEDIMIENTOS PARA FACTURA -----------------*/
create procedure sp_factura 
@op char(1) = null,
@cliente int = null,
@empleado int = null,
@montoPago float = null,
@montoCambio float = null,
@montoTotal	float = null,
@DetalleFactura [DetalleFactura] READONLY
as
begin	
	if(@op = 'I')---- INSERTAR
		begin try
			declare @Factura int = 0

			/* INICIAR TRANSACCIÓN */
			begin  transaction registro

			insert into Factura values (convert(date, getdate(),103), @cliente,@empleado,@montoPago,@montoCambio,@montoTotal)

			set @Factura = SCOPE_IDENTITY() /* OBTENER EL ULTIMO ID INSERTADO POR LA COSULTA INSERT INTO FACTURA */

			insert into DetalleFactura(Factura,Producto,Precio,Unidades, Total)
			select @Factura, Producto,Precio, Unidades , Total from @DetalleFactura

			commit transaction registro 
			/* FINALIZAR TRANSACCIÓN */

	     end try
	begin catch
		/* Si ocurre un error borrara todo y no se insertara niguna fila */
		rollback transaction registro
	end catch

	if(@op = 'L')---- LISTAR
		begin 
			select F.Id, convert(char(10), F.Fecha, 103)[Fecha], Cliente, E.Nombre, MontoPago, MontoCambio, MontoTotal 
			from
			Factura as F inner join Empleados as E on F.Empleado = E.Id
		end
  end
go





/* ---------- PROCEDIMIENTOS PARA DETALLE DE FACTURA -----------------*/
create procedure sp_detalle_factura
@op char(1) = null,
@id int = null,
@factura int = null,
@producto varchar(10) = null,
@precio float = null,
@unidades int = null,
@total float = null
as
	begin
	if(@op = 'L')---- LISTAR
		begin
			select DT.Id, Dt.Factura, P.Nombre [Producto], P.Codigo, DT.Precio, DT.Unidades, DT.Total
			from DetalleFactura DT
			inner join Productos P on DT.Producto = P.Codigo
			where Factura = @factura
		end
	end
go


/* ---------- PROCEDIMIENTOS PARA GENERAR REPORTES DE VENTAS -----------------*/
create proc sp_reporteVentas
@fechaInicio varchar(10)= null, 
@fechaFin varchar(10)= null 
as
	begin
		SET DATEFORMAT dmy;

		select CONVERT(char(10), F.Fecha, 103)[Fecha_Ingreso], E.Nombre [Empleado], C.Nombre[Cliente], P.Codigo,
		P.Nombre [Producto], P.Descripcion, DT.Precio, DT.Unidades, Dt.Total
		
		from Factura F
		inner join Empleados as E on F.Empleado = E.Id
		inner join DetalleFactura as DT on DT.Factura = F.Id
		inner join Productos as P on P.Codigo = DT.Producto
		inner join Clientes as C on F.Cliente = C.Id
		where F.Fecha between @fechaInicio and @fechaFin
	end
go


/************* (^_^) FIN (^_^) ***************/
/*-------------------------------------------*/










 
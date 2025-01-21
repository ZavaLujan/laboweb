USE [master]
GO
/****** Object:  Database [DBCatalogo]    Script Date: 1/21/2025 12:57:53 PM ******/
CREATE DATABASE [DBCatalogo]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DBCatalogo', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\DBCatalogo.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DBCatalogo_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\DBCatalogo_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [DBCatalogo] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DBCatalogo].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DBCatalogo] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DBCatalogo] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DBCatalogo] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DBCatalogo] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DBCatalogo] SET ARITHABORT OFF 
GO
ALTER DATABASE [DBCatalogo] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DBCatalogo] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DBCatalogo] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DBCatalogo] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DBCatalogo] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DBCatalogo] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DBCatalogo] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DBCatalogo] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DBCatalogo] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DBCatalogo] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DBCatalogo] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DBCatalogo] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DBCatalogo] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DBCatalogo] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DBCatalogo] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DBCatalogo] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DBCatalogo] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DBCatalogo] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DBCatalogo] SET  MULTI_USER 
GO
ALTER DATABASE [DBCatalogo] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DBCatalogo] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DBCatalogo] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DBCatalogo] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DBCatalogo] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DBCatalogo] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [DBCatalogo] SET QUERY_STORE = ON
GO
ALTER DATABASE [DBCatalogo] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [DBCatalogo]
GO
/****** Object:  Table [dbo].[Categoria]    Script Date: 1/21/2025 12:57:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categoria](
	[IdCategoria] [int] IDENTITY(1,1) NOT NULL,
	[DescCategoria] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Categoria] PRIMARY KEY CLUSTERED 
(
	[IdCategoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Producto]    Script Date: 1/21/2025 12:57:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producto](
	[IdProducto] [int] IDENTITY(1,1) NOT NULL,
	[IdCategoria] [int] NOT NULL,
	[NomProducto] [varchar](120) NOT NULL,
	[MarcaProducto] [varchar](120) NOT NULL,
	[ModeloProducto] [varchar](120) NOT NULL,
	[LineaProducto] [varchar](120) NOT NULL,
	[GarantiaProducto] [varchar](50) NULL,
	[Precio] [decimal](18, 2) NULL,
	[Imagen] [varbinary](max) NULL,
	[DescripcionTecnica] [text] NULL,
 CONSTRAINT [PK_Producto] PRIMARY KEY CLUSTERED 
(
	[IdProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rol]    Script Date: 1/21/2025 12:57:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rol](
	[IdRol] [int] IDENTITY(1,1) NOT NULL,
	[DesRol] [varchar](80) NOT NULL,
 CONSTRAINT [PK_Rol] PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stock]    Script Date: 1/21/2025 12:57:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stock](
	[IdProducto] [int] NOT NULL,
	[StockItems] [int] NOT NULL,
	[PuntoRepo] [int] NULL,
	[PrecioVenta] [decimal](18, 2) NULL,
 CONSTRAINT [PK_Stock] PRIMARY KEY CLUSTERED 
(
	[IdProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 1/21/2025 12:57:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[CodUsuario] [varchar](50) NOT NULL,
	[Clave] [binary](50) NOT NULL,
	[Nombres] [varchar](50) NOT NULL,
	[IdRol] [int] NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Categoria] ON 

INSERT [dbo].[Categoria] ([IdCategoria], [DescCategoria]) VALUES (1, N'Generadores')
INSERT [dbo].[Categoria] ([IdCategoria], [DescCategoria]) VALUES (2, N'Descargadores')
INSERT [dbo].[Categoria] ([IdCategoria], [DescCategoria]) VALUES (3, N'Transformadotres')
SET IDENTITY_INSERT [dbo].[Categoria] OFF
GO
SET IDENTITY_INSERT [dbo].[Producto] ON 

INSERT [dbo].[Producto] ([IdProducto], [IdCategoria], [NomProducto], [MarcaProducto], [ModeloProducto], [LineaProducto], [GarantiaProducto], [Precio], [Imagen], [DescripcionTecnica]) VALUES (2, 1, N'ABB Ability Smart Sensor - Motores', N'ABB Sensores', N'Gran Mineria', N'IOT Smart', N'Cinco Años', CAST(45000.00 AS Decimal(18, 2)), NULL, N'ABB ha desarrollado un sensor compacto que está unido al bastidor de motores de inducción de bajo voltaje. No es necesario ningún cableado. El uso de algoritmos de a bordo, basados en décadas de experiencia en motores de ABB, permite al pequeño sensor inteligente informarnos sobre el estado del motor a través de un teléfono móvil y a través de internet a un servidor seguro. Esta solución puede hacer que un gran número de motores se puedan beneficiar de estos dispositivos inteligentes')
INSERT [dbo].[Producto] ([IdProducto], [IdCategoria], [NomProducto], [MarcaProducto], [ModeloProducto], [LineaProducto], [GarantiaProducto], [Precio], [Imagen], [DescripcionTecnica]) VALUES (3, 1, N'IE3 MOTORS', N'ABB -Motores', N'Motor IE3', N'Motor a Explosion', N'6 Años', CAST(20000.00 AS Decimal(18, 2)), NULL, N'ABB, the leading power and automation technology group, is expanding its range of low voltage cast iron IE3 motors for explosive atmospheres with the introduction of new mid-range products')
INSERT [dbo].[Producto] ([IdProducto], [IdCategoria], [NomProducto], [MarcaProducto], [ModeloProducto], [LineaProducto], [GarantiaProducto], [Precio], [Imagen], [DescripcionTecnica]) VALUES (4, 2, N'High-voltage surge arresters', N'ABB-DC Up', N'1100 KV', N'IEC 60099-4 ', N'5 Años', CAST(35000.00 AS Decimal(18, 2)), NULL, N'ABB surge arresters are the primary protection against atmospheric and switching over voltages. ABB offers a complete range of surge arresters for high voltage applications comprising of solutions for AC and DC up to 1,100kV as well as special applications. The portfolio includes porcelain housed, silicone housed or SF6-insulated high voltage arresters. ABB has more than 75 years of experience of overvoltage protection and more than 25 years of experience with silicone insulation. The arresters are installed all over the world in all type of climates. The designs are type tested according to the IEC 60099-4 , ANSI/IEEEC62.11 and also comply with customer specific standards.')
INSERT [dbo].[Producto] ([IdProducto], [IdCategoria], [NomProducto], [MarcaProducto], [ModeloProducto], [LineaProducto], [GarantiaProducto], [Precio], [Imagen], [DescripcionTecnica]) VALUES (5, 2, N'High-voltage surge arresters II', N'ABB - Proteccion', N'Device - 520', N'Proteccion de equipos', N'3 Años', CAST(25000.00 AS Decimal(18, 2)), NULL, N'Surge arresters are used for protection of electrical equipment from all kind of overvoltages caused by lightening or switching operations. ABB has more than 100 years of experience in designing and manufacturing surge arresters and protection devices.')
INSERT [dbo].[Producto] ([IdProducto], [IdCategoria], [NomProducto], [MarcaProducto], [ModeloProducto], [LineaProducto], [GarantiaProducto], [Precio], [Imagen], [DescripcionTecnica]) VALUES (6, 3, N'Small distribution transformers', N'ABB- Networks', N'Oil Inmersed', N'Economico', N'2 Años', CAST(10000.00 AS Decimal(18, 2)), NULL, N'Small distribution transformers are typically oil-immersed and suitable for pole-, pad- or ground-mounting. They represent an economical option for certain networks, particularly those with low population densities')
INSERT [dbo].[Producto] ([IdProducto], [IdCategoria], [NomProducto], [MarcaProducto], [ModeloProducto], [LineaProducto], [GarantiaProducto], [Precio], [Imagen], [DescripcionTecnica]) VALUES (7, 3, N'Medium distribution transformers', N'ABB- three-phase ', N'Alto Voltaje', N'Transformadores', N'6 Años', CAST(30000.00 AS Decimal(18, 2)), NULL, N'Medium distribution transformers are used to step down three-phase high voltage to low voltage for energy distribution, mainly in the countryside or low-density populated areas')
INSERT [dbo].[Producto] ([IdProducto], [IdCategoria], [NomProducto], [MarcaProducto], [ModeloProducto], [LineaProducto], [GarantiaProducto], [Precio], [Imagen], [DescripcionTecnica]) VALUES (8, 3, N'Fit for purpose distribution transformers', N'ABB- Fit for purpose', N'Areas Criticas', N'Transfomadores ', N'7 Años', CAST(25000.00 AS Decimal(18, 2)), NULL, N'Distribution transformers are critical equipment for renewables and ABB has the world’s largest installed base of transformers providing more renewable energy than any other supplier. “Fit for purpose” distribution transformers offer application-specific units that have different design features compared to standard distribution transformers')
INSERT [dbo].[Producto] ([IdProducto], [IdCategoria], [NomProducto], [MarcaProducto], [ModeloProducto], [LineaProducto], [GarantiaProducto], [Precio], [Imagen], [DescripcionTecnica]) VALUES (14, 1, N'AEAAA Ability Smart Sensor - Motores', N'ABB Sensores', N'Gran Mineria', N'IOT Smart', N'Cinco Años', CAST(45000.00 AS Decimal(18, 2)), NULL, N'ABB ha desarrollado un sensor compacto que está unido al bastidor de motores de inducción de bajo voltaje. No es necesario ningún cableado. El uso de algoritmos de a bordo, basados en décadas de experiencia en motores de ABB, permite al pequeño sensor inteligente informarnos sobre el estado del motor a través de un teléfono móvil y a través de internet a un servidor seguro. Esta solución puede hacer que un gran número de motores se puedan beneficiar de estos dispositivos inteligentes')
INSERT [dbo].[Producto] ([IdProducto], [IdCategoria], [NomProducto], [MarcaProducto], [ModeloProducto], [LineaProducto], [GarantiaProducto], [Precio], [Imagen], [DescripcionTecnica]) VALUES (15, 1, N'CPP Ability Smart Sensor - Motores', N'ABB Sensores', N'Gran Mineria', N'IOT Smart', N'Cinco Años', CAST(45000.00 AS Decimal(18, 2)), NULL, N'ABB ha desarrollado un sensor compacto que está unido al bastidor de motores de inducción de bajo voltaje. No es necesario ningún cableado. El uso de algoritmos de a bordo, basados en décadas de experiencia en motores de ABB, permite al pequeño sensor inteligente informarnos sobre el estado del motor a través de un teléfono móvil y a través de internet a un servidor seguro. Esta solución puede hacer que un gran número de motores se puedan beneficiar de estos dispositivos inteligentes')
SET IDENTITY_INSERT [dbo].[Producto] OFF
GO
SET IDENTITY_INSERT [dbo].[Rol] ON 

INSERT [dbo].[Rol] ([IdRol], [DesRol]) VALUES (1, N'Supervisor Grupo 01')
INSERT [dbo].[Rol] ([IdRol], [DesRol]) VALUES (2, N'Vendedor Grupo 01')
INSERT [dbo].[Rol] ([IdRol], [DesRol]) VALUES (3, N'Supervisor Grupo 02')
INSERT [dbo].[Rol] ([IdRol], [DesRol]) VALUES (4, N'Vendedor Grupo 02')
SET IDENTITY_INSERT [dbo].[Rol] OFF
GO
INSERT [dbo].[Stock] ([IdProducto], [StockItems], [PuntoRepo], [PrecioVenta]) VALUES (2, 80, 20, CAST(30000.00 AS Decimal(18, 2)))
INSERT [dbo].[Stock] ([IdProducto], [StockItems], [PuntoRepo], [PrecioVenta]) VALUES (3, 100, 20, CAST(25000.00 AS Decimal(18, 2)))
INSERT [dbo].[Stock] ([IdProducto], [StockItems], [PuntoRepo], [PrecioVenta]) VALUES (4, 90, 10, CAST(40000.00 AS Decimal(18, 2)))
INSERT [dbo].[Stock] ([IdProducto], [StockItems], [PuntoRepo], [PrecioVenta]) VALUES (5, 60, 10, CAST(35000.00 AS Decimal(18, 2)))
INSERT [dbo].[Stock] ([IdProducto], [StockItems], [PuntoRepo], [PrecioVenta]) VALUES (6, 80, 10, CAST(25000.00 AS Decimal(18, 2)))
INSERT [dbo].[Stock] ([IdProducto], [StockItems], [PuntoRepo], [PrecioVenta]) VALUES (7, 70, 10, CAST(30000.00 AS Decimal(18, 2)))
INSERT [dbo].[Stock] ([IdProducto], [StockItems], [PuntoRepo], [PrecioVenta]) VALUES (8, 90, 10, CAST(35000.00 AS Decimal(18, 2)))
GO
SET IDENTITY_INSERT [dbo].[Usuario] ON 

INSERT [dbo].[Usuario] ([IdUsuario], [CodUsuario], [Clave], [Nombres], [IdRol]) VALUES (1, N'despinoza', 0x7A00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000, N'Espinoza Robles Armando', 1)
INSERT [dbo].[Usuario] ([IdUsuario], [CodUsuario], [Clave], [Nombres], [IdRol]) VALUES (3, N'ggutierrez', 0x7A00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000, N'gladys gutierrez', 3)
INSERT [dbo].[Usuario] ([IdUsuario], [CodUsuario], [Clave], [Nombres], [IdRol]) VALUES (4, N'jayquipa', 0x7A00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000, N'julia Ayquipa', 2)
INSERT [dbo].[Usuario] ([IdUsuario], [CodUsuario], [Clave], [Nombres], [IdRol]) VALUES (5, N'mzegarra', 0x7A00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000, N'Zegarra Aliaga', 2)
INSERT [dbo].[Usuario] ([IdUsuario], [CodUsuario], [Clave], [Nombres], [IdRol]) VALUES (7, N'gsalinas', 0x7A00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000, N'Salinas Asaña Gilberto', 4)
INSERT [dbo].[Usuario] ([IdUsuario], [CodUsuario], [Clave], [Nombres], [IdRol]) VALUES (8, N'jchavez', 0x7A00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000, N'Chavez saume Julio', 4)
INSERT [dbo].[Usuario] ([IdUsuario], [CodUsuario], [Clave], [Nombres], [IdRol]) VALUES (9, N'jsalazar', 0xD800000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000, N'Jose Salazar Lujan', 2)
INSERT [dbo].[Usuario] ([IdUsuario], [CodUsuario], [Clave], [Nombres], [IdRol]) VALUES (10, N'otorres', 0x7A00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000, N'Omar torres Willians', 4)
INSERT [dbo].[Usuario] ([IdUsuario], [CodUsuario], [Clave], [Nombres], [IdRol]) VALUES (11, N'jsalazar', 0x7A00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000, N'Jose Salazar', 2)
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [FK_Producto_Categoria] FOREIGN KEY([IdCategoria])
REFERENCES [dbo].[Categoria] ([IdCategoria])
GO
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [FK_Producto_Categoria]
GO
ALTER TABLE [dbo].[Stock]  WITH CHECK ADD  CONSTRAINT [FK_Stock_Producto] FOREIGN KEY([IdProducto])
REFERENCES [dbo].[Producto] ([IdProducto])
GO
ALTER TABLE [dbo].[Stock] CHECK CONSTRAINT [FK_Stock_Producto]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Rol1] FOREIGN KEY([IdRol])
REFERENCES [dbo].[Rol] ([IdRol])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Rol1]
GO
/****** Object:  StoredProcedure [dbo].[ListarRol]    Script Date: 1/21/2025 12:57:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ListarRol]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT IdRol, DesRol from Rol 
END
GO
/****** Object:  StoredProcedure [dbo].[ListarUsuarios]    Script Date: 1/21/2025 12:57:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ListarUsuarios] 
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT IdUsuario, CodUsuario, Clave, Nombres, u.IdRol, r.DesRol from Usuario u, Rol r
	where u.IdRol = r.IdRol
END


GO
/****** Object:  StoredProcedure [dbo].[paActualizarProducto]    Script Date: 1/21/2025 12:57:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[paActualizarProducto]
    @IdProducto int,
    @IdCategoria int,
    @NomProducto varchar(120),
    @MarcaProducto varchar(120),
    @ModeloProducto varchar(120),
    @LineaProducto varchar(120),
    @GarantiaProducto varchar(50),
    @Precio decimal(18,2),
    @Imagen image,
    @DescripcionTecnica text
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        -- Verificar si el producto existe
        IF NOT EXISTS (SELECT 1 FROM Producto WHERE IdProducto = @IdProducto)
        BEGIN
            RAISERROR('El producto especificado no existe.', 16, 1)
            RETURN 0
        END

        -- Verificar si la categoría existe
        IF NOT EXISTS (SELECT 1 FROM Categoria WHERE IdCategoria = @IdCategoria)
        BEGIN
            RAISERROR('La categoría especificada no existe.', 16, 1)
            RETURN 0
        END

        UPDATE Producto
        SET IdCategoria = @IdCategoria,
            NomProducto = @NomProducto,
            MarcaProducto = @MarcaProducto,
            ModeloProducto = @ModeloProducto,
            LineaProducto = @LineaProducto,
            GarantiaProducto = @GarantiaProducto,
            Precio = @Precio,
            Imagen = @Imagen,
            DescripcionTecnica = @DescripcionTecnica
        WHERE IdProducto = @IdProducto
        
        RETURN 1
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE()
        RAISERROR(@ErrorMessage, 16, 1)
        RETURN 0
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[paEliminarProducto]    Script Date: 1/21/2025 12:57:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Eliminar un producto
CREATE PROCEDURE [dbo].[paEliminarProducto]
    @IdProducto int
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        BEGIN TRANSACTION
            -- Primero eliminamos el registro en la tabla Stock si existe
            IF EXISTS (SELECT 1 FROM Stock WHERE IdProducto = @IdProducto)
            BEGIN
                DELETE FROM Stock WHERE IdProducto = @IdProducto
            END
            
            -- Luego eliminamos el producto
            DELETE FROM Producto WHERE IdProducto = @IdProducto
            
        COMMIT TRANSACTION
        RETURN 1
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION
            
        -- Puedes agregar logging del error aquí si lo deseas
        -- SELECT ERROR_MESSAGE() AS ErrorMessage
        
        RETURN 0
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[paInsertarProducto]    Script Date: 1/21/2025 12:57:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[paInsertarProducto]
    @IdCategoria int,
    @NomProducto varchar(120),
    @MarcaProducto varchar(120),
    @ModeloProducto varchar(120),
    @LineaProducto varchar(120),
    @GarantiaProducto varchar(50),
    @Precio decimal(18,2),
    @Imagen image,
    @DescripcionTecnica text,
    @IdProducto int OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        INSERT INTO Producto (IdCategoria, NomProducto, MarcaProducto, ModeloProducto, LineaProducto, 
                             GarantiaProducto, Precio, Imagen, DescripcionTecnica)
        VALUES (@IdCategoria, @NomProducto, @MarcaProducto, @ModeloProducto, @LineaProducto,
                @GarantiaProducto, @Precio, @Imagen, @DescripcionTecnica)
        
        SET @IdProducto = SCOPE_IDENTITY()
        RETURN 1
    END TRY
    BEGIN CATCH
        RETURN 0
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[paListarCategorias]    Script Date: 1/21/2025 12:57:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[paListarCategorias] 
AS
BEGIN
    SET NOCOUNT ON;

    SELECT IdCategoria, DescCategoria 
    FROM Categoria
END
GO
/****** Object:  StoredProcedure [dbo].[paListarProductos]    Script Date: 1/21/2025 12:57:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[paListarProductos] 
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [IdProducto], p.[IdCategoria],[DescCategoria],[NomProducto] , [MarcaProducto],
	[ModeloProducto],[LineaProducto],[GarantiaProducto],[Precio],[Imagen],[DescripcionTecnica]
	from Producto p, Categoria c
	where p.IdCategoria = c.IdCategoria
END



GO
/****** Object:  StoredProcedure [dbo].[paObtenerProducto]    Script Date: 1/21/2025 12:57:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Obtener un producto específico
CREATE PROCEDURE [dbo].[paObtenerProducto]
    @IdProducto int
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT p.IdProducto, p.IdCategoria, c.DescCategoria, p.NomProducto, p.MarcaProducto,
           p.ModeloProducto, p.LineaProducto, p.GarantiaProducto, p.Precio, p.Imagen, p.DescripcionTecnica
    FROM Producto p
    INNER JOIN Categoria c ON p.IdCategoria = c.IdCategoria
    WHERE p.IdProducto = @IdProducto
END
GO
/****** Object:  StoredProcedure [dbo].[paUsuario_BuscaCodUserClave]    Script Date: 1/21/2025 12:57:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[paUsuario_BuscaCodUserClave] 
	-- Add the parameters for the stored procedure here
	@ParamPass binary,
	@ParamUsuario varchar(40)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT IdUsuario,CodUsuario,Nombres From Usuario
	Where CodUsuario = @ParamUsuario and Clave = @ParamPass
END


GO
/****** Object:  StoredProcedure [dbo].[paUsuario_BuscaUserId]    Script Date: 1/21/2025 12:57:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[paUsuario_BuscaUserId] 
	-- Add the parameters for the stored procedure here
	@ParamUsuario int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT IdUsuario,CodUsuario,Nombres,u.IdRol,DesRol From Usuario u, Rol r
	Where u.IdRol= r.IdRol and  IdUsuario = @ParamUsuario 
END


GO
/****** Object:  StoredProcedure [dbo].[paUsuario_Eliminar]    Script Date: 1/21/2025 12:57:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Stored Procedure para eliminar usuario
CREATE PROCEDURE [dbo].[paUsuario_Eliminar]
    @IdUsuario int
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM Usuario WHERE IdUsuario = @IdUsuario
END
GO
/****** Object:  StoredProcedure [dbo].[paUsuario_insertar]    Script Date: 1/21/2025 12:57:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[paUsuario_insertar]
	@Clave binary,
	@CodUsuario varchar(50), 
	@Nombres varchar(50),
	@IdRol int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Insert into Usuario (CodUsuario, Clave,Nombres,IdRol)
	Values (@CodUsuario, @Clave, @Nombres,@IdRol)
END


GO
/****** Object:  StoredProcedure [dbo].[paUsuario_Modificar]    Script Date: 1/21/2025 12:57:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[paUsuario_Modificar]
	-- Add the parameters for the stored procedure here
	@IdUsuario int,
	@CodUsuario Varchar(50) ,
	@Clave binary, 
	@Nombres Varchar(80),
	@IdRol int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	update Usuario
	set
		CodUsuario=@CodUsuario,
		Clave = @Clave,
		Nombres=@Nombres,
		IdRol =@IdRol
	Where IdUsuario =@IdUsuario
END


GO
USE [master]
GO
ALTER DATABASE [DBCatalogo] SET  READ_WRITE 
GO

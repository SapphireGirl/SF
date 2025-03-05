USE [SF]
GO

--CREATE TABLE dbo.Homes(
--		ID INT IDENTITY NOT NULL PRIMARY KEY,
--		Address	NVARCHAR(50),
--		Price MONEY,
--		City NVARCHAR(50),
--		State NVARCHAR(50),
--		ZipCode INT,
--		Comments VARCHAR(MAX),
--		Image VARCHAR(100),
--		Url VARCHAR(50))


-- ID is the name of the  [to be] identity column
--ALTER TABLE dbo.Homes DROP COLUMN ID 
--ALTER TABLE dbo.Homes ADD ID INT IDENTITY(1,1)


--ALTER TABLE dbo.Homes
--   ADD ID INT IDENTITY
--       CONSTRAINT PK_Homes PRIMARY KEY CLUSTERED

---------------------------------------
-- Seed Data
-----------------------------------------
INSERT INTO [dbo].[Homes]
           ([Address]
           ,[Price]
           ,[ZipCode]
           ,[City]
           ,[State]
           ,[Url]
           ,[Comments])
     VALUES
           ('406B Los Pinos Rd'
           ,320000
           ,87507
           ,'Santa Fe'
           ,'New Mexico'
           ,'LosPinos_Url'
           ,'Run Down')
GO


INSERT INTO [dbo].[Homes]
           ([Address]
           ,[Price]
           ,[ZipCode]
           ,[City]
           ,[State]
           ,[Url]
           ,[Comments])
     VALUES
           ('4 Calle Las Casas'
           ,409000
           ,87507
           ,'Santa Fe'
           ,'New Mexico'
           ,'Calle_Url'
           ,'Small House')
GO

INSERT INTO [dbo].[Homes]
           ([Address]
           ,[Price]
           ,[ZipCode]
           ,[City]
           ,[State]
           ,[Url]
           ,[Comments])
     VALUES
           ('183 Sunrise Rd'
           ,585
           ,87507
           ,'Santa Fe'
           ,'New Mexico'
           ,'Sunrise_Url'
           ,'Nothing Special')
GO

INSERT INTO [dbo].[Homes]
           ([Address]
           ,[Price]
           ,[ZipCode]
           ,[City]
           ,[State]
           ,[Url]
           ,[Comments])
     VALUES
           ('3166 Plaza Blanca'
           ,500000
           ,87507
           ,'Santa Fe'
           ,'New Mexico'
           ,'PlazaBlanca_Url'
           ,'Cute Condo!')
GO

INSERT INTO [dbo].[Homes]
           ([Address]
           ,[Price]
           ,[ZipCode]
           ,[City]
           ,[State]
           ,[Url]
           ,[Comments])
     VALUES
           ('2804 Camino Mendoza'
           ,649000
           ,87507
           ,'Santa Fe'
           ,'New Mexico'
           ,'CaminoMendoza_Url'
           ,'Nice! 0.56 Acres 2 Stories with 3 bedrooms and 3 bathrooms')
GO

INSERT INTO [dbo].[Homes]
           ([Address]
           ,[Price]
           ,[ZipCode]
           ,[City]
           ,[State]
           ,[Url]
           ,[Comments])
     VALUES
           ('4654 Camino Cuervo'
           ,375000
           ,87507
           ,'Santa Fe'
           ,'New Mexico'
           ,'CaminoCuervo_Url'
           ,'Cute Town House, 3 bedrooms, 2 baths, 1133 sqft')
GO

INSERT INTO [dbo].[Homes]
           ([Address]
           ,[Price]
           ,[ZipCode]
           ,[City]
           ,[State]
           ,[Url]
           ,[Comments])
     VALUES
           ('7616 Mesa Del Oro Ln'
           ,409000
           ,87507
           ,'Santa Fe'
           ,'New Mexico'
           ,'MesaDelOro_Url'
           ,'House 3 bedrooms, 2 bathrooms 1458 sqft')
GO

INSERT INTO [dbo].[Homes]
           ([Address]
           ,[Price]
           ,[ZipCode]
           ,[City]
           ,[State]
           ,[Url]
           ,[Comments])
     VALUES
           ('941 Calle Mejia APT 1505'
           ,409000
           ,87507
           ,'Santa Fe'
           ,'New Mexico'
           ,'CalleMejia_Url'
           ,'Small Apt 789 sqft, 2 bedrooms, 2 bathrooms')

GO


INSERT INTO [dbo].[Homes]
           ([Address]
           ,[Price]
           ,[ZipCode]
           ,[City]
           ,[State]
           ,[Url]
           ,[Comments])
     VALUES
           ('1405 Vegas Verdes Dr UNIT 304'
           ,409000
           ,87507
           ,'Santa Fe'
           ,'New Mexico'
           ,'VegasVerdes_Url'
           ,'Condo, 1171 sqft, 2 bedrooms, 2 bathrooms, fireplace')
GO
SELECT * FROM dbo.Homes

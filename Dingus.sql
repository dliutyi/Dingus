CREATE DATABASE Dingus

CREATE TABLE DashboardItems
(
	Id int NOT NULL IDENTITY PRIMARY KEY,
	Name nvarchar(255) NOT NULL,
	Alias nvarchar(255) NOT NULL
)

CREATE TABLE Country
(
	Id int NOT NULL IDENTITY PRIMARY KEY,
	Name nvarchar(64) NOT NULL,
	Code nvarchar(8) NOT NULL
)

CREATE TABLE Users
(
	Id int NOT NULL IDENTITY PRIMARY KEY,
	Login nvarchar(32) NOT NULL,
	EMail nvarchar(255) NOT NULL,
	Phone nvarchar(255) NOT NULL,
	Password nvarchar(64) NOT NULL,
	Salt nvarchar(32) NOT NULL,
	FirstName nvarchar(64) NOT NULL,
	LastName nvarchar(64) NOT NULL
)

INSERT INTO Users VALUES ('admin', 'ljutyjj.dima@gmail.com', '982627331', 'EF4CD2C99D01E4551AA1331A988AADFD8A03FF354458186EC9800CD6E3EFC132', '0b156147a1c74bb4915692cf714b0eed', 'Admin', 'Admin')

INSERT INTO Country VALUES ('Ukraine', '380'), ('USA', '1'), ('Germany', '49')



INSERT INTO DashboardItems VALUES ('Dashboard', 'Dashboard'), 
                          ('Accounts', 'Accounts'),
						  ('Recent purchases', 'Purchases'),
						  ('Companies', 'Companies'),
						  ('Profit calculation', 'Calculation'),
						  ('Settings', 'Settings'),
						  ('Sign Out', 'SignOut')

SELECT * FROM Users
SELECT * FROM DashboardItems

DROP TABLE Users
DROP TABLE DashboardItems

DBCC CHECKIDENT('Users', RESEED, 0)

CountryId int FOREIGN KEY REFERENCES Country(Id),

CREATE DATABASE DD_QLQuanNet
GO
USE DD_QLQuanNet
GO
CREATE TABLE Users
(
	User_ID INT IDENTITY(1,1),
	Username NVARCHAR(50) NOT NULL,
	PasswordHash NVARCHAR(255) NOT NULL,
	Balance DECIMAL(10,0) DEFAULT 0,
	Role NVARCHAR(50) NOT NULL DEFAULT 'Member',
	Status NVARCHAR(10) NOT NULL DEFAULT 'Allowed'
	CONSTRAINT PK_Users PRIMARY KEY(User_ID),
	CONSTRAINT CK_Users_Role CHECK (Role IN ('Admin', 'Member', 'Staff')),
	CONSTRAINT CK_Users_Status CHECK (Status IN ('Allowed', 'Banned')),
	CONSTRAINT UQ_Users_Username UNIQUE(Username)
)
GO

CREATE TABLE Customers
(
	Customer_ID INT IDENTITY(1,1),
	Full_Name NVARCHAR(255) NULL,
	Birthdate DATE NULL,
	Gender NVARCHAR(10) NULL, 
	Email NVARCHAR(255) NULL,
	Address NVARCHAR(500) NULL,
	Phone NVARCHAR(20) NULL, 
	User_ID INT NOT NULL,
	CONSTRAINT PK_Customers PRIMARY KEY(Customer_ID),
	CONSTRAINT FK_Customers_Users FOREIGN KEY(User_ID) REFERENCES Users(User_ID),
	CONSTRAINT CK_Customers_Gender CHECK (Gender IN ('Male', 'Female', 'Order')),
	CONSTRAINT CK_Customers_Phone CHECK (Phone LIKE '[0-9]%' OR Phone IS NULL)
)


CREATE TABLE Stations
(
	Station_ID INT IDENTITY(1,1),
	Station_Name NVARCHAR(6),
	Status NVARCHAR(50),
	Type NVARCHAR(50),
	Price_Per_Hour DECIMAL(10,3),
	User_ID INT,
	CONSTRAINT PK_Stations PRIMARY KEY(Station_ID),
	CONSTRAINT FK_Stations_Users FOREIGN KEY(User_ID) REFERENCES Users(User_ID),
	CONSTRAINT CK_Stations_Status CHECK (Status IN ('Available', 'In Use', 'Maintenance'))
)
GO

CREATE TABLE Services
(
	Service_ID INT IDENTITY(1,1),
	Service_Name NVARCHAR(50),
	Price DECIMAL(10,3),
	Category INT,
	Description NVARCHAR(255),
	Image NVARCHAR(255),
	CONSTRAINT PK_Services PRIMARY KEY(Service_ID),
)
GO

CREATE TABLE Invoices
(
	Invoice_ID INT IDENTITY(1,1),
	Date DATETIME,
	Total DECIMAL(10,3),
	User_ID INT,
	CONSTRAINT PK_Invoices PRIMARY KEY(Invoice_ID),
	CONSTRAINT FK_Invoices_Users FOREIGN KEY(User_ID) REFERENCES Users(User_ID)
)
GO

CREATE TABLE Orders
(
	Order_ID INT IDENTITY(1,1),
	Order_Date DATETIME,
	Quantity INT,
	TotalCost DECIMAL(10,3),
	User_ID INT,
	Service_ID INT,
	Station_ID INT,
	Invoice_ID INT,
	CONSTRAINT PK_Orders PRIMARY KEY(Order_ID),
	CONSTRAINT FK_Orders_Users FOREIGN KEY(User_ID) REFERENCES Users(User_ID),
	CONSTRAINT FK_Orders_Services FOREIGN KEY(Service_ID) REFERENCES Services(Service_ID),
	CONSTRAINT FK_Orders_Stations FOREIGN KEY(Station_ID) REFERENCES Stations(Station_ID),
	CONSTRAINT FK_Orders_Invoices FOREIGN KEY(Invoice_ID) REFERENCES Invoices(Invoice_ID)
)
GO

CREATE TABLE Reports
(
	Report_ID INT IDENTITY(1,1),
	StartTime DATETIME,
	EndTime DATETIME,
	Total_Recharge DECIMAL(10,3),
	Total_Service DECIMAL(10,3),
	User_ID INT,
	CONSTRAINT PK_Reports PRIMARY KEY(Report_ID),
	CONSTRAINT FK_Reports_Users FOREIGN KEY(User_ID) REFERENCES Users(User_ID)
)
GO
CREATE TABLE TopUps
(
	TopUp_ID INT IDENTITY(1,1),
	TopUp_Date DATETIME,
	Amount DECIMAL(10,3),
	Description NVARCHAR(255),
	User_ID INT,
	CONSTRAINT PK_TopUps PRIMARY KEY(TopUp_ID),
	CONSTRAINT FK_TopUps_Users FOREIGN KEY(User_ID) REFERENCES dbo.Users(User_ID)
)
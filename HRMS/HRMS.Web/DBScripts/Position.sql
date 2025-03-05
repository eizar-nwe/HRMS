Create Database HRMS;

Use HRMS;
Create Table Position(
Id char(36) Primary Key,
Code nvarchar(200) Not NULL,
Description nvarchar(255) NOT NULL,
Level int,
CreatedAt DateTime NOT NULL,
Createdby CHAR(36) NOT NULL,
UpdatedAt DateTime,
UpdatedBy CHAR(36),
Ip char(255) NOT NULL,
IsActive bit NOT NULL
);


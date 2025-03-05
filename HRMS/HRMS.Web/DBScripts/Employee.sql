
Use HRMS;
create Table Employee(
Id char(36) Primary Key,
Code nvarchar(255) Not Null,
[Name] nvarchar(255) Not Null,
[Email] char(255) Not Null,
Gender char(1) Not Null,
DOB DateTime Not Null,
DOE DateTime Not Null,
DOR DateTime,
[Address] text,
BasicSalary decimal(18,2) Not Null,
Phone nvarchar(255) Not Null,
DepartmentId char(36) Not Null,
PositionId char(36) Not Null,

CreatedAt DateTime Not Null,
CreatedBy char(36) Not NUll,
UpdatedAt DateTime,
UpdatedBy char(36),
[Ip] char(255) Not Null,
IsActive bit Not Null,
constraint fk_department_employee FOREIGN KEY (DepartmentId) references Department(Id),
constraint fk_position_employee FOREIGN KEY (PositionId) references Position(Id),
);

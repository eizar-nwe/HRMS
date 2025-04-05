Create Table AttendanceMaster(
ID Char(36) Primary Key,
AttendanceDate DateTime Not Null,
InTime Time Not Null,
OutTime Time Not Null,
EmployeeId char(36) Not Null,
DepartmentId char(36) Not Null,
ShiftId Char(36) Not Null,
IsLate bit Not Null,
IsEarlyOut bit Not Null,
IsLeave bit Not Null,

CreatedAt DateTime Not Null,
CreatedBy char(36) Not NUll,
UpdatedAt DateTime,
UpdatedBy char(36),
[Ip] char(255) Not Null,
IsActive bit Not Null,

Constraint fk_employee_attMstr Foreign Key (EmployeeId) References Employee(id),
Constraint fk_department_attmstr Foreign Key (DepartmentId) REferences Department(Id),
Constraint fk_shift_attMstr Foreign Key (ShiftId) References Shift(Id),
)
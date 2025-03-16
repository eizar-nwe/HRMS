Use HRMS;
create Table Shift(
Id char(36) Primary Key,
Name nvarchar(255) Not Null,
InTime Time Not Null,
OutTime Time Not Null,
LateAfter Time Not Null,
EarlyOutBefore Time Not Null,
AttendancePolicyId char(36) Not Null,
CreatedAt DateTime Not Null,
CreatedBy char(36) Not NUll,
UpdatedAt DateTime,
UpdatedBy char(36),
[Ip] char(255) Not Null,
IsActive bit Not Null
constraint fk_attendancePolicy_shift FOREIGN KEY (AttendancePolicyId) references AttendancePolicy(Id),
);


Use HRMS;
Create Table AttendancePolicy(
	Id Char(36) Primary Key,
	Name nvarchar(255) Not Null,
	NumberOfLateTime Int Not Null,
	NumberOfEarlyOutTime Int Not Null,
	DeductionInAmount decimal(18,2) Not Null,
	DeductionInDay Int Not Null,
	CreatedAt DateTime Not Null,
	CreatedBy Char(36)  Not Null,
	UpdatedAt DateTime,
	UpdatedBy Char(36),
	[Ip] char(255) Not Null,
	IsActive bit
);


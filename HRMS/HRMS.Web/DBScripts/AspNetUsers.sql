
CREATE TABLE AspNetUsers(
	[Id] [nvarchar](450) NOT NULL PRIMARY KEY,
	[AccessFailedCount] [int] NOT NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[UserName] [nvarchar](256) NULL
	);


select * from AspNetUsers

Y8?QXkA_VSh_8@7

Y8?QXkA_VSh_8@7

BUr2tw%?n$UBS3q

y$6Ce@dbWKS-Z9e

insert into AspNetRoles values(1,'HR','HR',getdate());
insert into AspNetRoles values(2,'Employee','Employee',getdate());

select * from AspNetRoles

select * from AspNetUsers

select * from AspNetUserRoles

insert into AspNetUserRoles values('23d88f16-f429-4957-ad79-f4155194ec8a',1)
insert into AspNetUserRoles values('ce4408fe-b768-41e9-a16d-8bb665c72dfe',2)
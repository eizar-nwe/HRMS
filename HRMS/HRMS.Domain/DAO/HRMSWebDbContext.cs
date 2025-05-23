﻿using HRMS.Domain.Models.DataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Domain.DAO {
    public class HRMSWebDbContext : IdentityDbContext<IdentityUser, IdentityRole, string> {
        //constructor interchanging 
        public HRMSWebDbContext(DbContextOptions<HRMSWebDbContext> o) : base(o) {

        }
        //register for all of Data Models as DBSet
        public DbSet<PositionEntity> Positions { get; set; }
        public DbSet<DepartmentEntity> Departments { get; set; }
        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<AttendancePolicyEntity> AttendancePolicy { get; set; }
        public DbSet<ShiftEntity> Shifts { get; set; }
        public DbSet<ShiftAssignEntity> ShiftAssigns { get; set; }
        public DbSet<DailyAttendanceEntity> DailyAttendance { get; set; }                
        public DbSet<AttendanceMasterEntity> AttendanceMaster { get; set; }   
        public DbSet<PayrollEntity> Payroll { get; set; }

    }
}

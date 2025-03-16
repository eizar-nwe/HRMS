using HRMS.Web.DAO;
using HRMS.Web.Repositories.Domain;
using HRMS.Web.Services;
using HRMS.Web.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("HRMSWebDbContextConnection") ?? throw new InvalidOperationException("Connection string 'HRMSWebDbContextConnection' not found.");

// Add services to the container.
builder.Services.AddControllersWithViews();

//get the DB configuration from appsetting.json
var config = builder.Configuration;
//register the DBContext to connect to the database
builder.Services.AddDbContext<HRMSWebDbContext>(o => o.UseSqlServer(config.GetConnectionString("HRMSDB")));

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<HRMSWebDbContext>();

builder.Services.AddRazorPages();
builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<HRMSWebDbContext>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//depedency injection for all of domains
builder.Services.AddTransient<IPositionService, PositionService>();
builder.Services.AddTransient<IDepartmentService, DepartmentService>();
builder.Services.AddTransient<IEmployeeService, EmployeeService>();

builder.Services.AddTransient<IDailyAttendanceService, DailyAttendanceService>();

builder.Services.AddTransient<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // to access all files under wwwroot folder
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseStatusCodePages(async ctx =>
{
    if(ctx.HttpContext.Response.StatusCode==403 || ctx.HttpContext.Response.StatusCode == 404){
        ctx.HttpContext.Response.Redirect("/Home/AccessDenied");
    }
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();


using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using HRMS.WebAPIs.ConfigSwaggerOptions.HRMS.WebAPIs.Configurations;
using HRMS.Domain.DAO;
using HRMS.Services;
using HRMS.UnitOfWorks;
using Microsoft.DotNet.Scaffolding.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

var config = builder.Configuration;
//register the DBContext to connect to the database
builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<HRMSWebDbContext>().AddDefaultTokenProviders();
builder.Services.AddDbContext<HRMSWebDbContext>(o => o.UseSqlServer(config.GetConnectionString("HRMSDB")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//depedency injection for all of domains
builder.Services.AddTransient<IEmployeeService, EmployeeService>();
builder.Services.AddTransient<IDepartmentService, DepartmentService>();
builder.Services.AddTransient<IAuthService, AuthService>();

//Configure the JWT Bearer authentication
builder.Services.AddAuthentication(o =>
{
    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jo =>
{
    jo.SaveToken = true;
    jo.RequireHttpsMetadata = false;
    jo.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer=true,
        ValidateAudience = true,
        ValidIssuer = config["jwt:Issuer"],
        ValidAudience = config["jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["jwt:SecretKey"])),
    };
});

//configure for API versioning in .NET
builder.Services.AddApiVersioning(options => {
    // ** path : url , query string ,** header ,meida type
    options.DefaultApiVersion = new ApiVersion(1);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(), new HeaderApiVersionReader("X-Api-Version"));
})
.AddMvc() // This is needed for controllers
.AddApiExplorer(options => {
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Bind Swagger to versioning
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

var app = builder.Build();

//Configure the HTTP request pipeline.
var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                $"HRMS API {description.GroupName.ToUpperInvariant()}");
        }
    });
}

app.UseHttpsRedirection();
//Enable authentication and authorization
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();


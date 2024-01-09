using WebApi.DBOperations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebApi.Middlewares;
using WebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration; //.net 7 Configuration
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddDbContext(opt=>opt.UseSqlServer("Connection String"));
builder.Services.AddDbContext<BookStoreDbContext>(options => options.UseInMemoryDatabase(databaseName:"BookStoreDB")); //Added for .Net7
builder.Services.AddScoped<IBookStoreDbContext>(provider => provider.GetService<BookStoreDbContext>());
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly()); //Added for .Net7
builder.Services.AddSingleton<ILoggerService, DBLogger>(); //builder.Services.AddSingleton<ILoggerService, ConsoleLogger>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = configuration["Token:Issuer"],
        ValidAudience = configuration["Token:Audiance"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"])),
        ClockSkew = TimeSpan.Zero
    };
});
var app = builder.Build();
using (var scope = app.Services.CreateScope()) { var services = scope.ServiceProvider; DataGenerator.Initialize(services); } //Added for .Net7
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseDeveloperExceptionPage(); //
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseCors(builder => builder  //
    .AllowAnyOrigin()           //
    .AllowAnyMethod()           //
    .AllowAnyHeader());         //

//app.UseHttpsRedirection(); //Causes Fail to determine the HTTPS port for the redirect

app.UseAuthentication();

app.UseAuthorization();

app.UseCustomExceptionMiddle();

app.MapControllers();

app.Run();

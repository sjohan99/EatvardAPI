global using EatvardDataAccessLibrary.Data;
using EatvardAPI.Handlers;
using EatvardDataAccessLibrary;
using EatvardDataAccessLibrary.Repositories;
using EatvardDataAccessLibrary.Repositories.UserAccountRepository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient<IUserAccountRepository, UserAccountRepository>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddDbContext<EatvardContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    //options.UseSqlServer(builder.Configuration["Eatvard:AzureSqlServerConnectionString"]);
});
builder.Services
    .AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

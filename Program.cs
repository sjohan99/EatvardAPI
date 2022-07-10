using EatvardAPI.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
builder.Services.AddDbContext<EatvardContext>(options =>
{
    options.UseSqlServer(builder.Configuration["Eatvard:AzureSqlServerConnectionString"]);
});
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

app.UseAuthorization();

app.MapControllers();

app.Run();



//EatvardContext dbContext = new(builder.Configuration["Eatvard:AzureSqlServerConnectionString"]);
//var database = new PopulateDatabase(dbContext);
//database.AddRestaurant("Din�", "DrottningGatan 31", "Gothenburg");
//database.SaveChanges();

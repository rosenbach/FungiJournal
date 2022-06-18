using FungiJournal.DataAccess;
using System.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json");
builder.Services.Configure<DataAccessConfiguration>(builder.Configuration.GetSection(nameof(DataAccessConfiguration)));
// Add services to the container.

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("http://192.168.1.4:8080/",
                                                "http://localhost:8080");
                      });
});

builder.Services.AddControllers();

builder.Services.AddTransient<IDataAccess, SQLiteDataAccess>();
builder.Services.AddEntityFrameworkSqlite()
    .AddDbContext<CodeFirstDbContext>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors(MyAllowSpecificOrigins);
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

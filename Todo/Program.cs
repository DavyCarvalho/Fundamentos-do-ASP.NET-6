using Todo.Data;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>();

app.MapControllers();

app.Run();

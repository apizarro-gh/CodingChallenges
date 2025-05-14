var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // for Swagger
builder.Services.AddSwaggerGen();           // for Swagger

// to register (error when run in console simulator)
builder.Services.AddSingleton<CustomerStore>();

var app = builder.Build();

// Swagger UI in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

using Microsoft.EntityFrameworkCore;
using TaskRESTAPI.Data;
using TaskRESTAPI;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TaskRESTAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TaskRESTAPIContext") ?? throw new InvalidOperationException("Connection string 'TaskRESTAPIContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<FileUploadOperatingFilter>();
});

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

app.MapTaskModelEndpoints();

app.Run();

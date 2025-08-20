using STS.Application.Mapping;
using STS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//controllers [apicontroller] aktif edildi
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//automapper (application katmanindaki profile bulur)
builder.Services.AddAutoMapper(typeof(MappingProfile));

//dbcontext(connection string appsetting.json icinde)
builder.Services.AddDbContext<STSDbContext>();

var app = builder.Build();

// Swagger middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//middleware sirasi
app.MapControllers();//controller route baglamak
app.UseAuthorization();
app.UseHttpsRedirection();



//uygulamayi baslat

app.Run();// DbContext: appsettings.json -> ConnectionStrings:DefaultConnection
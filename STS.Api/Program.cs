using STS.Application.IRepositories;
using STS.Application.Mapping;
using STS.Infrastructure.Data;
using STS.Infrastructure.Repositories;



var builder = WebApplication.CreateBuilder(args);

//controllers [apicontroller] aktif edildi
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//automapper (application katmanindaki profile bulur)
builder.Services.AddAutoMapper(typeof(MappingProfile));

//dbcontext(connection string appsetting.json icinde)
builder.Services.AddDbContext<STSDbContext>();

// interfaceler infrastructure implementasyonlariyla eslesti
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<IStockMovementRepository, StockMovementRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddControllers();


var app = builder.Build();

// Swagger middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(option =>
    {
        option.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        option.RoutePrefix = string.Empty;
    });

    app.UseSwaggerUI();
}

//middleware sirasi
app.MapControllers();//controller route baglamak
app.UseAuthorization();
app.UseHttpsRedirection();



//uygulamayi baslat

app.Run();// DbContext: appsettings.json -> ConnectionStrings:DefaultConnection
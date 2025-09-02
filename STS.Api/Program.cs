using STS.Application.IRepositories;
using STS.Application.Mapping;
using STS.Infrastructure.Data;
using STS.Infrastructure.Repositories;
using System.Text.Json.Serialization;



var builder = WebApplication.CreateBuilder(args);

//controllers [apicontroller] aktif edildi
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//api string dönderiyor
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });


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






// cors policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", builder =>

        builder.WithOrigins("http://localhost:3000")//react portu
         .AllowAnyHeader()
         .AllowAnyMethod());
});

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
app.UseCors("AllowReactApp");//cors aktif
//middleware sirasi
app.UseRouting();
app.UseHttpsRedirection();

app.UseAuthorization();

//uygulamayi baslat
app.MapControllers();//controller route baglamak

app.Run();// DbContext: appsettings.json -> ConnectionStrings:DefaultConnection
using Microsoft.EntityFrameworkCore;
using PeliculasAPI;
using PeliculasAPI.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DbContext config.
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("name= DefaultConnection"));

//Automapper config.
builder.Services.AddAutoMapper(typeof(Program));

//Caché config.
builder.Services.AddOutputCache(options =>
{
    options.DefaultExpirationTimeSpan = TimeSpan.FromSeconds(60);

});

var origenesPermitidos = builder.Configuration.GetValue<string>("OrigenesPermitidos")!.Split(",");

//CORS config
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(optionsCors =>
    {
        optionsCors.WithOrigins(origenesPermitidos).AllowAnyMethod().AllowAnyHeader().
        WithExposedHeaders("cantidad-total-registros");
    });
});

//Config alm. archivos
builder.Services.AddTransient<IAlmacenadorArchivos, AlmacenadorArchivosLocal>();

builder.Services.AddHttpContextAccessor();



var app = builder.Build();


//Middlewares

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors();

app.UseOutputCache();

app.UseAuthorization();

app.MapControllers();

app.Run();

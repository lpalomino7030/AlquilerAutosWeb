using AlquilerAPI.Repositorio.Intefaces;
using AlquilerAPI.Repositorio.DAO;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAlquiler, AlquilerDAO>();
builder.Services.AddScoped<IAuto, AutoDAO>();
builder.Services.AddScoped<ICliente, ClienteDAO>();
builder.Services.AddScoped<ILogin, LoginDAO>();
builder.Services.AddScoped<IReporteAlquiler, ReporteAlquilerDAO>();
builder.Services.AddScoped<IUsuario, UsuarioDAO>();
builder.Services.AddScoped<IReporteAlquiler, ReporteAlquilerDAO>();



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

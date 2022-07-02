using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MovimentControl.Api.Repository;
using MovimentControl.Api.Persistence;
using MovimentControl.Api.Validations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers()
    .AddFluentValidation(option => option.RegisterValidatorsFromAssemblyContaining<ClientValidator>());

builder.Services
    .AddScoped<IClientRepository, ClientRepository>();

var _clientConn = builder.Configuration.GetConnectionString("ClientCs");
builder.Services
.AddDbContext<MovimentControlContext>(
    options => options.UseSqlServer(_clientConn));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1",new OpenApiInfo
    {
        Title = "MovimentControl.API",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "sltheus",
            Email = "matheus18sdl@outlook.com",
            Url = new Uri("https://www.linkedin.com/in/matheus-lima-8bb320203/")
        } 
    });
    var xmlFile = Path.Combine(AppContext.BaseDirectory,"MovimentControl.Api.xml");
    options.IncludeXmlComments(xmlFile);
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(config => config.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SendGrid.Extensions.DependencyInjection;
using MovimentControl.Domain.Interfaces.Services;
using MovimentControl.Domain.Interfaces.Repository;
using MovimentControl.Domain.Services;
using MovimentControl.Infra.Repository;
using MovimentControl.Domain.Validations;
using MovimentControl.Application.Mapper;
using MovimentControl.Infra;
using FluentValidation;
using MovimentControl.Domain.Models.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
builder.Services
    .AddScoped<IClientService, ClientService>();
builder.Services
    .AddScoped<IUserService, UserService>();

builder.Services
    .AddValidatorsFromAssemblyContaining<ClientValidations>().AddFluentValidationAutoValidation();

builder
    .Services.AddDbContext<Context>();

builder.Services
    .AddAutoMapper(typeof(ClientProfile));
builder.Services
    .AddScoped<IClientRepository, ClientRepository>();
builder.Services
    .AddScoped<IUserRepository, UserRepository>();


var SendGridKey = builder.Configuration.GetSection("SendGridApiKey").Value;

builder.Services.AddSendGrid(o => o.ApiKey = SendGridKey);


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}
).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),


    };
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
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
    var xmlFile = Path.Combine(AppContext.BaseDirectory, "MovimentControl.Api.xml");
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


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

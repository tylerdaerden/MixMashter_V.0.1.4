using Microsoft.EntityFrameworkCore;
using MixMashter.BLL.Interfaces;
using MixMashter.BLL.Services;
using MixMashter.DAL.Db;
using MixMashter.DAL.Repositories;
using MixMashter.DAL.Repositories.Implementations;
using MixMashter.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;



var builder = WebApplication.CreateBuilder(args);

//Ici je vais ajouter mes services � mon conteneur d'injection de d�pendances , l'injection de d�pendances en ASP.NET Core se fait ici dans le fichioer Program.cs
//Pour �tre au plus lisible comme vu en stage (en refzcto car manquant � la base) je serais exhaustif dans les comments et dans les ajouts de services

// ajout des services
builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
}); // indispensable pour le support PATCH , vu lors de mes test avec Swagger dans mes ArtistsTestController et aussi pour afficher les enums en string dans les retours json pour + de clart�
//builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
// Swagger avec support JWT (solution trouv�e via assistance IA ChatGPT je bloquais parce qu'avant j'avais juste ajout� Swagger comme dans mes projets pr�c�dents builder.Services.AddSwaggerGen() )
// Or Swagger ne sait pas g�rer le JWT tout seul , il faut lui dire comment faire , c'est ce que je fais ci-dessous suite � la suggestion de l'IA
builder.Services.AddSwaggerGen(c =>
{
    // D�finition du sch�ma de s�curit� JWT
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    // Ajout de l�exigence de s�curit� pour toutes les routes prot�g�es
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

//Entity Framework Core avec SQL Server , et il va r�cup�rer ma connection string dans le appsettings.json 
//donc on a la liason et la s�paration des couches , l'api acc�de � la DAL via le DbContext
builder.Services.AddDbContext<MixMashterDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);



// Repositories (DAL)
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMashupRepository, MashupRepository>();
builder.Services.AddScoped<ISongRepository, SongRepository>();
builder.Services.AddScoped<IPlaylistRepository, PlaylistRepository>();
builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
builder.Services.AddScoped<IFavoritesRepository, FavoritesRepository>();
builder.Services.AddScoped<IPlaylistMashupRepository, PlaylistMashupRepository>();

// Services (BLL)
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMashupService, MashupService>();
builder.Services.AddScoped<ISongService, SongService>();
builder.Services.AddScoped<IPlaylistService, PlaylistService>();
builder.Services.AddScoped<IArtistService, ArtistService>();
builder.Services.AddScoped<IFavoritesService, FavoritesService>();
builder.Services.AddScoped<IPlaylistMashupService, PlaylistMashupService>();

// R�cup�ration cl� secr�te depuis appsettings.json (solution simple ici mais pour le futur, en prod utiliser Azure Key Vault ou autre cf le dossier d'analyse)
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

// Ajouter le service CORS pour autoriser mon Front Blazor � acc�der � mon API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor", policy =>
        policy.WithOrigins("https://localhost:7276")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials());
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();

app.UseRouting();

// Activer CORS ici
app.UseCors("AllowBlazor");

app.UseAuthorization();

app.MapControllers();

app.Run();

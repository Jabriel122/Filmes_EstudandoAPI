
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//adiciona o servico de controller
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

//Adcionar o serviço de Jwt Bearer (forma de autenticação)
builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = "JwtBearer";
    options.DefaultAuthenticateScheme = "JwtBearer";
})

.AddJwtBearer("JwtBearer", options =>
 {
     options.TokenValidationParameters = new TokenValidationParameters
     {
         //Valida quem está solicitando
         ValidateIssuer = true,

         //valida quem está recebendo 
         ValidateAudience = true,

         //define se o tempo de expiração será valido
         ValidateLifetime = true,

         //forma de criptografar e valida a chave de autenticação
         ClockSkew = TimeSpan.FromMinutes(5),

         //nome da issuer (De onde está vindo)
         ValidIssuer = "webapi.filmes.manha",

         //nome do audinece (para onde está indo)
         ValidAudience = "webapi.filmes.manha"

     };

 });

//Adcionar Swagger
builder.Services.AddSwaggerGen(options =>
{
    //Aqui adciona informações sobre a API no Swagger
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API FILMES GABS",
        Description = "APi para gerenciamento de filmes - Introdução a Sprint 2 - Backend API",
        Contact = new OpenApiContact
        {
            Name = "Senai Informática - Turma Manhã - Gabriel Marchetti",
            Url = new Uri("https://github.com/Jabriel122")
        },
    });


    //Configure o Swagger para usar o arquivo XML gerado
    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));


});



var app = builder.Build();

//comeca a configuracaos do swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

//finaliza a configuracao do swagger

//adiciona mapeamento dos controllers
app.MapControllers();


//Adciona Autenticação
app.UseAuthentication();
//adciona autorização
app.UseAuthentication();

app.UseHttpsRedirection();

app.Run();

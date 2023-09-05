
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//adiciona o servico de controller
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

//Adcionar o servi�o de Jwt Bearer (forma de autentica��o)
builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = "JwtBearer";
    options.DefaultAuthenticateScheme = "JwtBearer";
})

.AddJwtBearer("JwtBearer", options =>
 {
     options.TokenValidationParameters = new TokenValidationParameters
     {
         //Valida quem est� solicitando
         ValidateIssuer = true,

         //valida quem est� recebendo 
         ValidateAudience = true,

         //define se o tempo de expira��o ser� valido
         ValidateLifetime = true,

         //forma de criptografar e valida a chave de autentica��o
         ClockSkew = TimeSpan.FromMinutes(5),

         //nome da issuer (De onde est� vindo)
         ValidIssuer = "webapi.filmes.manha",

         //nome do audinece (para onde est� indo)
         ValidAudience = "webapi.filmes.manha"

     };

 });

//Adcionar Swagger
builder.Services.AddSwaggerGen(options =>
{
    //Aqui adciona informa��es sobre a API no Swagger
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API FILMES GABS",
        Description = "APi para gerenciamento de filmes - Introdu��o a Sprint 2 - Backend API",
        Contact = new OpenApiContact
        {
            Name = "Senai Inform�tica - Turma Manh� - Gabriel Marchetti",
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


//Adciona Autentica��o
app.UseAuthentication();
//adciona autoriza��o
app.UseAuthentication();

app.UseHttpsRedirection();

app.Run();

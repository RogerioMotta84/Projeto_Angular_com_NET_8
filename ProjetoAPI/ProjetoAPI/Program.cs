using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using ProjetoAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao contêiner
builder.Services.AddControllers();

// Adiciona o LoginService como um serviço Singleton
builder.Services.AddSingleton<LoginService>();

// Configura o versionamento da API
var apiVersioningBuilder = builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0); // Define a versão padrão como v1.0
    options.AssumeDefaultVersionWhenUnspecified = true; // Assume a versão padrão se não especificada
    options.ReportApiVersions = true; // Adiciona a versão nos cabeçalhos da resposta
});

// Configura o explorador de versões para o Swagger
apiVersioningBuilder.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV"; // Formato: v1, v2, etc.
    options.SubstituteApiVersionInUrl = true; // Substitui a versão na URL
});

// Configurações do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

// Adiciona CORS para permitir chamadas da aplicação Angular
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

// Middleware global para tratamento de exceções
app.UseExceptionHandler(appBuilder =>
{
    appBuilder.Run(async context =>
    {
        context.Response.StatusCode = 500;
        await context.Response.WriteAsync("Ocorreu um erro inesperado. Por favor, tente novamente mais tarde.");
    });
});

// Middleware do Swagger
if (builder.Configuration.GetSection("SwaggerConfig")?.GetValue<string>("habilitado")?.Equals("true", StringComparison.CurrentCultureIgnoreCase) == true)
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
        }
    });
}

// Middleware CORS
app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();

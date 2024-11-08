using Crypto.Monitoring.Infra;
using Hangfire;
using Hangfire.PostgreSql;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var conn = Environment.GetEnvironmentVariable("MY_HANGFIRE_CONNECTION_POSTGRES");

// Configura o Hangfire para usar um banco de dados Postgres
builder.Services.AddHangfire(config => config
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UsePostgreSqlStorage(conn,
    new PostgreSqlStorageOptions
    {
        InvisibilityTimeout = TimeSpan.FromMinutes(5),
        QueuePollInterval = TimeSpan.FromSeconds(15),
        SchemaName = "public"
    })
);

builder.Services.AddServices();

// Servidor do Hangfire
builder.Services.AddHangfireServer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Painel do Hangfire
app.UseHangfireDashboard();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);

app.MapControllers();

app.UseHttpsRedirection();

app.Run();
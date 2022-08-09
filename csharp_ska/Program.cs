using csharp_ska.Services;
using Polly;
using Polly.Extensions.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IpService>();
builder.Services.AddSingleton<CountryCodeService>();
builder.Services
    .AddHttpClient("IpClient", c => c.BaseAddress = new System.Uri("https://api.ipify.org"))
    .AddPolicyHandler(
        HttpPolicyExtensions
            .HandleTransientHttpError()
            .RetryAsync(5));
builder.Services
    .AddHttpClient("CountryCodeClient", c => c.BaseAddress = new System.Uri("http://ip-api.com"))
    .AddPolicyHandler(
        HttpPolicyExtensions
            .HandleTransientHttpError()
            .RetryAsync(5));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
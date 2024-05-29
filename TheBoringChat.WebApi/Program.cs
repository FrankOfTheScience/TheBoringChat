var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(Environment.GetEnvironmentVariable("REACT_APP_NAME"), builder =>
    {
        builder.WithOrigins(Environment.GetEnvironmentVariable("REACT_APP_HOST"))
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});
builder.Services.AddSingleton<SharedDb>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<BoringChatHub>("/boring-chat");
app.UseCors(Environment.GetEnvironmentVariable("REACT_APP_NAME"));

app.Run();

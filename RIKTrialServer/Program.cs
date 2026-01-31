using Microsoft.EntityFrameworkCore;
using RIKTrialServer.Infra.Persistance;
using RIKTrialServer.Repositories.Implementations;
using RIKTrialServer.Repositories.Interfaces;
using RIKTrialServer.Services.Implementations;
using RIKTrialServer.Services.Interfaces;
using System;

using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ServerDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));


// -- di for event stuff
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IEventService, EventService>();

builder.Services.AddScoped<IParticipantRepository, ParticipantRepository>();
builder.Services.AddScoped<IParticipantService, ParticipantService>();

builder.Services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
builder.Services.AddScoped<IPaymentMethodService, PaymentMethodService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins("https://localhost:7000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    ServerDbContext db = scope.ServiceProvider.GetRequiredService<ServerDbContext>();
    db.Database.Migrate();
}

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("BlazorPolicy");

/*app.MapGet("/", () => "Hello World!");*/

app.Run();

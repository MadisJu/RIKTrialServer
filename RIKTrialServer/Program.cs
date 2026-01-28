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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

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


app.MapGet("/", () => "Hello World!");

app.Run();

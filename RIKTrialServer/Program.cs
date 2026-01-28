using Microsoft.EntityFrameworkCore;
using RIKTrialServer.Infra.Persistance;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ServerDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    ServerDbContext db = scope.ServiceProvider.GetRequiredService<ServerDbContext>();
    db.Database.Migrate();
}

app.MapGet("/", () => "Hello World!");

app.Run();

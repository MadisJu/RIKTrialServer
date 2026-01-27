using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using RIKTrialServer.Domain.Models;
using System;

namespace RIKTrialServer.Infra.Persistance
{
    public sealed class ServerDbContext : DbContext
    {
        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<EventParticipant> EventParticipants { get; set; } = null!;
        public DbSet<Participant> Participants { get; set; } = null!;
        public DbSet<PaymentMethod> PaymentMethods { get; set; } = null!;
        public DbSet<Person> Persons { get; set; } = null!;
        public DbSet<Company> Companies { get; set; } = null!;
        
        public ServerDbContext(DbContextOptions<ServerDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ServerDbContext).Assembly);
        }

    }
}

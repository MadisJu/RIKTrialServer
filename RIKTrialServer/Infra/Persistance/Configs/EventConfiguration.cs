using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RIKTrialServer.Domains.Models;

namespace RIKTrialServer.Infra.Persistance.Configs
{
    public sealed class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(e => e.Date)
                .IsRequired();

            builder.Property(e => e.Location)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.AdditionalInfo)
                .HasMaxLength(1000);

            builder.Navigation(e => e.Participants)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}

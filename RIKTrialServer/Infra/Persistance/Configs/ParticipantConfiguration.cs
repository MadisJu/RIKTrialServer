using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RIKTrialServer.Domain.Models;

namespace RIKTrialServer.Infra.Persistance.Configs
{
    public sealed class ParticipantConfig : IEntityTypeConfiguration<Participant>
    {
        public void Configure(EntityTypeBuilder<Participant> builder)
        {
            builder.ToTable("Participants");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.HasOne(x => x.PaymentMethod)
                .WithMany(pm => pm.Participants)
                .HasForeignKey(x => x.PaymentMethodId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.UseTptMappingStrategy();

            /*
            builder.HasDiscriminator<ParticipantType>("ParticipantType")
                .HasValue<Person>(ParticipantType.Person)
                .HasValue<Company>(ParticipantType.Company);
            I am stupid i dont need this
            */

            builder.Navigation(p => p.Events)
               .UsePropertyAccessMode(PropertyAccessMode.Field);

        }
    }
}

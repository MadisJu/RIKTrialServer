using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RIKTrialServer.Domain.Models;

namespace RIKTrialServer.Infra.Persistance.Configs
{
    public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.ToTable("PaymentMethods");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Method)
               .HasMaxLength(100)
               .IsRequired();

            builder.HasIndex(x => x.Method)
                .IsUnique();


            builder.HasData(
                new PaymentMethod { Id = 1, Method = "Pangaülekanne" },
                new PaymentMethod { Id = 2, Method = "Sularaha" }
            );
        }
    }
}

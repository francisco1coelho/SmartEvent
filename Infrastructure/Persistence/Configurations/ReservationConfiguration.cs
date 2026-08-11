using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartEvent.Domain.Entities;

namespace SmartEvent.Infrastructure.Persistence.Configurations
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable("reservations");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.CreatedAt)
                .IsRequired();

            builder.Property(r => r.CancelledAt);

            builder.Property(r => r.State)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.HasOne(r => r.Event)
                .WithMany(e => e.Reservations)
                .HasForeignKey(r => r.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Participant)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.ParticipantId)
                .OnDelete(DeleteBehavior.Restrict);

            // Garante que um participante não pode ter duas reservas ATIVAS no mesmo evento
            builder.HasIndex(r => new { r.EventId, r.ParticipantId })
                .IsUnique()
                .HasFilter("\"State\" != 'Cancelled'");
        }
    }
}

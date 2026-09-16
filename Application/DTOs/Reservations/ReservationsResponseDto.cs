using SmartEvent.Domain.Enums;

namespace SmartEvent.Application.DTOs.Reservations;

public class ReservationsResponseDto
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? CancelledAt { get; set; }

    public ReservationState State { get; set; }

    public int EventId { get; set; }

    public int ParticipantId { get; set; }
}
using SmartEvent.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace SmartEvent.Application.DTOs.Reservations;

public class CreateReservationDto
{
    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public int EventId { get; set; }

    [Required]
    public int ParticipantId { get; set; }

    [Required]
    public ReservationState State { get; set; }
}

public class UpdateReservationDto
{
    [Required]
    public int EventId { get; set; }

    [Required]
    public int ParticipantId { get; set; }

    [Required]
    public ReservationState State { get; set; }
}
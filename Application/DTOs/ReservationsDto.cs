using SmartEvent.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace SmartEvent.Application.DTOs.ReservationsDto
{
    public class CreateReservationDto
    {
        [Required]
        public DateTime CreatedAt { get; set; }

        [Required] 
        public int EventId { get; set; }

        [Required]
        public int ParticipantId { get; set; }
        
        [Required]
        public ReservationState state { get; set; }
    }
}
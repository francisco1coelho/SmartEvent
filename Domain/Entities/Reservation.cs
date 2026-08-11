using SmartEvent.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEvent.Domain.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CancelledAt { get; set; }
        public ReservationState State { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; } = null!;

        public int ParticipantId { get; set; }
        public User Participant { get; set; } = null!;
    }
}

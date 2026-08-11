using SmartEvent.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEvent.Domain.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MaxCapacity { get; set; }
        public string Location { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public EventState State { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public int OrganizerId { get; set; }
        public User Organizer { get; set; } = null!;

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}

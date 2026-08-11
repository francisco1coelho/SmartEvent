using SmartEvent.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEvent.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public Role Role { get; set; }
        public bool Locked { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<Event> OrganizedEvents { get; set; } = new List<Event>();
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}

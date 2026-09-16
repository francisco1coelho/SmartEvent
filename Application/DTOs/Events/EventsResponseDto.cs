using System.ComponentModel.DataAnnotations;
using SmartEvent.Domain.Enums;

namespace SmartEvent.Application.DTOs.Events;

public class EventsResponseDto
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

    public int OrganizerId { get; set; }
}
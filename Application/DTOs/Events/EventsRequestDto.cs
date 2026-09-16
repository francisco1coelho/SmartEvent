using System.ComponentModel.DataAnnotations;
using SmartEvent.Domain.Enums;

namespace SmartEvent.Application.DTOs.Events;

public class EventsRequestDto
{
    [Required(ErrorMessage = "Event name is required")]
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Start date is required")]
    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "End date is required")]
    public DateTime EndDate { get; set; }

    [Required(ErrorMessage = "Max capacity is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Max capacity must be greater than 0")]
    public int MaxCapacity { get; set; }

    [Required(ErrorMessage = "Location is required")]
    public string Location { get; set; } = string.Empty;

    [Required(ErrorMessage = "Event state is required")]
    public EventState State { get; set; }

    [Required(ErrorMessage = "Category ID is required")]
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "Organizer ID is required")]
    public int OrganizerId { get; set; }
}
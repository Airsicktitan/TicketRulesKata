using System.Text.Json.Serialization;
using TicketRulesKata.Enums;

namespace TicketRulesKata.Models;

public class Ticket
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public TicketPriority Priority { get; set; }
    public required Status Status { get; set; }
    public required Skill[] RequiredSkills { get; set; }
    public int OwningTeamId { get; set; }
    [JsonIgnore]
    public Team? OwningTeam { get; set; }
}
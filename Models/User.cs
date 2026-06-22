using System.Text.Json.Serialization;
using TicketRulesKata.Enums;

namespace TicketRulesKata.Models;

public class User
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int TeamId { get; set; }
    [JsonIgnore]
    public Team? Team { get; set; }
    public required Skill[] Skills { get; set; }
    public int CurrentWorkload { get; set; }
    public bool IsAvailable { get; set; } = true;
}
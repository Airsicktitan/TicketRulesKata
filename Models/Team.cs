using TicketRulesKata.Enums;

namespace TicketRulesKata.Models;

public class Team
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required Skill[] SupportedSkills { get; set; }
}
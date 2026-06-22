namespace TicketRulesKata.Models;

public class Response
{
    public List<Team> Teams { get; set; } = [];
    public List<User> Users { get; set; } = [];
    public List<Ticket> Tickets { get; set; } = [];
    
}
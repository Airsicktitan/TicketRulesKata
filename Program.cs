using System.Text.Json;
using System.Text.Json.Serialization;
using TicketRulesKata.Services;
using TicketRulesKata.Models;

namespace TicketRulesKata;
    
class Program
{
    static void Main(string[] args)
    {
        var filePath = Path.Combine("FakeData", "Data.json"); // Ensure this path points to your JSON data file
        var response = JsonSerializer.Deserialize<Response>(
            File.ReadAllText(filePath),
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            });

        // Example usage of the routing logic
        if(response is null)
        {
            Console.WriteLine("Failed to load data.");
            return;
        }

        Console.WriteLine(Directory.GetCurrentDirectory());
        Console.WriteLine($"Teams loaded: {response.Teams.Count}");
        Console.WriteLine($"Users loaded: {response.Users.Count}");
        Console.WriteLine($"Tickets loaded: {response.Tickets.Count}\n\n");

        var ticketAssignmentService = new TicketAssignmentService();
        var orderedPriorityTickets = response.Tickets.OrderByDescending(t => t.Priority).ToList();

        foreach (var t in orderedPriorityTickets)
        {
            var result = ticketAssignmentService.AssignTicket(t, response.Users, response.Teams);
            Console.WriteLine($"Ticket '{t.Title}' assigned: {result.WasAssigned}, Reason: {result.Reason}, Priority: {t.Priority}\n");
        }
        
        foreach (var u in response.Users)
        {
            Console.WriteLine($"User '{u.Name}' workload: {u.CurrentWorkload}");
        }
    }
}

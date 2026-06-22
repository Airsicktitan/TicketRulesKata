using System.Text.Json;
using System.Text.Json.Serialization;
using TicketRulesKata.Services;
using TicketRulesKata.Models;

namespace TicketRulesKata;
    
class Program
{
    static void Main(string[] args)
    {
        // Sample data setup
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
        var ticket = response.Tickets.First();

        var assignmentResult = ticketAssignmentService.AssignTicket(ticket, response.Users, response.Teams);

        Console.WriteLine($"Ticket '{ticket.Title}'");
        Console.WriteLine($"Assignment Result: {assignmentResult.WasAssigned}");

        if(assignmentResult.AssignedUser is not null)
        {
            Console.WriteLine($"Assigned User: {assignmentResult.AssignedUser?.Name}");
        }

        Console.WriteLine($"Reason: {assignmentResult.Reason}");
        
    }
}

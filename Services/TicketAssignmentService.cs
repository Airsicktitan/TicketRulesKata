using TicketRulesKata.Models;
using TicketRulesKata.Enums;

namespace TicketRulesKata.Services;

public class TicketAssignmentService
{
    public AssignmentResult AssignTicket(
        Ticket ticket,
        List<User> users,
        List<Team> teams
    )
    {

        var ticketStatus = ticket.Status;
        var workloadLimit = 2;
        
        if(ticketStatus != Status.Open && ticketStatus != Status.New)
        {
            return new AssignmentResult
            {
                Ticket = ticket,
                AssignedUser = null,
                WasAssigned = false,
                Reason = $"Ticket status is '{ticketStatus}', only 'Open' or 'New' tickets can be assigned."
            };
        }

        var owningTeam = teams.FirstOrDefault(team => team.Id == ticket.OwningTeamId);
        if(owningTeam == null)
        {
            return new AssignmentResult
            {
                Ticket = ticket,
                AssignedUser = null,
                WasAssigned = false,
                Reason = $"Owning team with ID {ticket.OwningTeamId} not found."
            };
        }

        var teamSupportsRequiredSkills = ticket.RequiredSkills.All(skill => owningTeam.SupportedSkills.Contains(skill));
        if(!teamSupportsRequiredSkills)        {
            return new AssignmentResult
            {
                Ticket = ticket,
                AssignedUser = null,
                WasAssigned = false,
                Reason = $"Owning team '{owningTeam.Name}' does not support all required skills for the ticket."
            };
        }

        var availableUsers = users.Where(user => user.TeamId == owningTeam.Id && user.IsAvailable).ToList();
        if(!availableUsers.Any())
        {
            return new AssignmentResult
            {
                Ticket = ticket,
                AssignedUser = null,
                WasAssigned = false,
                Reason = $"No available users in owning team '{owningTeam.Name}' to assign the ticket."
            };
        }

        var qualifiedUsers = availableUsers.Where(user => ticket.RequiredSkills.All(skill => user.Skills.Contains(skill))).ToList();
        if(!qualifiedUsers.Any())
        {
            return new AssignmentResult
            {
                Ticket = ticket,
                AssignedUser = null,
                WasAssigned = false,
                Reason = $"No available users in owning team '{owningTeam.Name}' have all the required skills to assign the ticket."
            };
        }

        var assignedUser = qualifiedUsers.OrderBy(user => user.CurrentWorkload).ThenBy(user => user.Id).First();

        
        if(assignedUser.CurrentWorkload >= workloadLimit)
        {
            return new AssignmentResult
            {
                Ticket = ticket,
                AssignedUser = null,
                WasAssigned = false,
                Reason = $"User '{assignedUser.Name}' has reached the workload limit of {workloadLimit} and cannot be assigned the ticket."
            };
        }

        assignedUser.CurrentWorkload++;

        if(ticketStatus == Status.New || ticketStatus == Status.Open)
        {
            ticket.Status = Status.InProgress;
        }

        return new AssignmentResult
        {
            Ticket = ticket,
            AssignedUser = assignedUser,
            WasAssigned = true,
            Reason = $"{assignedUser.Name} was assigned because they are available, on {owningTeam.Name}, have all required skills, and have the lowest workload."
        };
    }
}
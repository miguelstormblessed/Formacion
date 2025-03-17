namespace UsersManagement.Shared.Counters.Domain.Responses;

public class CountResponse
{
    public CountResponse(int activeUsers, int inactiveUsers)
    {
        this.ActiveUsers = activeUsers;
        this.InactiveUsers = inactiveUsers;
        this.TotalUsers = activeUsers + inactiveUsers;
    }

    public int ActiveUsers { get; set; }
    public int InactiveUsers { get; set; }
    public int TotalUsers { get; set; }
}
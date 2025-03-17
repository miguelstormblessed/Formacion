using Cojali.Shared.Domain.Bus.Query;
using UsersManagement.Shared.Users.Domain.Responses;

namespace UsersManagement.Shared.Users.Domain.Querys;

public class UserFinderQuery : Query<UserResponse> , IEquatable<UserFinderQuery>
{
    private UserFinderQuery(string userId)
    {
        UserId = userId;
    }
    public string UserId { get; set; }

    public static UserFinderQuery Create(string userId)
    {
        return new UserFinderQuery(userId);
    }

    public bool Equals(UserFinderQuery? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return UserId == other.UserId;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((UserFinderQuery)obj);
    }

    public override int GetHashCode()
    {
        return UserId.GetHashCode();
    }
}
using AdessoWorldLeague.Domain.Common;
using AdessoWorldLeague.Domain.Models.Teams;

namespace AdessoWorldLeague.Domain.Models;

public class Group:Entity<GroupId>
{
    private Group(){}
    public Group(GroupId id, GroupName name)
    {
        Id = id;
        Name = name;
    }
        
    public GroupName Name { get; set; }
    public ICollection<Team> Teams { get; set; } = [];
}

public record GroupName(string Value);

public record GroupId(Guid Value);
public record Reaction(Guid UserId, ReactionType reactionType);

public enum ReactionType
{
    Approve,
    OnPoint,
    SlightlyOn,
    SlighlyOff,
    Wayoff,
    Dissaprove,
}

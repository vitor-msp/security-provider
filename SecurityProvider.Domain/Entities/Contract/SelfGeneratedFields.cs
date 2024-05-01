namespace SecurityProvider.Domain.Entities.Contract;

public abstract class SelfGeneratedFields
{
    public Guid? Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public bool? Deleted { get; set; }
}
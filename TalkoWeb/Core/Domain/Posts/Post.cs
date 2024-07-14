using TalkoWeb.SharedKernel;

public class Post : BaseEntity
{
    public Guid PostID { get; set; }

    public Guid Owner { get; set; }
    public string PostTitle { get; set; } = string.Empty;

    public string PostBody { get; set; } = string.Empty;

    public int vote { get; set; }

}
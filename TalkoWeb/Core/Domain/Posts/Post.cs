using TalkoWeb.SharedKernel;

public class Post : BaseEntity
{
    public Guid PostID { get; set; }

    public Guid Owner { get; set; }
    public string PostTitle { get; set; } = string.Empty;

    public string PostContent { get; set; } = string.Empty;

    public int Votes { get; set; } = 0; // Updated when from user voted function

}
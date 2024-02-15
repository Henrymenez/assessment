namespace Movie.Service.Utility;

public class OmdConfig
{
    public virtual string ApiKey { get; set; } = null!;
    public virtual  string BaseUrl { get; set; } = null!;
    public virtual string SearchUrl { get; set; } = null!;
}

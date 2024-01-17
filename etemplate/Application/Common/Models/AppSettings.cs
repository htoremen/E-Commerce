namespace Application.Common.Models;

public class AppSettings
{
    public ConnectionStrings ConnectionStrings { get; set; }
    public Authenticate Authenticate { get; set; }
}

public class Authenticate
{
    public string Secret { get; set; }
    public string RefreshTokenTTL { get; set; }
}

public class ConnectionStrings
{
    public string ConnectionString { get; set; }
    public string Monitoring { get; set; }
}

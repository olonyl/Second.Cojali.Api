namespace Second.Cojali.Api.Contracts.Configuration;

public class AppSettings
{
    public bool UseDatabase { get; set; }
    public string UserJsonFilePath { get; set; } = string.Empty;
    public string ConnectionString { get; set; } = string.Empty;
}

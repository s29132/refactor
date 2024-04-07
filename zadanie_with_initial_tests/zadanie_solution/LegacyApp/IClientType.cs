namespace LegacyApp;

public interface IClientType
{
    public string Type { get; set; }
    void SetUpClient(ICreditInfo info);
}
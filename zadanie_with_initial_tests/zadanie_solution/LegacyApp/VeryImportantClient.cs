using System;

namespace LegacyApp;

public class VeryImportantClient : User, IClientType
{
    public string Name { get; internal set; }
    public int ClientId { get; internal set; }
    public string Email { get; internal set; }
    public string Address { get; internal set; }
    public string Type { get; set; }
    public override void SetUpClient(ICreditInfo creditInfo)
    {
        this.HasCreditLimit = false;
        if (creditInfo is IDisposable disposableObj)
        {
            disposableObj.Dispose();
        }
    }
}
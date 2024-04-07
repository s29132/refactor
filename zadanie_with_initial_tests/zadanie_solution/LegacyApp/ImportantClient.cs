using System;

namespace LegacyApp
{
    public class ImportantClient : User, IClientType
    {
        public string Name { get; internal set; }
        public int ClientId { get; internal set; }
        public string Email { get; internal set; }
        public string Address { get; internal set; }
        public string Type { get; set; }
        public override void SetUpClient(ICreditInfo creditInfo)
        {
                int creditLimit = creditInfo.GetCreditLimit(this.LastName, this.DateOfBirth);
                creditLimit = creditLimit * 2;
                this.CreditLimit = creditLimit;
                if (creditInfo is IDisposable disposableObj)
                {
                    disposableObj.Dispose();
                }
        }
    }
}
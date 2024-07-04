namespace Dhanman.MyHome.Application.Contracts.ServiceProviders;

public class ServiceProviderValidationResponse
{

    public string PinCode { get; set; }
    public bool IsValid { get; set; }
    public string Message { get; set; }
}

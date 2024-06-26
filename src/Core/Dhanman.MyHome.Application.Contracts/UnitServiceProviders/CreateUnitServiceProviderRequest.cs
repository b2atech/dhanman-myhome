﻿using Dhanman.MyHome.Application.Contracts.ServiceProviders;

namespace Dhanman.MyHome.Application.Contracts.UnitServiceProviders;

public class CreateUnitServiceProviderRequest
{
    #region Properties    

    public List<int> UnitIds { get; set; }
    public int ServiceProviderId { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    #endregion

    #region Constructors
    public CreateUnitServiceProviderRequest() { }
    #endregion
}

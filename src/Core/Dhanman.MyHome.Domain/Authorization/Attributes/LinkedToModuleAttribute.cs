namespace Dhanman.MyHome.Domain.Authorization.Attributes;

public sealed class LinkedToModuleAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LinkedToModuleAttribute"/> class.
    /// </summary>
    /// <param name="paidModules">The paid modules.</param>
    public LinkedToModuleAttribute(PaidModules paidModules) => PaidModules = paidModules;

    /// <summary>
    /// Gets the paid modules.
    /// </summary>
    public PaidModules PaidModules { get; }
}

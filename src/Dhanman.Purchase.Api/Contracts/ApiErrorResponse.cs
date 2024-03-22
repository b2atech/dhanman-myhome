
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Api.Contracts;

public class ApiErrorResponse
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApiErrorResponse"/> class.
    /// </summary>
    /// <param name="errors">The enumerable collection of errors.</param>
    public ApiErrorResponse(IEnumerable<Error> errors)
    {
        Errors = errors;
    }

    /// <summary>
    /// Gets the errors.
    /// </summary>
    public IEnumerable<Error> Errors { get; }
}

namespace Dhanman.Domain.Core.Validation;

public interface IValidator<T>
        where T : class
{
    IValidator<T> SetNext(IValidator<T> next);

    Result.Result Validate(T? item);
}
using Dhanman.MyHome.Domain.Exceptions.Base;

namespace Dhanman.MyHome.Domain.Exceptions;

public sealed class EventOccurrenceNotFoundException : NotFoundException
{
    public EventOccurrenceNotFoundException(int id) : base($"The EventOccurrence with the identifier {id} was not found.")
    {

    }
}
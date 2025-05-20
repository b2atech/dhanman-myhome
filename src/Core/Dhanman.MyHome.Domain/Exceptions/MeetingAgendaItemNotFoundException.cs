using Dhanman.MyHome.Domain.Exceptions.Base;

namespace Dhanman.MyHome.Domain.Exceptions;

public sealed class MeetingAgendaItemNotFoundException : NotFoundException
{
    public MeetingAgendaItemNotFoundException(int id) : base($"The MeetingAgendaItem with the identifier {id} was not found.")
    {

    }
}
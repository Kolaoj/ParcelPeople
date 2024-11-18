namespace ParcelPeople.Domain.Exceptions
{
    public class OriginDestinationConflictException(string message) : Exception(message)
    {
    }
}

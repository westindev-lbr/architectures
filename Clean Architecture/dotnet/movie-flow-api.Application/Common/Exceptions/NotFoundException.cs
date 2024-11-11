namespace movie_flow_api.Application.Common.Exceptions;

public class NotFoundException(string message = null!) : Exception(message)
{
}

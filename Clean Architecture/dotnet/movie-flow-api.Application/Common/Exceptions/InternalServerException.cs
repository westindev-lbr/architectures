namespace movie_flow_api.Application.Common.Exceptions;

public class InternalServerException(string message) : Exception(message)
{
}

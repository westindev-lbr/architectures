namespace movie_flow_api.Application.Common.Exceptions;

public class ServiceUnavailableException(string message) : Exception(message)
{
}

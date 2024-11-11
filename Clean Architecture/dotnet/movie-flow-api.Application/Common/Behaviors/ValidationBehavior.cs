using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace movie_flow_api.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        if (_validators.Any())
        {
            ValidationContext<TRequest> context = new ValidationContext<TRequest>(request);

            IEnumerable<ValidationResult> validationFailures = await Task.WhenAll(
                _validators.Select(validator => validator.ValidateAsync(context, cancellationToken))
            );

            List<ValidationFailure> errors = validationFailures
                .Where(validationresult => !validationresult.IsValid)
                .SelectMany(validationresult => validationresult.Errors)
                .ToList();

            if (errors.Any())
                throw new Exceptions.ValidationException(errors);
        }
        return await next();
    }
}

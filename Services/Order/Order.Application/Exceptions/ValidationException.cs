using FluentValidation.Results;
using System.Collections.Concurrent;

namespace Order.Application.Exceptions;

public class ValidationException : ApplicationException
{
    public ValidationException()
        : base("One or more validation failures occurred!")
    {
        Errors = new ConcurrentDictionary<string, string[]>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures) : this()
    {
        Errors = failures.GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(d => d.Key, d => d.ToArray());
    }

    public IDictionary<string, string[]> Errors { get; set; }
}

namespace CleanArchitectureTemplate.Application.Common.Models;

public class Result
{
    internal Result(bool succeeded, object? model, IEnumerable<string> errors)
    {
        Succeeded = succeeded;
        Errors = errors.ToArray();
        Model = model;
    }

    public bool Succeeded { get; set; }

    public string[] Errors { get; set; }
    public object? Model { get; set; }

    public static Result Success(object model)
    {
        return new Result(true, model, Array.Empty<string>());
    }

    public static Result Success()
    {
        return new Result(true, null, Array.Empty<string>());
    }

    public static Result Failure(IEnumerable<string> errors)
    {
        return new Result(false, null, errors);
    }
}

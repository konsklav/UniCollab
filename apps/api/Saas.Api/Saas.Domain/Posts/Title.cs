using Ardalis.Result;

namespace Saas.Domain.Posts;

/// <summary>
/// Represents a post's title
/// </summary>
public sealed record Title
{
    private Title(string Value)
    {
        this.Value = Value;
    }

    public const int MaxLength = 55;
    public string Value { get; init; }

    public static Result<Title> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Invalid(new ValidationError("A title must not be empty."));

        if (value.Length > MaxLength)
            return Result.Invalid(new ValidationError($"A title must not exceed {MaxLength} characters in length."));

        return new Title(value);
    }

    public void Deconstruct(out string Value)
    {
        Value = this.Value;
    }
}
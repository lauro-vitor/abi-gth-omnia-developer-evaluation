namespace Ambev.DeveloperEvaluation.Domain.QueryResult.Users;

/// <summary>
/// Represents the user's full name, including first and last names.
/// </summary>
public class NameQueryResult
{
    /// <summary>
    /// Gets or sets the user's first name.
    /// This field is required and must not be null or empty.
    /// </summary>
    public string Firstname { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's last name.
    /// This field is required and must not be null or empty.
    /// </summary>
    public string Lastname { get; set; } = string.Empty;
}



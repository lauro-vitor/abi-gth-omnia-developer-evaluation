namespace Ambev.DeveloperEvaluation.Domain.QueryResult.Users;

/// <summary>
/// Represents the user's geolocation, including latitude and longitude.
/// </summary>
public class GeolocationQueryResult
{
    /// <summary>
    /// Gets or sets the latitude of the user's geolocation.
    /// This field is required and must be a valid latitude value in decimal degrees format.
    /// </summary>
    public string Lat { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the longitude of the user's geolocation.
    /// This field is required and must be a valid longitude value in decimal degrees format.
    /// </summary>
    public string Long { get; set; } = string.Empty;
}

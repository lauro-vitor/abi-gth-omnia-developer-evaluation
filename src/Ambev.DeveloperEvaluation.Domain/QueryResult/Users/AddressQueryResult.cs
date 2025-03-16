namespace Ambev.DeveloperEvaluation.Domain.QueryResult.Users;

/// <summary>
/// Represents the user's address, including city, street, number, zip code, and geolocation.
/// </summary>
public class AddressQueryResult
{
    /// <summary>
    /// Gets or sets the city where the user resides.
    /// This field is required and must not be null or empty.
    /// </summary>
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the street where the user resides.
    /// This field is required and must not be null or empty.
    /// </summary>
    public string Street { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the number of the user's residence.
    /// This field is not required.
    /// </summary>
    public int? Number { get; set; }

    /// <summary>
    /// Gets or sets the zip code of the user's residence.
    /// This field is required and must follow the standard zip code format for the user's country.
    /// </summary>
    public string Zipcode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's geolocation, including latitude and longitude.
    /// </summary>
    public GeolocationQueryResult Geolocation { get; set; } = new GeolocationQueryResult();
}


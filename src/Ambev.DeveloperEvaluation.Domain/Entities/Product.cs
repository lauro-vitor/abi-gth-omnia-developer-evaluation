namespace Ambev.DeveloperEvaluation.Domain.Entities;
/// <summary>
/// Represents a product in the system.
/// This class encapsulates the essential attributes of a product, including its identification,
/// title, price, description, category, image, rating, inventory count, and audit information.
/// </summary>
public class Product
{
    /// <summary>
    /// Gets or sets the unique identifier for the product.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the title or name of the product.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the price of the product.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets a detailed description of the product.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category to which the product belongs.
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the URL or path to the product's image.
    /// </summary>
    public string Image { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the rating of the product, typically representing user feedback or reviews.
    /// </summary>
    public decimal Rate { get; set; }

    /// <summary>
    /// Gets or sets the count of the product available in inventory.
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// Gets the date and time when the product was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time of the last update to the product's information.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<CartProductItem> CartProducts { get; set; } = [];

    /// <summary>
    /// Initializes a new instance of the <see cref="Product"/> class.
    /// Sets the creation date to the current date and time in UTC.
    /// </summary>
    public Product()
    {
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Updates the current product with the values from another product instance.
    /// This method is used to synchronize the properties of the current product with those of the provided product.
    /// The <see cref="UpdatedAt"/> property is automatically set to the current date and time in UTC.
    /// </summary>
    /// <param name="product">The product instance containing the updated values.</param>
    public void Update(Product product)
    {
        Title = product.Title;
        Price = product.Price;
        Description = product.Description;
        Category = product.Category;
        Image = product.Image;
        Rate = product.Rate;
        Count = product.Count;
        CreatedAt = product.CreatedAt;
        UpdatedAt = DateTime.UtcNow;
    }
}


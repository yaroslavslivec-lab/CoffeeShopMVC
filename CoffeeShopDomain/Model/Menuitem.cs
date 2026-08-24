using System;
using System.Collections.Generic;

namespace CoffeeShopDomain.Model;

public partial class Menuitem : BaseEntity
{

    public int? CategoryId { get; set; }

    public string ItemName { get; set; } = null!;

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Itemvariation> Itemvariations { get; set; } = new List<Itemvariation>();

    public virtual Nutritionalvalue? Nutritionalvalue { get; set; }

    public virtual ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
}

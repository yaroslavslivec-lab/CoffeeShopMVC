using System;
using System.Collections.Generic;

namespace CoffeeShopDomain.Model;

public partial class Ingredient : BaseEntity
{

    public string IngredientName { get; set; } = null!;

    public virtual ICollection<Menuitem> MenuItems { get; set; } = new List<Menuitem>();
}

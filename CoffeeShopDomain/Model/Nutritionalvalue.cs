using System;
using System.Collections.Generic;

namespace CoffeeShopDomain.Model;

public partial class Nutritionalvalue
{
    public int MenuItemId { get; set; }

    public int? Calories { get; set; }

    public decimal? Proteins { get; set; }

    public decimal? Fats { get; set; }

    public decimal? Carbs { get; set; }

    public virtual Menuitem MenuItem { get; set; } = null!;
}

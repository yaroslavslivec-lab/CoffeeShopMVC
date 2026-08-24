using System;
using System.Collections.Generic;

namespace CoffeeShopDomain.Model;

public partial class Itemvariation : BaseEntity
{

    public int? MenuItemId { get; set; }

    public int? SizeId { get; set; }

    public decimal Price { get; set; }

    public virtual Menuitem? MenuItem { get; set; }

    public virtual Itemsize? Size { get; set; }
}

using System;
using System.Collections.Generic;

namespace CoffeeShopDomain.Model;

public partial class Itemsize : BaseEntity
{

    public string SizeName { get; set; } = null!;

    public virtual ICollection<Itemvariation> Itemvariations { get; set; } = new List<Itemvariation>();
}

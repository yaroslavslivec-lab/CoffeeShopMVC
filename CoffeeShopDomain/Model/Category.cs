using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShopDomain.Model;

public partial class Category : BaseEntity
{
    [Required(ErrorMessage = "Category name is required")]
    [StringLength(255)]
    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Menuitem> Menuitems { get; set; } = new List<Menuitem>();
}

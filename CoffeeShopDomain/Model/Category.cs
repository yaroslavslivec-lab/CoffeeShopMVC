using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShopDomain.Model;

public partial class Category : BaseEntity
{
    [Required(ErrorMessage = "Назва категорії обов'язкова")]
    [StringLength(255)]
    [Display(Name = "Назва категорії")]
    public string CategoryName { get; set; }

    public virtual ICollection<Menuitem> Menuitems { get; set; } = new List<Menuitem>();
}

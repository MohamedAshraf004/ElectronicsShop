using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsShop.Models;

public class Product : IBaseEntity
{
    public int Id { get; set; }

    public string ProductName { get; set; }

    public string Desc { get; set; }

    public double Price { get; set; }
    //public double? PriceAfterDiscount { get; set; }
    //public double DiscountPercentageForRegisteredUsers { get; set; }

    public int CategoryId { get; set; }
    [ForeignKey(nameof(CategoryId))]
    public Category Category { get; set; }

    [InverseProperty(nameof(Models.ProductDiscount.Product))]
    public ICollection<ProductDiscount> ProductDiscount { get; set; }
        = new List<ProductDiscount>();
    public DateTime InsertDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public DateTime? DeleteDate { get; set; }
    public string InsertBy { get; set; }
    public string UpdateBy { get; set; }
    public string DeleteBy { get; set; }
}
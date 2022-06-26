using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsShop.Models;

public class ProductDiscount : IBaseEntity
{
    public int Id { get; set; }

    public int ProductId { get; set; }
    [ForeignKey(nameof(ProductId))]
    [InverseProperty(nameof(Models.Discount.ProductDiscount))]
    public Product Product { get; set; }

    public int DiscountId { get; set; }
    [ForeignKey(nameof(DiscountId))]
    [InverseProperty(nameof(Models.Discount.ProductDiscount))]
    public Discount Discount { get; set; }

    public DateTime InsertDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public DateTime? DeleteDate { get; set; }
    public string InsertBy { get; set; }
    public string UpdateBy { get; set; }
    public string DeleteBy { get; set; }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsShop.Models;

public class Discount :IBaseEntity
{
    public int Id { get; set; }
    public DiscountType DiscountType { get; set; }
    public double DiscountPercentageValue { get; set; }
    [InverseProperty(nameof(Models.ProductDiscount.Discount))]
    public ICollection<ProductDiscount> ProductDiscount { get; set; }
        = new List<ProductDiscount>();
    public DateTime InsertDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public DateTime? DeleteDate { get; set; }
    public string InsertBy { get; set; }
    public string UpdateBy { get; set; }
    public string DeleteBy { get; set; }
}

public enum DiscountType
{
    RegisteredUsersDiscount,
    TwoPiecesDiscount
}
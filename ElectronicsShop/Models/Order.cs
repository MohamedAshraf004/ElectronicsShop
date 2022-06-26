using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectronicsShop.Models;

public class Order : IBaseEntity
{
    public int Id { get; set; }

    public double Total { get; set; }
    public double TotalAfterDiscount { get; set; }

    [MaxLength(500)]
    public string Notes { get; set; }
    [Required]
    public string UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public AppUser User { get; set; }


    public DateTime InsertDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public DateTime? DeleteDate { get; set; }
    public string InsertBy { get; set; }
    public string UpdateBy { get; set; }
    public string DeleteBy { get; set; }
}
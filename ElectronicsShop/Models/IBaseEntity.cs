using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsShop.Models;

public interface IBaseEntity
{
    public int Id { get; set; }

    [Column(TypeName = "datetime")] 
    public DateTime InsertDate { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime UpdateDate { get; set; } 
    [Column(TypeName = "datetime")]
    public DateTime? DeleteDate { get; set; }

    public string InsertBy { get; set; }
    public string UpdateBy { get; set; }
    public string DeleteBy { get; set; }
}
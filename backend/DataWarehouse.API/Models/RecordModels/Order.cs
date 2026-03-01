using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataWarehouse.API.Models.RecordModels;

[Table("Orders")]
public class Order
{
    [Key]
    public int Id { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal Amount { get; set; }
}

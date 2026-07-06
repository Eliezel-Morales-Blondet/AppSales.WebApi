using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppSales.WebApi.Data.Entities
{
    public class SaleDetail
    {
        public int SaleDetailId { get; set; }
        public int SaleId { get; set; }
        [Required] 
        public string ProductName{ get; set; }
        public int Quantity { get; set; }
        [Column(TypeName ="decimal(10,2)")]
        public decimal UnitPrice { get; set; }
        public Sale Sale { get; set; } = null!;




    }
}

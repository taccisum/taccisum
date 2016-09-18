using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    [Table("dbo.OrderItem")]
    public class OrderItem:DTO
    {
        public string OrderNO { get; set; }
        public string ProductNO { get; set; }
        public int ProductNum { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
    }
}

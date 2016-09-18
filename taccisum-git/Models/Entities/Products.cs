using System.ComponentModel.DataAnnotations.Schema;
using Model.Entity;

namespace Model.Entities
{
    [Table("dbo.Product")]
    public class Product : DTO
    {

        public string ProductName { get; set; }
        public string Description { get; set; }
        public string BandName { get; set; }
        public string PictureUrl { get; set; }

        public decimal Price { get; set; }

        public int ProductNum { get; set; }//商品数量

        public bool IsSaled { get; set; }

        public string ProductCode { get; set; }//商品编号

    }
}

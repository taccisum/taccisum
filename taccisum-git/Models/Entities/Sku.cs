using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
     [Table("dbo.Sku")]
     public class Sku : DTO
     {
         public string SkuNum { get; set; }//编码编号
         public string SkuName { get; set; }//编码名称
         public string ProductCode { get; set; }//商品编号

     }
}

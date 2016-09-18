using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    [Table("dbo.Band")]
    public class Band : DTO
    {
        public string BandNum { get; set; }//品牌编号
        public string BandName { get; set; }//品牌名

        public string EnglishName { get; set; }//英文名
        public string EnglishFirstChar { get; set; }//英文首字母

        public string Description { get; set; }//品牌描述
    }
}

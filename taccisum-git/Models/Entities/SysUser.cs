using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entity
{
    [Table("dbo.SysUser")]
    public class SysUser : DTO
    {
        public string Uid { get; set; }
        public string Psd { get; set; }

    }
}
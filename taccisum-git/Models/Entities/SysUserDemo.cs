using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    [Table("dbo.SysUserDemo")]
    public class SysUserDemo : DTO
    {
        public string Account { get; set; }
        public string Password { get; set; }
        public string NickName { get; set; }
    }
}

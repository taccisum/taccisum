using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entity
{
    [Table("dbo.SysMenu")]
    public class SysMenu : DTO
    {
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public int SortNo { get; set; }
        public bool EnabledState { get; set; }
        public string Description { get; set; }
    }
}

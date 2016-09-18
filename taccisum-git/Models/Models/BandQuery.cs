using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Models.CommonModel;

namespace Model.Models
{
    public class BandQuery : DataTablesQuery
    {
        public string BandName { get; set; }
        public string EnglishName { get; set; }
        public string EnglishFirstChar { get; set; }
    }
}

using Model.Models.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class ProductQuery : DataTablesQuery
    {

       public string ProductName { get; set; }

       public string BandName { get; set; }
    }
}

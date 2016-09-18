using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Models.CommonModel;

namespace Model.Models
{
    public class UserDemoQuery : DataTablesQuery
    {
        public string account { get; set; }
        public string nickname { get; set; }
    }
}

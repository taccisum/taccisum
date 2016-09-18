using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models.CommonModel
{
    public class DataTablesQuery
    {
        public int draw { get; set; }
        public int length { get; set; }
        public int start { get; set; }

        public List<object> columns { get; set; }
        public List<object> order { get; set; }
        public SearchObj search { get; set; }

        public int pageindex { get{
            try
            {
                return this.start/this.length + 1;
            }
            catch
            {
                return 1;
            }
        } }

    }

    public class SearchObj
    {
        public bool regex { get; set; }
        public string value { get; set; }
    }

}

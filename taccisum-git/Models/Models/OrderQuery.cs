using Model.Models.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class OrderQuery : DataTablesQuery
    {

        /// <summary>
        /// 顾客姓名
        /// </summary>
        public string Name{get;set;}
        /// <summary>
        /// 收货地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 下单时间开始
        /// </summary>
        public DateTime startTime { get; set; }
        /// <summary>
        /// 下单时间结束
        /// </summary>
        public DateTime endTime { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNO { get; set; }
    }
}

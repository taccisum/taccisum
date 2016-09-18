using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entity;
using Repository.Dao.Interf.Orders.Product;
using Service.Base;
using Service.Interf.Orders.Product;

namespace Service.Impl.Orders.Product
{
    [Export(typeof(IProductOrderItemService))]
    public class ProductOrderItemServiceImpl : BaseService, IProductOrderItemService
    {
        [Import]
        protected IProductOrderItemDao ProductOrderItemDao { get; set; }

        public OrderItem GetOrderItem(string OrderNO)
        {
            OrderItem orderItem = ProductOrderItemDao.Query().FirstOrDefault(oi => oi.OrderNO.Equals(OrderNO));

            return orderItem;
        }

        public int AddOrderItem(OrderItem oi)
        {
            if (oi != null)
            {
                ProductOrderItemDao.Create(oi,false);
            }
            if (ProductOrderItemDao.Submit() != -1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}

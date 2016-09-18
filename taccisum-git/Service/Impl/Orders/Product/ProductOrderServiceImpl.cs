using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Model.Entity;
using Model.Models;
using Repository.Dao.Interf.Orders.Product;
using Service.Base;
using Service.Interf.Orders.Product;

namespace Service.Impl.Orders.Product
{
    [Export(typeof(IProductOrderService))]
    public class ProductOrderServiceImpl : BaseService, IProductOrderService
    {
        [Import]
        protected IProductOrderDao ProductOrderDao { get; set; }
        public Order getById(Guid id)
        {
            return ProductOrderDao.GetEntity(id);
        }

        public List<Order> getOrderList(OrderQuery orderQuery)
        {
            var orders = ProductOrderDao.Query();

            if (orderQuery == null)
            {
                orderQuery = new OrderQuery();
            }
            //收货地址模糊查询
            if (!string.IsNullOrEmpty(orderQuery.Address))
            {
                orders = orders.Where(p => p.Address.Contains(orderQuery.Address));
            }
            //联系方式模糊查询
            if (!string.IsNullOrEmpty(orderQuery.Phone))
            {
                orders = orders.Where(p => p.Phone.Contains(orderQuery.Phone));
            }
            //顾客姓名模糊查询
            if (!string.IsNullOrEmpty(orderQuery.Name))
            {
                orders = orders.Where(p => p.Name.Contains(orderQuery.Name));
            }
            //订单号条件查询
            if (!string.IsNullOrEmpty(orderQuery.OrderNO))
            {

                orders = orders.Where(p => p.OrderNO.Equals(orderQuery.OrderNO));

            }
            orders = orders.OrderByDescending(o => o.CreatedOn);
            orders = orders.Skip(orderQuery.start).Take(orderQuery.length);

            return orders != null ? orders.ToList() : new List<Order>();
        }

        public int OrderCount(OrderQuery orderQuery)
        {
            var orders = ProductOrderDao.Query();

            if (orderQuery == null)
            {
                orderQuery = new OrderQuery();
            }
            //收货地址模糊查询
            if (!string.IsNullOrEmpty(orderQuery.Address))
            {
                orders = orders.Where(p => p.Address.Contains(orderQuery.Address));
            }
            //联系方式模糊查询
            if (!string.IsNullOrEmpty(orderQuery.Phone))
            {
                orders = orders.Where(p => p.Phone.Contains(orderQuery.Phone));
            }
            //顾客姓名模糊查询
            if (!string.IsNullOrEmpty(orderQuery.Name))
            {
                orders = orders.Where(p => p.Name.Contains(orderQuery.Name));
            }
            //订单号条件查询
            if (!string.IsNullOrEmpty(orderQuery.OrderNO))
            {

                orders = orders.Where(p => p.OrderNO.Equals(orderQuery.OrderNO));

            }
            int count = orders.Count();
            return count;
        }

        public int addOrder(Order order)
        {
            if (order != null)
            {
                ProductOrderDao.Create(order, false);
            }
            return ProductOrderDao.Submit() != -1 ? 1 : 0;
        }
    }
}

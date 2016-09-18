using System;
using System.Collections.Generic;
using Model.Entity;
using Model.Models;

namespace Service.Interf.Orders.Product
{
    public interface IProductOrderService
    {
        /// <summary>
        /// 根据ID(订单号获取订单信息)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Order getById(Guid id);
        /// <summary>
        /// 根据查询条件获取订单列表
        /// </summary>
        /// <param name="orderQuery"> 查询条件对象</param>
        /// <returns></returns>
        List<Order> getOrderList(OrderQuery orderQuery);
        /// <summary>
        /// 获取订单总记录数，分页使用
        /// </summary>
        /// <param name="orderQuery"></param>
        /// <returns></returns>
        int OrderCount(OrderQuery orderQuery);
        /// <summary>
        /// 下单操作（立即购买）
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        int addOrder(Order order);
    }
}

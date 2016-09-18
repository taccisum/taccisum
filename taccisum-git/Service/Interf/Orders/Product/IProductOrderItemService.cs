using Model.Entity;

namespace Service.Interf.Orders.Product
{
    public interface IProductOrderItemService
    {
        /// <summary>
        /// 根据订单编号获取订单详情
        /// </summary>
        /// <param name="OrderNO"></param>
        /// <returns></returns>
        OrderItem GetOrderItem(string OrderNO);
        int AddOrderItem(OrderItem oi);
    }
}

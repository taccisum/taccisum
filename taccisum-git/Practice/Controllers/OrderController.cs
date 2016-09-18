using Model.Common;
using Model.Entity;
using Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Practice.Controllers.Base;
using Service.Interf.Orders.Product;

namespace Practice.Controllers
{
    [Export]
    public class OrderController :  BaseController
    {
        [Import]
        protected IProductOrderService ProductOrderService { get; set; }
        [Import]
        protected IProductOrderItemService ProductOrderItemService { get; set; }
        private Order order ;
        private OrderItem orderItem;
        
        public ActionResult OrderList()
        {
            return View();
        }
        public ActionResult GetOrderList(OrderQuery orderQuery)
        {
            var ods = this.ProductOrderService.getOrderList(orderQuery);
            var tableData = new {
                draw = orderQuery.draw,
                recordsTotal = ods.Count(),
                recordsFiltered = ods.Count(),
                data = ods.Select(p => new { p.OrderNO, p.Name, p.Address, Phone = p.Phone, CreatedOn = p.CreatedOn.ToString("yyyy-MM-dd HH:mm:ss") }).ToList()
            };

            return Json( tableData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetOrderItem(string orderNO)
        {
            var item = this.ProductOrderItemService.GetOrderItem(orderNO);
            return Json(item,JsonRequestBehavior.AllowGet);

        }
        public ActionResult addOrder()
        {
            return View();
        }
        public ActionResult orderNow(AddOrder aor)
        {
            string OrderNO = "ON";
            Random rd = new Random();           
            string strTm = DateTime.Now.ToString("yyyyMMddHHmmfff");
            OrderNO += strTm + rd.Next(100, 999);            

            order=new Order();
            order.Name = aor.Name;
            order.Address = aor.Address;
            order.Phone = aor.Phone;
            order.OrderNO = OrderNO;

            orderItem = new OrderItem();
            orderItem.OrderNO = OrderNO;
            orderItem.ProductNO = aor.ProductNO;
            orderItem.ProductPrice = aor.ProductPrice;
            orderItem.ProductNum = aor.ProductNum;
            orderItem.ProductName = aor.ProductName;

            int resOrder = this.ProductOrderService.addOrder(order);

            int resOrderItem = this.ProductOrderItemService.AddOrderItem(orderItem);

            if (resOrder == 1 && resOrderItem==1)
            {
                return Json(new ApiResult()
                {
                    Success = true,
                    Data = null,
                    Message = "购买成功"
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new ApiResult()
                {
                    Success = false,
                    Data = null,
                    Message = "购买失败"
                }, JsonRequestBehavior.AllowGet);
            }          
            
        }
    }
}
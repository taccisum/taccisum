using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Entity;
using Model.Common;
using Model.Models;
using System.IO;
using String = Common.Tool.Extend._string;
using System.Collections;
using System.ComponentModel.Composition;
using Model.Entities;
using Practice.Controllers.Base;
using Service.Interf.Product;


namespace Practice.Controllers
{
    [Export]
    public class ProductController : BaseController
    {
        [Import]
        protected IProductService ProductService { get; set; }


        // GET: /Product/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductInfo()
        {
            return View();
        }

        public ActionResult OnSaledProduct(string id,string type)
        {
            ApiResult result;
            Guid guid = String.ToGuid(id);
            result = ProductService.OnSaledProduct(guid,type);
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase fileData)
        {
            ApiResult result;
            if (fileData == null)
            {
                result = new ApiResult()
                {
                    Success = false,
                    Message = "文件为空"
                };
            }

            // 文件上传后的保存路径
            string filePath = Server.MapPath("/uploadfiles/");//真实路径
            string serverPath = "/uploadfiles/";//服务器相对路径
            string fileName = Path.GetFileName(fileData.FileName);      //原始文件名称
            string fileExtension = Path.GetExtension(fileName);         //文件扩展名
            string serverUrl = Guid.NewGuid().ToString() + fileExtension;
            string saveName = filePath + serverUrl; //保存文件名称

            try
            {
                fileData.SaveAs(saveName);
                result = new ApiResult()
                {
                    Success = true,
                    Data = serverPath + serverUrl,
                    Message = "文件成功"
                };
            }
            catch
            {
                result = new ApiResult()
                {
                    Success = false,
                    Data = serverUrl,
                    Message = "文件上传失败"
                };
            }


            //return Content("上传成功！", "text/plain");
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        public JsonResult InsertProduct(string jsonStr)
        {

           ApiResult result = ProductService.Add(jsonStr);
           return Json(result, JsonRequestBehavior.DenyGet);
        }

       

        public JsonResult Remove(string idList)
        {
            ApiResult result;

            result = ProductService.Remove(idList);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProductList(ProductQuery query)
        {
            var list = ProductService.GetProductList(query);
            int count = ProductService.countProduct(query);
            var tableData = new
            {
                draw = query.draw,
                recordsTotal = count,
                recordsFiltered = count,
                data = list
            };

            return Json(tableData, JsonRequestBehavior.AllowGet);
        }



        public JsonResult Edit(Product p)
        {

            int rowNum = ProductService.Edit(p);
            //先判断 必要参数
            var result = new ApiResult()
            {
                Success = rowNum >= 1 ? true : false,
                Data = p,
                Message = "成功"
            };

            return Json(result, JsonRequestBehavior.DenyGet);
        }


    }

}

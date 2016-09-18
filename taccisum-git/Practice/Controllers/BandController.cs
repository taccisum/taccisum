using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Common;
using Model.Entity;
using Model.Models;
using Practice.Controllers.Base;
using Service.Interf.Bands.Product;

namespace Practice.Controllers
{
    [Export]
    public class BandController : BaseController
    {
        [Import]
        protected IProductBandService ProductBandService { get; set; }

        // GET: /Band/
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult InsertBand(Band band)
        {
            return Json(ProductBandService.Add(band), JsonRequestBehavior.DenyGet);
        }



        public JsonResult GetForProductBandList()
        {
            var bandlist = ProductBandService.GetBandList();

            return Json(bandlist, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBandList(BandQuery query)
        {
            var bandlist = ProductBandService.GetBandList(query);
            int count = ProductBandService.countBand(query);
            var tableData = new
            {
                draw = query.draw,
                recordsTotal = count,
                recordsFiltered = count,
                data = bandlist
            };
            return Json(tableData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Remove(string idList)
        {
            ApiResult result;

            result = ProductBandService.Remove(idList);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Edit(Band band)
        {

            ApiResult result = ProductBandService.Edit(band);


            return Json(result, JsonRequestBehavior.DenyGet);
        }

    }
}
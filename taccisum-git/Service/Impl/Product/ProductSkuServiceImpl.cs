using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Tool.Extend;
using Model.Common;
using Model.Entity;
using Model.Models;
using Repository.Dao.Interf.Product;
using Repository.Generic;
using Service.Base;
using Service.Interf.Product;

namespace Service.Impl.Product
{
    [Export(typeof(IProductSkuService))]
    public class ProductSkuServiceImpl: BaseService, IProductSkuService
    {
        [Import]
        protected IProductSkuDao ProductSkuDao { get; set; }


        public Sku GetById(Guid id)
        {
            return ProductSkuDao.GetEntity(id);
        }

        public List<Sku> GetSkuList(SkuQuery skuQuery)
        {
            var skus = ProductSkuDao.Query();
            skus = skus.Where(p => p.ProductCode == skuQuery.ProductCode);

            skus = skus.OrderBy(m => m.CreatedOn).Skip(skuQuery.start).Take(skuQuery.length);
            return skus != null ? skus.ToList() : new List<Sku>(); 
        }

        public int countBand()
        {
            throw new NotImplementedException();
        }

        public ApiResult Edit(Sku sku)
        {
            Sku oldSku = ProductSkuDao.Query(t => t.ID == sku.ID).FirstOrDefault();
            ApiResult result;

            //在赋值
            oldSku.SkuName = sku.SkuName;

            ProductSkuDao.Update(oldSku, false);
            int affectNum = ProductSkuDao.Submit();

            bool isUpdateSuccess = affectNum > 0 ? true : false;

            if (isUpdateSuccess)
            {
                result = ApiResult.SuccessResult(sku, "编辑商品规格成功");
            }
            else
            {
                result = ApiResult.FailedResult("编辑商品规格失败");
            }

            return result;
        }

        public ApiResult Add(Sku sku)
        {
            ApiResult result;
            sku.SkuNum = _string.GetSequence("SKU");


            int affectNum;
            try
            {
                ProductSkuDao.Create(sku,false);
                affectNum = ProductSkuDao.Submit();
            }
            catch (Exception)
            {
                result = ApiResult.FailedResult("添加商品规格失败");
                throw;
            }

            bool isUpdateSuccess = affectNum > 0 ? true : false;

            if (isUpdateSuccess)
            {
                result = ApiResult.SuccessResult(sku, "添加商品规格成功");
            }
            else
            {
                result = ApiResult.FailedResult("添加商品规格失败");
            }

            return result;
        }

        public void Delete(Guid id)
        {
            ProductSkuDao.Delete(id);
        }

        public ApiResult Remove(string idList)
        {
            ApiResult result;

            var idArr = idList.Split(',');

            if (idArr.Any() && !string.IsNullOrWhiteSpace(idArr[0]))
            {
                var ids = idArr.Select(id => id.ToGuid());

                foreach (var id in ids)
                {
                    ProductSkuDao.Delete(id, false);
                }

                if (ProductSkuDao.Submit() != -1)
                {
                    return result = ApiResult.SuccessResult(ids.Count(), "删除成功");
                }
                else
                {
                    return result = ApiResult.FailedResult("删除失败");
                }
            }
            else
            {
                return result = ApiResult.SuccessResult(0, "未选中任何数据");
            }
        }
    }
}

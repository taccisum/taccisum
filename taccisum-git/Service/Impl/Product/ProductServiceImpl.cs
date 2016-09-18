using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Tool.Extend;
using Model.Common;
using Model.Entity;
using Model.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Repository.Dao.Interf.Product;
using Repository.Generic;
using Service.Base;
using Service.Interf.Product;

namespace Service.Impl.Product
{
    [Export(typeof(IProductService))]
    public class ProductServiceImpl : BaseService, IProductService
    {
        [Import]
        protected IProductDao ProductDao { get; set; }

        public Model.Entities.Product GetById(Guid id)
        {
            return ProductDao.GetEntity(id);
        }

        public List<Model.Entities.Product> GetProductList(ProductQuery query)
        {
            var products = ProductDao.Query();

            if (query == null)
            {
                query = new ProductQuery();
            }


            if (!string.IsNullOrWhiteSpace(query.ProductName))
            {
                products = products.Where(p => p.ProductName.Contains(query.ProductName.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(query.BandName))
            {
                products = products.Where(p => p.BandName.Contains(query.BandName.Trim()));
            }

            products = products.OrderBy(m => m.CreatedOn).Skip(query.start).Take(query.length);
            return products != null ? products.ToList() : new List<Model.Entities.Product>();
        }

        public int countProduct(ProductQuery productQuery)
        {
            var products = ProductDao.Query();
            if (productQuery == null)
            {
                productQuery = new ProductQuery();
            }
            
            if (!string.IsNullOrWhiteSpace(productQuery.ProductName))
            {
                products = products.Where(p => p.ProductName.Contains(productQuery.ProductName));
            }
            if (!string.IsNullOrWhiteSpace(productQuery.BandName))
            {
                products = products.Where(p => p.BandName.Contains(productQuery.BandName));
            }

            int count = products.Count();
            return count;
        }

        public int Edit(Model.Entities.Product product)
        {
            Model.Entities.Product oldProduct = ProductDao.Query(t => t.ID == product.ID).FirstOrDefault();

            //在赋值
            oldProduct.Description = product.Description;
            oldProduct.BandName = product.BandName;
            oldProduct.ProductName = product.ProductName;
            oldProduct.Price = product.Price;
            oldProduct.ProductNum = product.ProductNum;
            oldProduct.PictureUrl = product.PictureUrl;

            if (oldProduct.ProductCode == "")
            {
                oldProduct.ProductCode = _string.GetSequence("P");
            }
            ProductDao.Update(oldProduct, false);
            int rows = ProductDao.Submit();

            return rows;
        }

        public int Add(Model.Entities.Product product)
        {
            System.Guid guid = new Guid();
            guid = Guid.NewGuid();
            product.ID = guid;
            product.IsSaled = false;
            product.ProductCode = _string.GetSequence("P");
            if (product.PictureUrl == null || product.PictureUrl == "")
            {
                product.PictureUrl = "/uploadfiles/nopic.jpg";
            }

            ProductDao.Create(product, false);

            return ProductDao.Submit();
        }

        public void Delete(Guid id)
        {
            ProductDao.Delete(id, false);
        }

        public ApiResult Add(string jsonStr)
        {
            throw new NotImplementedException();
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
                    ProductDao.Delete(id, false);
                }

                if (ProductDao.Submit() != -1)
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

        public ApiResult OnSaledProduct(Guid id, string type)
        {
            ApiResult result;
            Model.Entities.Product onSaleingProduct = ProductDao.Query(t => t.ID == id).FirstOrDefault();

            if (type == "downSaledProduct")
            {
                onSaleingProduct.IsSaled = false;
            }
            else
            {
                onSaleingProduct.IsSaled = true;
            }



            int affectNum = ProductDao.Submit();
            bool isUpdateSuccess = affectNum > 0 ? true : false;

            if (isUpdateSuccess)
            {
                result = ApiResult.SuccessResult(type, "");
            }
            else
            {
                result = ApiResult.FailedResult("删除失败");
            }

            return result;
        }
    }
}

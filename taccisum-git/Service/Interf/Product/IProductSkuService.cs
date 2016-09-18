using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Common;
using Model.Entity;
using Model.Models;

namespace Service.Interf.Product
{
    public interface IProductSkuService
    {
        Sku GetById(Guid id);
        List<Sku> GetSkuList(SkuQuery skuQuery);
        int countBand();
        ApiResult Edit(Sku sku);
        ApiResult Add(Sku sku);
        void Delete(Guid id);
        ApiResult Remove(string idList);
    }
}

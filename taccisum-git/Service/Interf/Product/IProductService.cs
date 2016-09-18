using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Common;
using Model.Models;

namespace Service.Interf.Product
{
    public interface IProductService 
    {
        Model.Entities.Product GetById(Guid id);

        List<Model.Entities.Product> GetProductList(ProductQuery query);

        int countProduct(ProductQuery productQuery);
        int Edit(Model.Entities.Product product);
        int Add(Model.Entities.Product product);
        void Delete(Guid id);

        ApiResult Add(string jsonStr);
        ApiResult Remove(string idList);
        ApiResult OnSaledProduct(Guid id, string type);
    }
}

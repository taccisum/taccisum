using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entity;
using Repository.Dao.Interf.Product;
using Repository.Repository.Base;

namespace Repository.Dao.Impl.Product
{
    [Export]
    public class ProductSkuDaoImpl: RepositorySupport<Sku>, IProductSkuDao
    {
    }
}

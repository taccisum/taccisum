using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entity;
using Repository.Dao.Interf.Orders.Product;
using Repository.Repository.Base;

namespace Repository.Dao.Impl.Orders.Product
{
    [Export(typeof(IProductOrderDao))]
    public class ProductOrderDaoImpl : RepositorySupport<Order>, IProductOrderDao
    {
    }
}

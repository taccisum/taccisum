using System.ComponentModel.Composition;
using Model.Entity;
using Repository.Dao.Interf.Bands.Product;
using Repository.Repository.Base;

namespace Repository.Dao.Impl.Bands.Product
{
    [Export(typeof(IProductBandsDao))]
    public class ProductBandsDaoImpl : RepositorySupport<Band>, IProductBandsDao
    {
    }
}

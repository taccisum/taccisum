using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Common;
using Model.Entity;
using Model.Models;

namespace Service.Interf.Bands.Product
{
    public interface IProductBandService
    {
        Band GetById(Guid id);
        List<Band> GetBandList(BandQuery bandQuery);
        List<Band> GetBandList();
        int countBand(BandQuery bandQuery);
        ApiResult Edit(Band band);
        ApiResult Add(Band band);
        void Delete(Guid id);
        ApiResult Remove(string idList);
    }
}

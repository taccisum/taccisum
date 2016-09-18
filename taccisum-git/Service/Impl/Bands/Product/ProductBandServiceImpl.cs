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
using Repository.Dao.Interf.Bands.Product;
using Service.Base;
using Service.Interf.Bands.Product;

namespace Service.Impl.Bands.Product
{
    [Export(typeof(IProductBandService))]
    public class ProductBandServiceImpl : BaseService, IProductBandService
    {
        [Import]
        protected IProductBandsDao ProductBandsDao { get; set; }


        public Band GetById(Guid id)
        {
            return ProductBandsDao.GetEntity(id);
        }

        public List<Band> GetBandList(BandQuery bandQuery)
        {
            var bands = ProductBandsDao.Query();

            if (bandQuery == null)
            {
                bandQuery = new BandQuery();
            }


            if (!string.IsNullOrWhiteSpace(bandQuery.BandName))
            {
                bands = bands.Where(p => p.BandName.Contains(bandQuery.BandName.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(bandQuery.EnglishName))
            {
                bands = bands.Where(p => p.EnglishName.Contains(bandQuery.EnglishName.Trim()));
            }

            if (!string.IsNullOrWhiteSpace(bandQuery.EnglishFirstChar))
            {
                bands = bands.Where(p => p.EnglishFirstChar.Contains(bandQuery.EnglishFirstChar.Trim()));
            }
            bands = bands.OrderBy(m => m.CreatedOn).Skip(bandQuery.start).Take(bandQuery.length);
            return bands != null ? bands.ToList() : new List<Band>(); 
        }

        public List<Band> GetBandList()
        {
            var bands = ProductBandsDao.Query().OrderBy(m => m.CreatedOn);

            return bands != null ? bands.ToList() : new List<Band>();
        }

        public int countBand(BandQuery bandQuery)
        {
            var bands = ProductBandsDao.Query();

            if (bandQuery == null)
            {
                bandQuery = new BandQuery();
            }


            if (!string.IsNullOrWhiteSpace(bandQuery.BandName))
            {
                bands = bands.Where(p => p.BandName.Contains(bandQuery.BandName.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(bandQuery.EnglishName))
            {
                bands = bands.Where(p => p.EnglishName.Contains(bandQuery.EnglishName.Trim()));
            }

            if (!string.IsNullOrWhiteSpace(bandQuery.EnglishFirstChar))
            {
                bands = bands.Where(p => p.EnglishFirstChar.Contains(bandQuery.EnglishFirstChar.Trim()));
            }

            int count = bands.Count();
            return count;
        }

        public ApiResult Edit(Band band)
        {
            Band oldBand = ProductBandsDao.Query(t => t.ID == band.ID).FirstOrDefault();
            ApiResult result;

            //在赋值
            oldBand.Description = band.Description;
            oldBand.BandName = band.BandName;
            oldBand.BandNum = band.BandNum;
            oldBand.EnglishFirstChar = band.EnglishFirstChar;
            oldBand.EnglishName = band.EnglishName;

            ProductBandsDao.Update(oldBand, false);
            int affectNum = ProductBandsDao.Submit();

            bool isUpdateSuccess = affectNum > 0 ? true : false;

            if (isUpdateSuccess)
            {
                result = ApiResult.SuccessResult(band, "编辑品牌成功");
            }
            else
            {
                result = ApiResult.FailedResult("编辑品牌失败");
            }

            return result;
        }

        public ApiResult Add(Band band)
        {
            ApiResult result;
            band.BandNum = _string.GetSequence("B");


            int affectNum;
            try
            {
                ProductBandsDao.Create(band, false);
                affectNum = ProductBandsDao.Submit();
            }
            catch (Exception)
            {
                result = ApiResult.FailedResult("添加品牌失败");
                throw;
            }

            bool isUpdateSuccess = affectNum > 0 ? true : false;

            if (isUpdateSuccess)
            {
                result = ApiResult.SuccessResult(band, "添加品牌成功");
            }
            else
            {
                result = ApiResult.FailedResult("添加品牌失败");
            }

            return result;
        }

        public void Delete(Guid id)
        {
            ProductBandsDao.Delete(id, false);
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
                    ProductBandsDao.Delete(id, false);
                }

                if (ProductBandsDao.Submit() != -1)
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

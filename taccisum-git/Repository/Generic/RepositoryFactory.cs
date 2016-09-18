using System.Collections.Generic;
using Model.Entity;
using Repository.Context;

namespace Repository.Generic
{
    public class RepositoryFactory
    {
        private Dictionary<string, object> _units;
        private TacContext _context = new TacContext();


        public RepositoryFactory()
        {
            _units = new Dictionary<string, object>();
        }


        public IGenericRepository<TModel> At<TModel>() where TModel : DTO
        {
            var key = typeof (TModel).FullName;

            if (_units.ContainsKey(key))
            {
                return (IGenericRepository<TModel>) _units[key];
            }
            else
            {
                var unit = new GenericRepository<TModel>(_context);
                _units.Add(key, unit);

                return unit;
            }
        }

        public int Submit()
        {
            return _context.SaveChanges();
        }
    }
}

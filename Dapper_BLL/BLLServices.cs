using AutoMapper;
using Dapper_BLL.Interfaces;
using Dapper_DAL;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper_BLL
{
    //abstract generic class. Paramater T for communication with presentation level (BLL object), K - with DAL (DAL object)
    public abstract class BLLServices<T, K> : IGeneralService<T> where T : class, IEntityBLL where K : class, IEntityDAL
    {
        protected DapperRepository<K> _curDapperRep;
        public virtual int Add(T item)
        {
            try
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<T, K>()).CreateMapper();
                var itemToAdd = mapper.Map<T, K>(item);
                int addResult = _curDapperRep.Add(itemToAdd);
                return addResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }
        public virtual bool Delete(T item)
        {
            bool deleteResult = false;
            var publisherById = _curDapperRep.GetById(item.Id);
            if (publisherById != null)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<T, K>()).CreateMapper();
                var itemToDelete = mapper.Map<T, K>(item);
                try
                {
                    deleteResult = _curDapperRep.Delete(itemToDelete);
                    return deleteResult;
                }
                catch (Exception)
                {
                    Console.WriteLine("Current item has external links, cant delete");
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public virtual IEnumerable<T> GetAll()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<K, T>()).CreateMapper();
            return mapper.Map<IEnumerable<K>, List<T>>(_curDapperRep.GetAll());
        }
        public virtual T GetById(int id)
        {
            var publisherById = _curDapperRep.GetById(id);
            if (publisherById != null)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<K, T>()).CreateMapper();
                return mapper.Map<K, T>(publisherById);

            }
            Console.WriteLine($"Item with Id={id} does not exist");
            return null;
        }
        public virtual bool Update(T item)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<T, K>()).CreateMapper();
            var itemToUpdate = mapper.Map<T, K>(item);
            bool updResult = _curDapperRep.Update(itemToUpdate);
            return updResult;
        }
    }
}

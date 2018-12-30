using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ashyana.UI.Web.Repository
{
    public interface IBaseRepository<T> where T: class
    {
        IEnumerable<T> GetModel();
        T GetModelByID(int modelId);
        void InsertModel(T model);
        void DeleteModel(int modelID);
        void UpdateModel(T model);
        void Save();


    
        //T SingleOrDefault(Expression<Func<T, bool>> whereCondition);
        //IEnumerable<T> GetAll(Expression<Func<T, bool>> whereCondition);
        //void Delete(Expression<Func<T, bool>> whereCondition);
        //bool Exists(Expression<Func<T, bool>> whereCondition);

    }
}

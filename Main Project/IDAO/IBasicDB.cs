using Main_Project.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Project.IDAO
{
    public interface IBasicDB<T> where T : IPoco
    {
        T Get(long ID);
        IList<T> GetAll();
         void Add(T t);
        void Remove(T t);
        void Update(T t);
    }
}

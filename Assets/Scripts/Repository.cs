using System.Collections.Generic;

namespace DefaultNamespace
{
    public interface Repository<T>
    {
        public List<T> GetAll();
        public void Add(T t);
        public void Remove(T t);
        public T Get(int index);
        
    }
}
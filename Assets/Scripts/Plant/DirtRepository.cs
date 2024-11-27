using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace Plant
{
    public class DirtRepository: MonoBehaviour, Repository<Dirt>
    {
        [SerializeField] List<Dirt> dirts;
        public List<Dirt> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public void Add(Dirt t)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(Dirt t)
        {
            throw new System.NotImplementedException();
        }

        public Dirt Get(int index)
        {
            throw new System.NotImplementedException();
        }
    }
    
    
}
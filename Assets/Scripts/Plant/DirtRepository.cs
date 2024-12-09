using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

namespace Plant
{
    public class DirtRepository: MonoBehaviour, Repository<Dirt>
    {
        [SerializeField] List<Dirt> dirts;
        private float distanceMin = 10;
        private int nextId = 0;
        public List<Dirt> GetAll()
        {
            return this.dirts;
        }

        public void Add(Dirt t)
        {
            if (t == null)
            {
                throw new ArgumentException();
            }

            if (dirts.Contains(t))
            {
                throw new ArgumentException();
            }

            if (isDirtPositionValid(t))
            {
                throw new ArgumentException();
            }
            
            this.dirts.Add(t);
        }

        public void Remove(Dirt t)
        {
            this.dirts.Remove(t);
        }

        public Dirt GetIndex(int index)
        {
            return this.dirts.GetRange(index, 1)[0];
        }

        public Dirt GetId(int id)
        {
            return dirts.Where(dirt => dirt.Id == id).FirstOrDefault();
        }

        public int GetId()
        {
            return this.nextId++;
        }

        private bool isDirtPositionValid(Dirt dirt)
        {
            foreach (Dirt dirtSaved in dirts)
            {
                if (Vector3.Distance(dirt.transform.position, dirtSaved.transform.position) < this.distanceMin)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
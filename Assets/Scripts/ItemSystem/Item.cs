using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemSystem
{
    [Serializable]
    public class Item : MonoBehaviour
    {
        public ItemData item { get; private set; }
        public GameObject prefab { get; private set; }
        public float weight { get; private set; }
    }
}

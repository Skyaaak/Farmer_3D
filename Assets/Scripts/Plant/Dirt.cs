using System.Collections;
using System.Collections.Generic;
using Plant;
using UnityEngine;

public class Dirt : MonoBehaviour
{
    public int Id { get; }
    private DirtRepository repository;
    
    public Dirt(DirtRepository repository)
    {
        this.repository = repository;
        this.Id = this.repository.GetId();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

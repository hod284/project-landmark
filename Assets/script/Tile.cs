using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enumspace;
public class Tile : MonoBehaviour
{
    public tileuse tilestate;
    // Start is called before the first frame update
    private void Awake()
    {
        tilestate = tileuse.notuse;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

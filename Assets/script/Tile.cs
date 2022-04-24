using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enumspace;
public class Tile : MonoBehaviour
{
    public monsterroad mroad;
    public buildingstate bstate;
    // Start is called before the first frame update
    private void Awake()
    {
       mroad = monsterroad.notuse;
        bstate = buildingstate.notuse;
    }

    // Update is called once per frame

}

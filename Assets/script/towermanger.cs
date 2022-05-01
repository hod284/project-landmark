using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enumspace;

public class towermanger : MonoBehaviour
{
    public GameObject cube;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void setbuilding()
    {
        var child = cube.transform.GetComponentInChildren<BuildingInformation>();
        if (cube.GetComponent<Renderer>().material == child.canbuilt)
        {
            var building = Instantiate(child, transform);
            building.transform.position = cube.transform.position;
            var sitechild = GameObject.FindObjectOfType<Tilemanger>().GetComponentsInChildren<Tile>();
            List<Tile> arrayList = new List<Tile>(sitechild);
            var site = arrayList.Find(x => x.transform.position == child.transform.position);
            var sx = 0;
            int.TryParse(site.gameObject.name[0].ToString(), out sx);
            var sz = 0;
            int.TryParse(site.gameObject.name[1].ToString(), out sz);
            for (int i = sx - (int)child.leftx; i < sx + (int)child.rightx; i++)
            {
                for (int j = sz - (int)child.leftz; j < sz + (int)child.rightz; j++)
                {
                    var name = i.ToString() + j.ToString();
                    arrayList.Find(x => x.name == name).bstate = buildingstate.use;
                }
            }
        }
    } 
}

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
            var x = 0;
            int.TryParse(site.gameObject.name[0].ToString(), out x);
            var z = 0;
            int.TryParse(site.gameObject.name[1].ToString(), out z);
            for (int i = x - (int)child.leftx; i < x + (int)child.rightx; i++)
            {
                for (int j = z - (int)child.leftz; j < z + (int)child.rightz; j++)
                {
                    var name = i.ToString() + j.ToString();
                    arrayList.Find(x => x.name == name).bstate = buildingstate.use;
                }
            }
        }
    } 
}

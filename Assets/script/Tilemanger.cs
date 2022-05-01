using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using Enumspace;
public class Tilemanger : MonoBehaviour
{
    public GameObject tilecolider;
    public GameObject block;
    public int xcolidercount;
    public int zcolidercount;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < xcolidercount; i++)
        {
            var x = 0.0f;
            var xname = "";
            if (i == 0)
                x = 0.5f;
             else
                 x = (i + 0.5f);
             var n = i;
             xname = n.ToString();
            for (int j = 0; j < zcolidercount; j++)
            {
                var z = 0.0f;
                var zname = "";
                    if (j == 0)
                        z = 0.5f;
                    else
                        z = (j + 0.5f);
                n = j;
                zname = n.ToString();
                var mapcolider = Instantiate(tilecolider, new Vector3(x, 0, z), Quaternion.identity,transform);
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(xname);
                stringBuilder.Append(zname);
                mapcolider.gameObject.name = stringBuilder.ToString();
            }
        }
    }

    public void monsterroadselect()
    {
        StartCoroutine("enumerator");
      
    }
    public void Resettile()
    {
        var child = GetComponentsInChildren<Tile>();
        for (int i = 0; i < child.Length; i++)
        {
            if (child[i].bstate == buildingstate.use)
            {
                Destroy(child[i].gameObject);
                child[i].bstate = buildingstate.notuse;
            }
        }
    }
    IEnumerator enumerator()
    {
        var child = GetComponentsInChildren<Tile>();
        for (int i = 0; i < child.Length; i++)
        {
            if (child[i].mroad == monsterroad.use)
            {
                Instantiate(block, child[i].transform);
                yield return  new WaitForSeconds(0.5f);
            }
        }
    }
}

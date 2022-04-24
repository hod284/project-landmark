using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enumspace;
using System.IO;

namespace Data
{
public class SaveandLoadManger : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    
        public void save()
        {
            var parent = GameObject.FindObjectOfType<Tilemanger>();
            var child = parent.GetComponentsInChildren<Tile>();
            for (int i = 0; i < child.Length; i++)
            {

                tiledata t = new tiledata();
                t.tilename = child[i].name;
                t.tileuse = child[i].bstate;
                File.WriteAllText(Application.dataPath + "/data.json", JsonUtility.ToJson(t));
            }
        }
        public void load()
        {
            string str2 = File.ReadAllText(Application.dataPath + "/data.json");

            tiledata t = new tiledata(); 
            t = JsonUtility.FromJson<tiledata>(str2);
            var parent = GameObject.FindObjectOfType<Tilemanger>();
            var child = parent.GetComponentsInChildren<Tile>();
        }
}
    public class tiledata
    {
        public string tilename;
        public buildingstate tileuse;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace monster
{
    public class monster: MonoBehaviour 
    {
        List<Vector3> tile;
        public float damamge;
        float speed;
        private void Awake()
        {
            tile = new List<Vector3>();
            var tilemanger = GameObject.FindObjectOfType<Tilemanger>();
            for (int i = 0; i < tilemanger.xcolidercount-1; i++)
            {
                for (int j = 0; j < tilemanger.zcolidercount-1; j++)
                {
                    if (i == 0 || j == tilemanger.xcolidercount - 1)
                    {
                        var tileposition = GameObject.Find(i.ToString()+j.ToString());
                        tile.Add(tileposition.transform.position);
                    }
                }
            }
            for (int i = 0; i < tilemanger.zcolidercount - 1; i++)
            {
                for (int j = 0; j < tilemanger.xcolidercount - 1; j++)
                {
                    if (i == 0 || j == tilemanger.zcolidercount - 1)
                    {
                        var tileposition = GameObject.Find(i.ToString() + j.ToString());
                        tile.Add(tileposition.transform.position);
                    }
                }
            }

        }
        private void Update()
        {
            attack();
            move();
        }
        public virtual void attack()
        {
            Debug.DrawRay(this.transform.position, Vector3.forward, Color.red, 5f);
            var hitinformation = new RaycastHit();
            var hit = Physics.Raycast(this.transform.position,Vector3.forward,out hitinformation ,5.0f);
            if (tile.Contains(this.transform.position))
               hitinformation.collider.GetComponent<wall>().energy -= damamge;
        }

      public virtual void  move()
      {
            transform.Translate(Vector3.forward*speed*Time.deltaTime);
      }

    }
}

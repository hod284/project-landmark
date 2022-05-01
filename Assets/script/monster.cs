using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace monster
{
    public class monster: MonoBehaviour 
    {
        public float damamge;
        float speed;
        List<Tile> mroad;
        RaycastHit  hitinformation;
        private void Awake()
        {
            mroad = GameObject.FindObjectOfType<player>().mroadlist;
            this.transform.position = mroad[0].gameObject.transform.position;
            hitinformation = new RaycastHit();
        }
        private void Update()
        {
            Debug.DrawRay(this.transform.position, Vector3.forward, Color.red, 5f);
            Physics.Raycast(this.transform.position, Vector3.forward, out hitinformation, 5.0f);
            var wallattack = new wall();
            if (TryGetComponent<wall>(out wallattack))
            attack();
            else if( hitinformation.collider !=null)
            move();
        }
        public virtual void attack()
        {
            Physics.Raycast(this.transform.position, Vector3.forward, out hitinformation, 5.0f);
            hitinformation.collider.GetComponent<wall>().energy -= damamge;
        }

      public virtual void  move()
      {
            var fowardname =  hitinformation.collider.GetComponent<Tile>().name;
            var fxn = 0;
            int.TryParse(fowardname[0].ToString(), out fxn);
            var fzn = 0;
            int.TryParse(fowardname[1].ToString(), out fzn);
            var nowname = (fxn-1).ToString() + fzn.ToString();
            var index = mroad.FindIndex(x => x.gameObject.name == nowname);
            var nextname= mroad[index + 1].name;
            var nxn = 0;
            int.TryParse(nextname[0].ToString(), out nxn);
            var nzn = 0;
            int.TryParse(nextname[1].ToString(), out nzn);
            if (fxn < nxn)
               transform.eulerAngles= new Vector3(0,90,0);
            else if (fxn == nxn&& fzn <nzn)
                transform.eulerAngles = new Vector3(0, 180, 0);
            else if (fxn == nxn && fzn > nzn)
                transform.eulerAngles = new Vector3(0, 360, 0);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);  
      }

    }
}

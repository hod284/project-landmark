using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enumspace;
public class BuildingInformation : MonoBehaviour
{
    public selectbuilding selectornot;
    public int xmapcolidercount;
    public int zmapcolidercount;
    // Start is called before the first frame update
    void Awake ()
    {
        // this.gameObject.SetActive(false);
        //  selectornot = selectbuilding.notselect;
    }

    // Update is called once per frame
    void Update()
    {
        if (selectornot == selectbuilding.select)
        {
            var pointsx = 0;
            var pointsz = 0;
            var pointex = 0;
            var pointez = 0;
            if (Input.GetMouseButton(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Physics.Raycast(ray, out hit);
                var hitgameobject = hit.collider;
              
                if (hitgameobject!=null&&hitgameobject.GetComponent<Tile>().tilestate != tileuse.use)
                {
                    for (int i = pointsx; i < pointex; i++)
                    {
                        for (int j = pointsz; j < pointez; j++)
                        {
                            var tile = GameObject.Find(i.ToString() + j.ToString());
                            tile.GetComponent<Tile>().tilestate = tileuse.notuse;
                        }
                    }
                    pointsx = 0;
                    pointsz = 0;
                    pointex = 0;
                    pointez = 0;
                    if (xmapcolidercount != 0 && zmapcolidercount != 0)
                    {
                        var colidername = hitgameobject.name;
                        var locationx = 0.0f;
                        var locationz = 0.0f;
                        var x = colidername[0];
                        var z = colidername[1];

                        var tilemapmanger = GameObject.FindObjectOfType<Tilemanger>();
                        if (xmapcolidercount == 2)
                        {
                            var xposition = xmapcolidercount;
                            locationx = x == tilemapmanger.xcolidercount - 1 ? (Mathf.Abs(hitgameobject.transform.position.x) - xposition) / 2 : (Mathf.Abs(hitgameobject.transform.position.x) + xposition) / 2;
                            pointsx = x == tilemapmanger.xcolidercount - 1 ? x - 1 : x;
                            pointex = pointsx + 2;
                        }
                        else if (xmapcolidercount > 2)
                        {
                            if (x != tilemapmanger.xcolidercount - 1 || x != 0)
                            {
                                var left = 0.0f;
                                var right = 0.0f;
                                if (xmapcolidercount % 2 == 0)
                                {
                                    left = Mathf.Ceil((xmapcolidercount - 1) / 2);
                                    right = Mathf.Floor((xmapcolidercount - 1) / 2);
                                }
                                else
                                {
                                    left = (xmapcolidercount - 1) / 2;
                                    right = (xmapcolidercount - 1) / 2;
                                }
                                var intx = 0;
                                int.TryParse(x.ToString(), out intx);
                                pointsx = x - (int)left;
                                pointex = x + (int)right;
                                locationx = (Mathf.Abs(hitgameobject.transform.position.x) - left + Mathf.Abs(hitgameobject.transform.position.x) + right) / 2;
                            }
                            else if (x == tilemapmanger.xcolidercount - 1 || x == 0)
                            {
                                pointsx = x == 0 ? 0 : tilemapmanger.xcolidercount - xmapcolidercount;
                                pointex = pointsx + xmapcolidercount;
                                locationx = x == 0 ? (Mathf.Abs(hitgameobject.transform.position.x) + xmapcolidercount) / 2 : (Mathf.Abs(hitgameobject.transform.position.x) - xmapcolidercount) / 2;
                            }
                        }
                        else
                        {
                            pointsx = x;
                            pointex = pointsx + 1;
                            locationx = hit.collider.transform.position.x;
                        }
                        if (zmapcolidercount == 2)
                        {
                            var zposition = zmapcolidercount;
                            locationz = z == tilemapmanger.zcolidercount - 1 ? (Mathf.Abs(hitgameobject.transform.position.z) - zposition) / 2 : (Mathf.Abs(hitgameobject.transform.position.z) + zposition) / 2;
                            pointsz = z == tilemapmanger.xcolidercount - 1 ? z - 1 : z;
                            pointez = pointsz + 2;
                        }
                        else if (zmapcolidercount > 2)
                        {
                            if (z != tilemapmanger.xcolidercount - 1 || z == 0)
                            {
                                var left = 0.0f;
                                var right = 0.0f;
                                if (zmapcolidercount % 2 == 0)
                                {
                                    left = Mathf.Ceil((zmapcolidercount - 1) / 2);
                                    right = Mathf.Floor((zmapcolidercount - 1) / 2);
                                }
                                else
                                {
                                    left = (zmapcolidercount - 1) / 2;
                                    right = (zmapcolidercount - 1) / 2;
                                }
                                var intz = 0;
                                int.TryParse(z.ToString(), out intz);
                                pointsz = z - (int)left;
                                pointez = z + (int)right;
                                locationz = ((Mathf.Abs(hitgameobject.transform.position.z) - left) + (Mathf.Abs(hitgameobject.transform.position.z) + right)) / 2;
                            }
                            else if (z == tilemapmanger.zcolidercount - 1 || z == 0)
                            {
                                pointsz = z == 0 ? 0 : tilemapmanger.zcolidercount - zmapcolidercount;
                                pointez = pointsz + zmapcolidercount;
                                locationz = z == 0 ? (Mathf.Abs(hitgameobject.transform.position.z) + zmapcolidercount) / 2 : (Mathf.Abs(hitgameobject.transform.position.z) - zmapcolidercount) / 2;
                            }
                        }
                        else
                        {
                            pointsz = z;
                            pointez = pointsz + 1;
                            locationz = hit.collider.transform.position.z;
                        }
                        transform.position = new Vector3(locationx, 10.0f, locationz);

                    }                 
                    for (int i = pointsx; i < pointex; i++)
                    {
                        for (int j = pointsz; j < pointez; j++)
                        {
                            var tile = GameObject.Find(i.ToString() + j.ToString());
                            tile.GetComponent<Tile>().tilestate = tileuse.willuse;
                        }
                    }
                }
                else
                {
                    if (pointsz != 0 && pointez != 0 && pointsx != 0 && pointex != 0)
                    {
                        for (int i = pointsx; i < pointex; i++)
                        {
                            for (int j = pointsz; j < pointez; j++)
                            {
                                var tile = GameObject.Find(i.ToString() + j.ToString());
                                tile.GetComponent<Tile>().tilestate = tileuse.notuse;
                            }
                        }
                        pointsx = 0;
                        pointsz = 0;
                        pointex = 0;
                        pointez = 0;
                    }
                    Vector3 pos = Input.mousePosition;
                    pos.z = Camera.main.farClipPlane;
                    //마우스포인터에 따라 오브젝트를 움직일때 마우스의 포지션을 월드로 바꾸어 주어야 할 필요가 있음 마우스 포인트 xy밖에 안나오며 이때 z 포지션을 설정해주어야 한다
                    // z포지션은 카메라와 오브젝트 사이의 거리를 설정한거라고 생각해주면 됨 만약z를 설정 해주지 않으면 오브젝트가 마우스 포인터를 안따라간다
                    transform.position = Camera.main.ScreenToWorldPoint(pos);
                }
            }
        }
        
    }
}

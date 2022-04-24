using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enumspace;
public class BuildingInformation : MonoBehaviour
{
    public GameObject cube;
    public selectbuilding selectornot;
    public int xmapcolidercount;
    public int zmapcolidercount;
    public Material canbuilt;
    public Material donbuilt;
    public Material original;
    [HideInInspector]
    public float leftx ;
    [HideInInspector]
    public float rightx ;
    [HideInInspector]
    public float leftz ;
    [HideInInspector]
    public float rightz ;
    // Start is called before the first frame update
    void Awake ()
    {
        leftx = 0.0f;
        rightx = 0.0f;
        leftz = 0.0f;
        rightz = 0.0f;
        this.gameObject.SetActive(false);
        selectornot = selectbuilding.notselect;
    }

    // Update is called once per frame
    void Update()
    {
        if (selectornot == selectbuilding.select)
        {
            transform.SetParent(cube.transform);
            transform.position = Vector3.zero;
            cube.transform.localScale = new Vector3(xmapcolidercount, 1, zmapcolidercount);
            if (Input.GetMouseButton(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red, 5f);
                var hit = Physics.RaycastAll(ray);
                var hitgameobject = transform.gameObject;
                if (hit.Length >0)
                  hitgameobject = hit[0].collider.gameObject;
                var tilemg = GameObject.FindObjectOfType<Tilemanger>();
                var inthetile = false;
                for (int i = 0; i < hit.Length; i++)
                {
                    if (hit[i].collider == tilemg.tilecolider)
                    {
                        inthetile = true;
                        break;
                    }
                    else
                        inthetile = false;
                }
                if (hitgameobject != null && inthetile)
                {
                    Tile tile;
                    hitgameobject.TryGetComponent<Tile>(out tile);
                    BuildingInformation information;
                    hitgameobject.TryGetComponent<BuildingInformation>(out information);
                    if (tile != null)
                    {
                        if (xmapcolidercount != 0 && zmapcolidercount != 0 && tile.bstate != buildingstate.use)
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
                                leftx = x == 0 ? xmapcolidercount: 0;
                                rightx = x == tilemapmanger.xcolidercount - 1? xmapcolidercount:0;
                            }
                            else if (xmapcolidercount > 2)
                            {
                                if (x != tilemapmanger.xcolidercount - 1 || x != 0)
                                {
                                    if (xmapcolidercount % 2 == 0)
                                    {
                                        leftx = Mathf.Ceil((xmapcolidercount ) / 2);
                                        rightx =Mathf.Floor((xmapcolidercount ) / 2);
                                    }
                                    else
                                    {
                                        leftx = (xmapcolidercount - 1) / 2;
                                        rightx = (xmapcolidercount - 1) / 2;
                                    }
                                    locationx = (Mathf.Abs(hitgameobject.transform.position.x) - leftx + Mathf.Abs(hitgameobject.transform.position.x) + rightx) / 2;
                                }
                                else if (x == tilemapmanger.xcolidercount - 1 || x == 0)
                                {
                                    leftx = x==0?  xmapcolidercount : 0;
                                    rightx = x==tilemapmanger.xcolidercount?xmapcolidercount :0;
                                    locationx = x == 0 ? (Mathf.Abs(hitgameobject.transform.position.x) + xmapcolidercount) / 2 : (Mathf.Abs(hitgameobject.transform.position.x) - xmapcolidercount) / 2;
                                }
                            }
                            else
                            {
                                leftx = 0;
                                rightx = 0;
                                locationx = hitgameobject.transform.position.x;
                            }
                            if (zmapcolidercount == 2)
                            {
                                var zposition = zmapcolidercount;
                                locationz = z == tilemapmanger.zcolidercount - 1 ? (Mathf.Abs(hitgameobject.transform.position.z) - zposition) / 2 : (Mathf.Abs(hitgameobject.transform.position.z) + zposition) / 2;
                                leftz = z == 0 ? zmapcolidercount  : 0;
                                rightz = z == tilemapmanger.zcolidercount - 1 ? zmapcolidercount  : 0;
                            }
                            else if (zmapcolidercount > 2)
                            {
                                if (z != tilemapmanger.xcolidercount - 1 || z != 0)
                                {
                                    if (zmapcolidercount % 2 == 0)
                                    {
                                        leftz = Mathf.Ceil((zmapcolidercount) / 2);
                                        rightz = Mathf.Floor((zmapcolidercount) / 2);
                                    }
                                    else
                                    {
                                        leftz = (zmapcolidercount-1) / 2;
                                        rightz = (zmapcolidercount-1) / 2;
                                    }
                                    locationz = ((Mathf.Abs(hitgameobject.transform.position.z) - leftz) + (Mathf.Abs(hitgameobject.transform.position.z) + rightz)) / 2;
                                }
                                else if (z == tilemapmanger.zcolidercount - 1 || z == 0)
                                {
                                    leftz = z == 0 ? zmapcolidercount  : 0;
                                    rightz = z == tilemapmanger.zcolidercount ? zmapcolidercount  : 0;
                                    locationz = z == 0 ? (Mathf.Abs(hitgameobject.transform.position.z) + zmapcolidercount) / 2 : (Mathf.Abs(hitgameobject.transform.position.z) - zmapcolidercount) / 2;
                                }
                            }
                            else
                            {
                                leftz = 0;
                                rightz = 0;
                                locationz = hitgameobject.transform.position.z;
                            }
                            cube.transform.position = new Vector3(locationx, 10.0f, locationz);
                            var lx = x- (int)leftx;
                            var rx = x+ (int)rightx;
                            var lz = z- (int)leftz;
                            var rz = z+ (int)rightz;
                            if(lx>0&&rx<tilemapmanger.zcolidercount&& lz > 0 && rz < tilemapmanger.zcolidercount)
                                cube.transform.GetComponent<Renderer>().material = canbuilt;
                            else
                                cube.transform.GetComponent<Renderer>().material = donbuilt;
                        }
                       
                    }
                    else if (information != null && !inthetile)
                    {
                        
                        if (information.selectornot == selectbuilding.select)
                        {
                            cube.transform.GetComponent<Renderer>().material = original;
                            Vector3 pos = Input.mousePosition;
                            pos.z = Camera.main.farClipPlane;
                            //마우스포인터에 따라 오브젝트를 움직일때 마우스의 포지션을 월드로 바꾸어 주어야 할 필요가 있음 마우스 포인트 xy밖에 안나오며 이때 z 포지션을 설정해주어야 한다
                            // z포지션은 카메라와 오브젝트 사이의 거리를 설정한거라고 생각해주면 됨 만약z를 설정 해주지 않으면 오브젝트가 마우스 포인터를 안따라간다
                            var position = Camera.main.ScreenToWorldPoint(pos);
                            transform.position = position;
                        }
                    }
                }
         
            }
        }
        
    }
}

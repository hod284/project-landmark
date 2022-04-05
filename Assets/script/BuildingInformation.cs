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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        if(selectornot== selectbuilding.select&&hit.collider.gameObject.layer !=3)
        {
            Vector3 pos = Input.mousePosition;
            pos.z = Camera.main.farClipPlane;
            //마우스포인터에 따라 오브젝트를 움직일때 마우스의 포지션을 월드로 바꾸어 주어야 할 필요가 있음 마우스 포인트 xy밖에 안나오며 이때 z 포지션을 설정해주어야 한다
            // z포지션은 카메라와 오브젝트 사이의 거리를 설정한거라고 생각해주면 됨 만약z를 설정 해주지 않으면 포인트가 제대로 안나온다
            transform.position = Camera.main.ScreenToWorldPoint(pos);
        }
    }
}

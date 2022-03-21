using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enumspace;

public class player : MonoBehaviour
{
    public float distance;
    public float limitex;
    public float limitey;
    public float zoomspeed;
    float devicex;
    float  devicey;
    buildmodeornot buildmodeornot;

    private void Awake()
    {
        // 3D 카메라 fov 맞추기
        var width = Screen.width / 2;
        var angle = Mathf.Atan((distance / width)) * Mathf.Rad2Deg;
        Camera.main.fieldOfView = angle * 2;

    }
    // Start is called before the first frame update
    void Start()
    {
        buildmodeornot = buildmodeornot.nobuild;
        devicex= Screen.width/2;
        devicey= Screen.height/2;
    }

    // Update is called once per frame
    void Update()
    {
       var currentx = Mathf.Abs(Camera.main.transform.transform.localPosition.x) + devicex;
       var  currenty = Mathf.Abs(Camera.main.transform.transform.localPosition.y) + devicey;

        if (buildmodeornot == buildmodeornot.nobuild)
        {
            if (limitex > currentx && -limitex < -currentx && limitey > currenty && -limitey < -currenty&&Input.touchCount==1)
            {
                var moveAmount = new Vector3(Camera.main.transform.localPosition.x +Input.GetTouch(0).deltaPosition.x, Camera.main.transform.localPosition.y + Input.GetTouch(0).deltaPosition.y, Camera.main.transform.localPosition.z);
                Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, moveAmount, Time.deltaTime / 10.0f);
            }
            if (devicex < Screen.width/2 && devicex > (Screen.width / 2)-50 && Input.touchCount == 2)
            {

                var prevTouchAPos = Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition;
                var prevTouchBPos = Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition;
                var curTouchAPos = Input.GetTouch(0).position;
                var curTouchBPos = Input.GetTouch(1).position;
                var deltaDistance = Vector2.Distance(Normalize(curTouchAPos), Normalize(curTouchBPos))- Vector2.Distance(Normalize(prevTouchAPos), Normalize(prevTouchBPos));
                devicex -= deltaDistance * zoomspeed;
                devicey -= deltaDistance * zoomspeed;
                Camera.main.fieldOfView = (Mathf.Atan((distance / devicex)) * Mathf.Rad2Deg)*2;
            }

        }
    }
    // 화면의 크기가 핸드폰 마다 다르기 때문에 스크린좌표를 받은뒤 -1과 1의 값으로 정규화
    private Vector2 Normalize(Vector2 position)
    {
        var normlizedPos = new Vector2(
            (position.x - Screen.width * 0.5f) / (Screen.width * 0.5f),
            (position.y - Screen.height * 0.5f) / (Screen.height * 0.5f));
        return normlizedPos;
    }

}

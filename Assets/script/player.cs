using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enumspace;
using System.Text;
public class player : MonoBehaviour
{
    public float limitex;
    public float limitey;
    public float zoomspeed;
    float devicex;
    float distance;
    float  devicey;
    float standangle;
    buildmodeornot buildmodeornot;

    private void Awake()
    {
        //드디어 예전에 2와 3d 카메라를 같이 쓸때 썼던 방식이 생각났다 
        // 주로 원근 투영은 3d 직교투영을 2d를 표현한다 그래서 2개의 카메라를 쓰는데 두개의 카메라를 쓸때는 중요한것 두개의 카메라가 가시영역이 교차 해야된다
        // 안그럼 빈공간이 생겨 ui가 이상하게 나온다 
        //fov를 맞추어 주어야 하는데 삼각함수에 tan를 구하는 공삭울 이용한다 거리를 밑면 직교투영의 size를 높이로 한다
        //orthographicSize는 카메라의 사이즈를 절반을표현하기 때문에 먼저 카메라의 크기에 따라 직교투영의 사이즈를 맞춘뒤에 그것을 높이로 이용하여 앵글을 구하면 끜
        // 여기서는 2d 카메라를 쓰지 않을 것이다 그래서 해상도를 이용해 사이즈를 구한뒤 그것을 이용해 앵글을 구할것임
        // 거리는 원근투영의 nearclipPlane을이용
        var width =Screen.height/(100.0f*2.0f);
        devicex = Screen.width / 2;
        devicey = Screen.height / 2;
        distance = Camera.main.nearClipPlane;
        // 3D 카메라 fov 맞추기
        var angle = Mathf.Atan(width/distance) * Mathf.Rad2Deg;
        Camera.main.fieldOfView = angle * 2;
        standangle = Camera.main.fieldOfView;
    }
    // Start is called before the first frame update
    void Start()
    {
        buildmodeornot = buildmodeornot.nobuild;
    }

    // Update is called once per frame
    void Update()
    {
            var currentx = Camera.main.transform.transform.localPosition.x > 0 ? Camera.main.transform.transform.localPosition.x + devicex : Camera.main.transform.transform.localPosition.x - devicex;
            var currenty = Camera.main.transform.transform.localPosition.y > 0 ? Camera.main.transform.transform.localPosition.y + devicey : Camera.main.transform.transform.localPosition.y - devicey;
            if (Mathf.Abs(limitex) > currentx && -Mathf.Abs(limitex) < currentx && Mathf.Abs(limitey) > currenty && -Mathf.Abs(limitey) < currenty && Input.touchCount == 1)
            {
                var moveAmount = new Vector3(Camera.main.transform.localPosition.x + Input.GetTouch(0).deltaPosition.x, Camera.main.transform.localPosition.y + Input.GetTouch(0).deltaPosition.y, Camera.main.transform.localPosition.z);
                Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.   localPosition, moveAmount, Time.deltaTime / 10.0f);
            }
            if (Camera.main.fieldOfView <= standangle && Camera.main.fieldOfView > standangle - 50 && Input.touchCount == 2)
            {

                var prevTouchAPos = Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition;
                var prevTouchBPos = Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition;
                var curTouchAPos = Input.GetTouch(0).position;
                var curTouchBPos = Input.GetTouch(1).position;
                var deltaDistance = Vector2.Distance(Normalize(curTouchAPos), Normalize(curTouchBPos)) - Vector2.Distance(Normalize(prevTouchAPos), Normalize(prevTouchBPos));
                Camera.main.fieldOfView -= deltaDistance;
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

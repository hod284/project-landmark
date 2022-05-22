using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameracontrol: MonoBehaviour
{
    public Camera uicamera;
    public float distance;
    int setWidth = 1920; // 사용자 설정 너비
    int setHeight = 1080; // 사용자 설정 높이

    // Start is called before the first frame update
    void Start()
    {

        //드디어 예전에 2와 3d 카메라를 같이 쓸때 썼던 방식이 생각났다 
        // 주로 원근 투영은 3d 직교투영을 2d를 표현한다 그래서 2개의 카메라를 쓰는데 두개의 카메라를 쓸때는 중요한것 두개의 카메라가 가시영역이 교차 해야된다
        // 안그럼 빈공간이 생겨 ui가 이상하게 나온다 
        //fov를 맞추어 주어야 하는데 삼각함수에 tan를 구하는 공삭울 이용한다 거리를 밑면 직교투영의 size를 높이로 한다
        //orthographicSize는 카메라의 사이즈를 절반을표현하기 때문에 먼저 카메라의 크기에 따라 직교투영의 사이즈를 맞춘뒤에 그것을 높이로 이용하여 앵글을 구하면 끜
        // 여기서는 2d 카메라를 쓰지 않을 것이다 그래서 해상도를 이용해 사이즈를 구한뒤 그것을 이용해 앵글을 구할것임
        // 거리는 원근투영의 nearclipPlane을이용하거나 직접지정
        int deviceWidth = Screen.width;
        int deviceHeight = Screen.height;
        var camera = Camera.main;
        // UI 카메라 사이즈 맞추기
        uicamera.orthographicSize = (deviceHeight / (deviceWidth / 16.0F)) / 9.0F;
        // 3D 카메라 fov 맞추기
        // 프로스터(절두체 컬링)에대한 카메라 해상도 대응

        var angle = Mathf.Atan(setWidth * 0.5f / distance) * Mathf.Rad2Deg;
        camera.fieldOfView = angle * 2;

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true); // SetResolution 함수 제대로 사용하기

        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // 기기의 해상도 비가 더 큰 경우
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // 새로운 너비
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // 새로운 Rect 적용
        }
        else // 게임의 해상도 비가 더 큰 경우
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // 새로운 높이
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // 새로운 Rect 적용
        }
        uicamera.rect = Camera.main.rect;
    }

    // Update is called once per frame
    void Update()
    {
      
    
    }
}

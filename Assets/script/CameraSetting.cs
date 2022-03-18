using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetting : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera uicamera;
    public float distance;
    void Start()
    {
        int deviceWidth = Screen.width;
        int deviceHeight = Screen.height;
        var camera = Camera.main;
        // UI 카메라 사이즈 맞추기
        uicamera.orthographicSize = (deviceHeight / (deviceWidth / 16.0F)) / 9.0F;
        // 3D 카메라 fov 맞추기
        var canvus = FindObjectOfType<Canvas>();
        var width = FindObjectOfType<Canvas>().GetComponent<RectTransform>().rect.width / 2;
        var angle = Mathf.Atan((distance / width)) * Mathf.Rad2Deg;
        camera.fieldOfView = angle * 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

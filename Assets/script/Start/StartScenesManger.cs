using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartScenesManger : MonoBehaviour
{
    public Camera uicamera;
    public float distance;
    private float degree=0.0f;
    public Slider loadingbar;
    private void Awake()
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
    private void Start()
    {
        loadingbar.gameObject.SetActive(false);
    }

    private void Update()
    {
        //스카이 박스 돌리기
        degree += Time.deltaTime/10;
        if (degree >= 360)
            degree = 0;
        RenderSettings.skybox.SetFloat("_Rotation", degree);
    }

    public void Changescence()
    {
        // 비동기로 플레이 씬을 불러오기
        var canvus = FindObjectOfType<Canvas>();
        var stbutton=  canvus.GetComponentInChildren<Button>();
        stbutton.gameObject.SetActive(false);
        loadingbar.gameObject.SetActive(true);
        StartCoroutine(loading());
    }

    private IEnumerator loading()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(1);
        async.allowSceneActivation = false;
        var Timer = 0.0f;
        while (!async.isDone)
        {
            // AsyncOperation에 있는 progress 가 0.9가 최대 인것 같다고 함
            Timer += Time.deltaTime;
            if (async.progress < 0.9f)
             loadingbar.value = Mathf.Lerp(async.progress, 1f, Timer);
            else
            {
               loadingbar.value = Mathf.Lerp(loadingbar.value, 1f, Timer);
                if (loadingbar.value >= 0.99f)
                    async.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}

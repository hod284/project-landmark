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
        // UI ī�޶� ������ ���߱�
        uicamera.orthographicSize = (deviceHeight / (deviceWidth / 16.0F)) / 9.0F;
        // 3D ī�޶� fov ���߱�
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
        //��ī�� �ڽ� ������
        degree += Time.deltaTime/10;
        if (degree >= 360)
            degree = 0;
        RenderSettings.skybox.SetFloat("_Rotation", degree);
    }

    public void Changescence()
    {
        // �񵿱�� �÷��� ���� �ҷ�����
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
            // AsyncOperation�� �ִ� progress �� 0.9�� �ִ� �ΰ� ���ٰ� ��
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

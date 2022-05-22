using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartScenesManger : MonoBehaviour
{
    private float degree=0.0f;
    public Slider loadingbar;
    public GameObject basetile;
    public GameObject maptile;

    private void Awake()
    {
        var camerpositionx = 0.0f;
        var camerpositionz = 0.0f;
        var x = 0.0f;
        var z = 0.0f;
        for (int i = 0; i < 600; i++)
        {
            if (i == 0)
                x = 0.5f;      
            else
                x += 1.0f;
            if (i == 300)
                camerpositionx = x;
            for (int j = 0; j < 600; j++)
            {
                if (j== 0)
                    z = 0.5f;
               else
                    z += 1.0f;

                if (j == 300)
                    camerpositionz = z;

                if (i <350  && i > 250 && j < 350 && j > 250)
                    Instantiate(maptile, new Vector3(x, 0.5f, z), new Quaternion(), transform);
                 else
                Instantiate(basetile, new Vector3(x,0,z), new Quaternion(),transform);
            }
        }


        Camera.main.transform.position = new Vector3(camerpositionx,Camera.main.transform.position.y,camerpositionz);
    }
    private void Start()
    {
        loadingbar.gameObject.SetActive(false);
    }

    private void Update()
    {
        //스카이 박스 돌리기
     // degree += Time.deltaTime/10;
     // if (degree >= 360)
     //     degree = 0;
     // RenderSettings.skybox.SetFloat("_Rotation", degree);
        
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

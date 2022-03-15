using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScenesManger : MonoBehaviour
{
    public Material HologramShader;
    Material OriginalShader;
    GameObject SelectMapitem;
    public GameObject MapItemparent;
    void Start()
    {
        int setWidth = 1920; 
        int setHeight = 1080; 
        int deviceWidth = Screen.width; 
        int deviceHeight = Screen.height; 
        float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // 货肺款 臭捞
        Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // 货肺款 Rect 利侩 
    }
    public void movingintotheroom()
    {
        StartCoroutine(moving());
        var childmapitem = MapItemparent.GetComponentsInChildren<GameObject>();
        onmapitem(0, childmapitem);
    }
    IEnumerator moving()
    {
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, Vector3.forward, Time.deltaTime);
        yield return new WaitForSeconds(0.1f);
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, Vector3.forward, Time.deltaTime);
        yield return new WaitForSeconds(0.1f);
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, Vector3.forward, Time.deltaTime);
        yield return new WaitForSeconds(0.1f);
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, Vector3.forward, Time.deltaTime);
        yield return new WaitForSeconds(0.1f);
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, Vector3.forward, Time.deltaTime);
        yield return new WaitForSeconds(0.1f);
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, Vector3.forward, Time.deltaTime);
        yield return new WaitForSeconds(0.1f);
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, Vector3.forward, Time.deltaTime);
        yield return new WaitForSeconds(0.1f);
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, Vector3.forward, Time.deltaTime);
        yield return new WaitForSeconds(0.1f);
    }
    public void Selectmap()
    {
        if (SelectMapitem != null)
            SelectMapitem.GetComponent<MeshRenderer>().material = OriginalShader;
        var screenpoint = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit = new RaycastHit();
        if (Physics.Raycast(screenpoint.origin, screenpoint.direction, out raycastHit))
        {
            OriginalShader= raycastHit.transform.GetComponent<MeshRenderer>().material;
            raycastHit.transform.GetComponent<MeshRenderer>().material = HologramShader;
            SelectMapitem = raycastHit.transform.gameObject;
        }
    }
    public void onmapitem(int count, GameObject[] mapitem)
    {
        if (count == mapitem.Length-1)
            return;
        mapitem[count].gameObject.SetActive(true);
        onmapitem(count += 1,mapitem);
    }
    
}

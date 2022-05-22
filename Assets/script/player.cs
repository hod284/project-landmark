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
    [SerializeField]
    float distance;
    float  devicey;
    float standangle;
    [HideInInspector]
    public List<Tile> mroadlist;
    Ray ray;
    private void Awake()
    {
        
     
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
       

#if UNITY_EDITOR_WIN

         ray = Camera.main.ScreenPointToRay(Input.mousePosition);

#elif UNITY_ANDROID
       
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(nput.touchCount == 1)
        {
          var currentx = Camera.main.transform.transform.localPosition.x > 0 ? Camera.main.transform.transform.localPosition.x + devicex : Camera.main.transform.transform.localPosition.x - devicex;
        var currenty = Camera.main.transform.transform.localPosition.y > 0 ? Camera.main.transform.transform.localPosition.y + devicey : Camera.main.transform.transform.localPosition.y - devicey;
        
             if (Mathf.Abs(limitex) > currentx && -Mathf.Abs(limitex) < currentx && Mathf.Abs(limitey) > currenty && -Mathf.Abs(limitey) < currenty )
            {
                var moveAmount = new Vector3(Camera.main.transform.localPosition.x + Input.GetTouch(0).deltaPosition.x, Camera.main.transform.localPosition.y + Input.GetTouch(0).deltaPosition.y, Camera.main.transform.localPosition.z);
                Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, moveAmount, Time.deltaTime / 10.0f);
            }
        }
        if (Camera.main.fieldOfView <= standangle && Camera.main.fieldOfView > standangle - 50 && Input.touchCount == 2)
        {

            var prevTouchAPos = Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition;
            var prevTouchBPos = Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition;
            var curTouchAPos = Input.GetTouch(0).position;
            var curTouchBPos = Input.GetTouch(1).position;
            var deltaDistance = Vector2.Distance(Normalize(curTouchAPos), Normalize(curTouchBPos)) - Vector2.Distance(Normalize(prevTouchAPos), Normalize(prevTouchBPos));
            Camera.main.fieldOfView -= deltaDistance;
            devicex=   2.0f * distance * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad);
             devicey =  devicex / Camera.main.aspect;
       }
#endif
        var hit = new RaycastHit();
        Physics.Raycast(ray, out hit);
        var tile = new Tile();
        if (hit.collider.TryGetComponent<Tile>(out tile))
            hit.collider.GetComponent<Tile>().mroad = monsterroad.use;
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

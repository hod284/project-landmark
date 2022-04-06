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
        //���� ������ 2�� 3d ī�޶� ���� ���� ��� ����� �������� 
        // �ַ� ���� ������ 3d ���������� 2d�� ǥ���Ѵ� �׷��� 2���� ī�޶� ���µ� �ΰ��� ī�޶� ������ �߿��Ѱ� �ΰ��� ī�޶� ���ÿ����� ���� �ؾߵȴ�
        // �ȱ׷� ������� ���� ui�� �̻��ϰ� ���´� 
        //fov�� ���߾� �־�� �ϴµ� �ﰢ�Լ��� tan�� ���ϴ� ����� �̿��Ѵ� �Ÿ��� �ظ� ���������� size�� ���̷� �Ѵ�
        //orthographicSize�� ī�޶��� ����� ������ǥ���ϱ� ������ ���� ī�޶��� ũ�⿡ ���� ���������� ����� ����ڿ� �װ��� ���̷� �̿��Ͽ� �ޱ��� ���ϸ� ��
        // ���⼭�� 2d ī�޶� ���� ���� ���̴� �׷��� �ػ󵵸� �̿��� ����� ���ѵ� �װ��� �̿��� �ޱ��� ���Ұ���
        // �Ÿ��� ���������� nearclipPlane���̿�
        var width =Screen.height/(100.0f*2.0f);
        devicex = Screen.width / 2;
        devicey = Screen.height / 2;
        distance = Camera.main.nearClipPlane;
        // 3D ī�޶� fov ���߱�
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
    // ȭ���� ũ�Ⱑ �ڵ��� ���� �ٸ��� ������ ��ũ����ǥ�� ������ -1�� 1�� ������ ����ȭ
    private Vector2 Normalize(Vector2 position)
    {
        var normlizedPos = new Vector2(
            (position.x - Screen.width * 0.5f) / (Screen.width * 0.5f),
            (position.y - Screen.height * 0.5f) / (Screen.height * 0.5f));
        return normlizedPos;
    }

}

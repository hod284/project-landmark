using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameracontrol: MonoBehaviour
{
    public Camera uicamera;
    public float distance;
    int setWidth = 1920; // ����� ���� �ʺ�
    int setHeight = 1080; // ����� ���� ����

    // Start is called before the first frame update
    void Start()
    {

        //���� ������ 2�� 3d ī�޶� ���� ���� ��� ����� �������� 
        // �ַ� ���� ������ 3d ���������� 2d�� ǥ���Ѵ� �׷��� 2���� ī�޶� ���µ� �ΰ��� ī�޶� ������ �߿��Ѱ� �ΰ��� ī�޶� ���ÿ����� ���� �ؾߵȴ�
        // �ȱ׷� ������� ���� ui�� �̻��ϰ� ���´� 
        //fov�� ���߾� �־�� �ϴµ� �ﰢ�Լ��� tan�� ���ϴ� ����� �̿��Ѵ� �Ÿ��� �ظ� ���������� size�� ���̷� �Ѵ�
        //orthographicSize�� ī�޶��� ����� ������ǥ���ϱ� ������ ���� ī�޶��� ũ�⿡ ���� ���������� ����� ����ڿ� �װ��� ���̷� �̿��Ͽ� �ޱ��� ���ϸ� ��
        // ���⼭�� 2d ī�޶� ���� ���� ���̴� �׷��� �ػ󵵸� �̿��� ����� ���ѵ� �װ��� �̿��� �ޱ��� ���Ұ���
        // �Ÿ��� ���������� nearclipPlane���̿��ϰų� ��������
        int deviceWidth = Screen.width;
        int deviceHeight = Screen.height;
        var camera = Camera.main;
        // UI ī�޶� ������ ���߱�
        uicamera.orthographicSize = (deviceHeight / (deviceWidth / 16.0F)) / 9.0F;
        // 3D ī�޶� fov ���߱�
        // ���ν���(����ü �ø�)������ ī�޶� �ػ� ����

        var angle = Mathf.Atan(setWidth * 0.5f / distance) * Mathf.Rad2Deg;
        camera.fieldOfView = angle * 2;

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true); // SetResolution �Լ� ����� ����ϱ�

        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // ����� �ػ� �� �� ū ���
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // ���ο� �ʺ�
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // ���ο� Rect ����
        }
        else // ������ �ػ� �� �� ū ���
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // ���ο� ����
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // ���ο� Rect ����
        }
        uicamera.rect = Camera.main.rect;
    }

    // Update is called once per frame
    void Update()
    {
      
    
    }
}

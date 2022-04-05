using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enumspace;
public class BuildingInformation : MonoBehaviour
{
    public selectbuilding selectornot;
    public int xmapcolidercount;
    public int zmapcolidercount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        if(selectornot== selectbuilding.select&&hit.collider.gameObject.layer !=3)
        {
            Vector3 pos = Input.mousePosition;
            pos.z = Camera.main.farClipPlane;
            //���콺�����Ϳ� ���� ������Ʈ�� �����϶� ���콺�� �������� ����� �ٲپ� �־�� �� �ʿ䰡 ���� ���콺 ����Ʈ xy�ۿ� �ȳ����� �̶� z �������� �������־�� �Ѵ�
            // z�������� ī�޶�� ������Ʈ ������ �Ÿ��� �����ѰŶ�� �������ָ� �� ����z�� ���� ������ ������ ����Ʈ�� ����� �ȳ��´�
            transform.position = Camera.main.ScreenToWorldPoint(pos);
        }
    }
}

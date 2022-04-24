using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towermanger : MonoBehaviour
{
    public GameObject cube;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void setbuilding()
    {
        var child = GetComponentInChildren<Transform>();
        child.transform.SetParent(transform);
        child.transform.position = cube.transform.position;
    } 
}

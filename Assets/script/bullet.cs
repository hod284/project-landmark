using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [HideInInspector]
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.forward*speed*Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.SetActive(false);
    }
}

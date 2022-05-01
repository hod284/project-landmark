using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace attackmechine
{
    public class attackmechine : MonoBehaviour
    {
        public float attackdamege;
        public GameObject head;
        public float speed;
        public GameObject bullet;
       
        private void OnTriggerStay(Collider other)
        {
            head.transform.LookAt(other.transform);
            if (!bullet.activeSelf)
            {
                bullet.GetComponent<bullet>().speed = speed;
                bullet.gameObject.SetActive(true);
            }
        }


    }
}

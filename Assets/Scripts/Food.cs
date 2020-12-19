using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.I.enemies.Add(gameObject.transform);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Kapan")
        {
            GameManager.I.enemies.Remove(gameObject.transform);
            GameManager.I.check = true;
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        GameManager.I.enemies.Remove(gameObject.transform);
        GameManager.I.check = true;
        Destroy(gameObject);
    }
}

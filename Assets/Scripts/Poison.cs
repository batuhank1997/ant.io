using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "Player")
            other.gameObject.transform.localScale -= other.gameObject.transform.localScale * 0.01f;
    }
}

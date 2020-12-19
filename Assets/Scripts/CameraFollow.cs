using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    private Vector3 offset;

    void Start()
    {
        offset = new Vector3(0, 20, -20);
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player");

        if (target != null)
            transform.position = target.transform.position + offset + new Vector3(0, target.transform.localScale.x * 2, 0);
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player");
    }
}

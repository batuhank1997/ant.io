using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    private Vector3 offset;

    void Start()
    {
        target = GameManager.I.player;
        offset = new Vector3(0, 20, -20);
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
            transform.position = target.transform.position + offset;
    }
}

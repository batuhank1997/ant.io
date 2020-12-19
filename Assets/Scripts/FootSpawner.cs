using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSpawner : MonoBehaviour
{
    public GameObject foot;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnFoot", 10, 10);
    }

    // Update is called once per frame
    void Update()
    {
    }
    void SpawnFoot()
    {
        Vector3 position = new Vector3(Random.Range(-40, 40), 35, Random.Range(-30, 30));
        Instantiate(foot, position, Quaternion.identity);
    }
}

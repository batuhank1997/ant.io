using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject food;
    public float spawnTime;
    public GameObject[] foods;


    private void Awake()
    {
        FI = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 2, spawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        foods = GameObject.FindGameObjectsWithTag("Food");
    }

    void Spawn()
    {
        if (foods.Length < 20)
        {
            for (int i = 0; i < 7; i++)
            {
                Vector3 randomPoint = new Vector3(Random.Range(-40, 40), 0, Random.Range(-30, 30));

                Instantiate(food, randomPoint, Quaternion.identity);
            }
        }
        GameManager.I.check = true;
    }

    public static FoodSpawner FI;
    public static FoodSpawner FIn
    {
        get
        {
            if(FI == null)
            {
                FI = GameObject.Find("GameManager").GetComponent<FoodSpawner>();
            }
            return FI;
        }
    }
}

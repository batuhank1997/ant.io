using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextAbove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponentInChildren<TextMeshPro>().transform.rotation = Quaternion.Euler(45, 0, 0);

        if (gameObject.tag == "Player")
        {
            GetComponentInChildren<TextMeshPro>().text = Mathf.Round(transform.localScale.x * 1000).ToString();
        }
        else
        {
            GetComponentInChildren<TextMeshPro>().text = gameObject.name + "'s size: " + Mathf.Round(transform.localScale.x * 1000).ToString();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public float timer = 60f;
    public Text timerText;
    // Start is called before the first frame update
    void Start()
    {
        timerText.text = "Time: " + timer.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            GameManager.I.matchFinished = true;
            return;
        }

        timer -= Time.deltaTime;
        timerText.text = "Time: " + Mathf.Round(timer).ToString();
        
    }
}

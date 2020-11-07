using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public bool isGoing;
    public GameObject restart;

    public bool gameOver;

    private void Awake()
    {
        II = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameOver == true)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        restart.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static GameManager II;
    public static GameManager I
    {
        get
        {
            if(II == null)
            {
                II = GameObject.Find("GameManager").GetComponent<GameManager>();
            }
            return II;
        }
    }
}

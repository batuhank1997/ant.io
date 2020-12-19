using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public int playerSize;
    public GameObject playerPrefab;
    public bool isGoing;
    public GameObject restart;
    public GameObject matchFinishObj;
    public GameObject newEnemy;

    public bool gameOver;
    public bool matchFinished;
    public bool check = true;

    public GameObject[] enemyArray;

    public GameObject[] spawnPoints;
    public GameObject[] foodTargets;
    public List<Transform> enemies;

    //public GameObject[] enemiesInGame;

    public List<GameObject> enemiesInGame;


    public Text[] leaderboardTextsBoxes;


    Vector3 offset;

    private void Awake()
    {
        II = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<Transform>();
        offset = new Vector3(40, 0, 0);
        playerSize = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver == true)
        {
            GameOver();
        }
        if (matchFinished == true)
        {
            matchFinish();
        }
        SortScores();
        SpawnNewEnemy();
    }
    void SortScores()
    {

        //Debug.Log(enemiesInGame.Length);

        BubbleSort(enemiesInGame);

        for(int i = 0; i < enemiesInGame.Count; i++)
        {
            //Debug.Log(enemiesInGame[i].transform.localScale.x);
            if(enemiesInGame[i] != null)
                leaderboardTextsBoxes[i].text = (10 - (i + 1)) + ". " + enemiesInGame[i].name + "    " + Mathf.Round(enemiesInGame[i].transform.localScale.x * 1000).ToString();
            //else
                //leaderboardTextsBoxes[i].text = (9 - (i + 1)) + ". " + GameObject.FindGameObjectWithTag("Player").name + "    " + Mathf.Round(GameObject.FindGameObjectWithTag("Player").transform.localScale.x * 1000).ToString();
        }
    }
    void BubbleSort(List<GameObject> arr)
    {
        int n = arr.Count;

        for (int i = 0; i < n - 1; i++)
            for (int j = 0; j < n - i - 1; j++)
                if (arr[j].transform.localScale.x > arr[j + 1].transform.localScale.x)
                {
                    // swap temp and arr[i] 
                    GameObject temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
    }
    public void SpawnNewEnemy()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length < 8)
        {
            int rand = Random.Range(0, 3);
            int rand2 = Random.Range(0, 5);

            Instantiate(enemyArray[rand], spawnPoints[rand2].transform.position, Quaternion.identity);
        }
    }
    void GameOver()
    {
        restart.SetActive(true);
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void matchFinish()
    {
        matchFinishObj.SetActive(true);
    }

    public void Restart()
    {
        gameOver = false;
        Instantiate(playerPrefab, transform.position, Quaternion.identity);
        restart.SetActive(false);
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

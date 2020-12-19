using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Joystick joystick;
    private float horizontalMove;
    private float verticalMove;
    public float speed;
    private Rigidbody rb;
    private Quaternion lastRotation;
    public bool check;
    private Animator anim;
    public GameObject deathObj;
    bool onTouch;
    public float rotationSpeed = 5f;

    private void Awake()
    {
        GameManager.I.player = gameObject;
        joystick = FindObjectOfType<Joystick>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        check = true;
        GameManager.I.enemiesInGame.Add(gameObject);
        gameObject.name = "You";
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            rb.velocity = Vector3.zero;
            onTouch = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            onTouch = false;
        }

        //rb.velocity = Vector3.zero;

        transform.position = new Vector3(transform.position.x, transform.localScale.x / 2, transform.position.z);

        //rb.velocity = Vector3.zero;

        if (check)
        {
            GameManager.I.enemies.Add(gameObject.transform);
            check = false;
        }

        speed = (1 / gameObject.transform.localScale.x + 0.5f) * 500;



        if (gameObject.transform.localScale.x < 0.5f)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if(GameManager.I.matchFinished != true)
        {
            Movement();
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    void Movement()
    {
        anim = gameObject.GetComponentInChildren<Animator>();

        horizontalMove = joystick.Horizontal;
        verticalMove = joystick.Vertical;

        if(horizontalMove != 0 && verticalMove != 0)
        {
            //idle anim;
            anim.SetBool("isWalking", false);
        }
        else
        {
            anim.SetBool("isWalking", true);
        }

        rb.velocity = new Vector3(horizontalMove * speed * Time.fixedDeltaTime, 0, verticalMove * speed * Time.fixedDeltaTime);

        if (onTouch)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity.normalized);
            lastRotation = transform.rotation;

        } else
        {
            transform.rotation = lastRotation;
        }
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (transform.localScale.x > collision.gameObject.transform.localScale.x)
            {
                transform.localScale += collision.gameObject.transform.localScale * 0.5f;
                GameManager.I.enemies.Remove(collision.gameObject.transform);
                Instantiate(deathObj, collision.gameObject.transform.position, Quaternion.identity);
                deathObj.GetComponent<ParticleSystem>().GetComponent<Renderer>().material = collision.gameObject.GetComponent<MeshRenderer>().material;
                GameManager.I.enemiesInGame.Remove(collision.gameObject);
                Destroy(collision.gameObject);

            }
            else if (transform.localScale.x < collision.gameObject.transform.localScale.x)
            {
                collision.gameObject.transform.localScale += transform.localScale * 0.5f;
                check = false;
                GameManager.I.enemies.Remove(gameObject.transform);
                Instantiate(deathObj, transform.position, Quaternion.identity);
                deathObj.GetComponent<ParticleSystem>().GetComponent<Renderer>().material = gameObject.GetComponent<MeshRenderer>().material;
                GameManager.I.enemiesInGame.Remove(gameObject);
                Destroy(gameObject);
                GameManager.I.gameOver = true;
            }
        }
        
        if (collision.gameObject.tag == "Foot")
        {
            Instantiate(deathObj, transform.position, Quaternion.identity);
            deathObj.GetComponent<ParticleSystem>().GetComponent<Renderer>().material = gameObject.GetComponent<MeshRenderer>().material;
            GameManager.I.enemies.Remove(gameObject.transform);
            GameManager.I.enemiesInGame.Remove(gameObject);
            Destroy(gameObject);
            GameManager.I.gameOver = true;
        }
        if (collision.gameObject.tag == "Food")
        {
            transform.localScale += new Vector3(1 / (transform.localScale.x * 10), 1 / (transform.localScale.y * 10), 1 / (transform.localScale.z * 10));
            GameManager.I.playerSize++;
        }
        if (collision.gameObject.tag == "Kapan")
        {
            check = false;
            Instantiate(deathObj, transform.position, Quaternion.identity);
            deathObj.GetComponent<ParticleSystem>().GetComponent<Renderer>().material = gameObject.GetComponent<MeshRenderer>().material;
            GameManager.I.enemies.Remove(gameObject.transform);
            GameManager.I.enemiesInGame.Remove(gameObject);
            Destroy(gameObject);
            GameManager.I.gameOver = true;
        }
    }
    
}

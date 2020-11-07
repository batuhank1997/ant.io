using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Joystick joystick;
    private float horizontalMove;
    private float verticalMove;
    private float speed;
    private Rigidbody rb;
    private Quaternion lastRotation;

    private void Awake()
    {
        GameManager.I.player = gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        speed = (1 / gameObject.transform.localScale.x + 0.5f) * 500;

        Movement();

        if(gameObject.transform.localScale.x < 0.5f)
        {
            Destroy(gameObject);
        }
    }

    void Movement()
    {
        horizontalMove = joystick.Horizontal * speed * Time.deltaTime;
        verticalMove = joystick.Vertical * speed * Time.deltaTime;
        rb.velocity = new Vector3(horizontalMove, 0, verticalMove);
        if (rb.velocity != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity.normalized);
            lastRotation = transform.rotation;
        } else if(rb.velocity == Vector3.zero)
        {
            transform.rotation = lastRotation;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (transform.localScale.x > collision.gameObject.transform.localScale.x)
            {
                transform.localScale += collision.gameObject.transform.localScale * 0.5f;
                Destroy(collision.gameObject);
            }
            else if (transform.localScale.x < collision.gameObject.transform.localScale.x)
            {
                collision.gameObject.transform.localScale += transform.localScale * 0.5f;
                Destroy(gameObject);
                GameManager.I.gameOver = true;
            }
        }
        if (collision.gameObject.tag == "Food")
        {
            transform.localScale += new Vector3(1 / (transform.localScale.x * 10), 1 / (transform.localScale.y * 10), 1 / (transform.localScale.z * 10));
            Destroy(collision.gameObject);
        }
    }
}

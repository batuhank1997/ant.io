using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody rb;
    private float speed;
    public GameObject[] targets;
    private GameObject target;
    public Search search;
    private Quaternion lastRotation;
    public bool isGoing = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        speed = (1 / gameObject.transform.localScale.x + 0.5f) * 500;

        if (gameObject.transform.localScale.x < 0.5f)
        {
            Destroy(gameObject);
        }

        targets = GameObject.FindGameObjectsWithTag("Food");

        if (isGoing == false)
        {
            AI();
        }

        Movement();

    }

    void AI()
    {

        if (targets.Length == 0)
            return;
        int rand = Random.Range(0, targets.Length);
        target = targets[rand];
    }

 

    void Movement()
    {
        if (targets.Length != 0 && target != null)
        {
            rb.velocity = (target.transform.position - gameObject.transform.position).normalized * speed * Time.deltaTime;   //error
            isGoing = true;
        }
        else if(targets.Length == 0)
        {
            isGoing = false;

            if (GameManager.I.player != null)
            {
                if (transform.localScale.x > GameManager.I.player.transform.localScale.x)
                    rb.velocity = (GameManager.I.player.transform.position - transform.position).normalized;
            }
        }

        if (search.GetClosestEnemy(search.enemies, search.gameObject.transform) != null)
        {
            if (search.GetClosestEnemy(search.enemies, search.gameObject.transform).transform.localScale.x < transform.localScale.x)
            {
                rb.velocity = (search.GetClosestEnemy(search.enemies, search.gameObject.transform).position - gameObject.transform.position).normalized * speed * Time.deltaTime;
            if(isGoing)
                    isGoing = false;
            }
            else if (search.GetClosestEnemy(search.enemies, search.gameObject.transform).transform.localScale.x > transform.localScale.x)
            {
                rb.velocity = (gameObject.transform.position - search.GetClosestEnemy(search.enemies, search.gameObject.transform).position).normalized * speed * Time.deltaTime;
            if(isGoing)
                    isGoing = false;
            }
        }

        if (rb.velocity != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity.normalized);
            lastRotation = transform.rotation;
        }
        else if (rb.velocity == Vector3.zero)
        {
            transform.rotation = lastRotation;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player")
        {
            if (transform.localScale.x > collision.gameObject.transform.localScale.x)
            {
                transform.localScale += collision.gameObject.transform.localScale * 0.2f;
                search.enemies.Remove(collision.gameObject.transform);
                Destroy(collision.gameObject);
            }
        }

        if (collision.gameObject.tag == "Food")
        {
            transform.localScale += new Vector3(1 / (transform.localScale.x * 10), 1 / (transform.localScale.y * 10), 1 / (transform.localScale.z * 10));
            Destroy(collision.gameObject);
            isGoing = false;
        }

        if (collision.gameObject.tag == "Wall")
        {

        }
    }
}

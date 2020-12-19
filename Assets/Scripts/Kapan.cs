using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kapan : MonoBehaviour
{
    public Animator anim;
    private void Start()
    {
        anim = GetComponentInParent<Animator>();
        anim.SetBool("isTrigger", false);
    }
    private void Update()
    {
        
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        anim.SetBool("isTrigger", true);
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isTrigger", false);
    }

    
}

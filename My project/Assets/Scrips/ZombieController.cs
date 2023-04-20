using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    Rigidbody2D rb;
    public int velo = 2;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = new Vector2(-velo, rb.velocity.y);//hace que el zombie camine
    }
    void OnCollisionEnter2D(Collision2D other){

        if(other.gameObject.tag == "Bala"){
            Destroy(this.gameObject);
        }

    }
}
